using backend_app.DTO;
using backend_app.IRepository.dashboard;
using backend_app.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend_app.Services.dashboard
{
    public class FacilitieService : IFacilitie
    {
        private readonly DatabaseContext db;

        private readonly IWebHostEnvironment HostEnvironment;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public FacilitieService(DatabaseContext db, IWebHostEnvironment hostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            HostEnvironment = hostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IEnumerable<Facilities>> GetList()
        {
            var request = _httpContextAccessor.HttpContext.Request;
            return await db.Facilities.Select(x => new Facilities
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Image = string.Format("{0}://{1}{2}/{3}", request.Scheme, request.Host, request.PathBase, x.Image),
            }).ToListAsync();
        }


        public async Task<Facilities> store(FacilitieImage facilitieImage)
        {
            var facilities = new Facilities
            {
                Title = facilitieImage.Title,
                Description = facilitieImage.Desciption,
                Image = await SaveImage(facilitieImage.Image),
            };
            db.Facilities.Add(facilities);
            await db.SaveChangesAsync();
            return facilities;
        }
        public async Task<bool> checkTitle(FacilitieImage FacilitieImage)
        {
            if (FacilitieImage != null)
            {
                if (FacilitieImage.Id != null)
                {
                    return await db.Facilities.AnyAsync(a => a.Title == FacilitieImage.Title && a.Id != FacilitieImage.Id);
                }
                else
                {
                    return await db.Facilities.AnyAsync(a => a.Title == FacilitieImage.Title);
                }
            }

            return false;
        }
        public async Task<Facilities> GetEdit(int id)
        {
            var facilitie = await db.Facilities.FindAsync(id);
            if(facilitie != null)
            {
                var request = _httpContextAccessor.HttpContext.Request;
                var facilitieNew = new Facilities
                {
                    Id = facilitie.Id,
                    Title = facilitie.Title,
                    Description = facilitie.Description,
                    Image = string.Format("{0}://{1}{2}/{3}", request.Scheme, request.Host, request.PathBase, facilitie.Image),
                };
                return facilitieNew;
            }
            return null;
        }
        public async Task<Facilities> UpdateFacilitie(FacilitieImage facilitieImage)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var facilitie = await db.Facilities.FindAsync(facilitieImage.Id);
                    facilitie.Title = facilitieImage.Title;
                    facilitie.Description = facilitieImage.Desciption;

                    if (facilitieImage.Image != null)
                    {
                        var imagePath = Path.Combine(facilitie.Image);
                        if (File.Exists(imagePath))
                        {
                            File.Delete(imagePath);
                        }
                        facilitie.Image = await SaveImage(facilitieImage.Image);
                    }
                    await db.SaveChangesAsync();
                    transaction.Commit(); 
                    return facilitie;
                }
                catch (Exception)
                {
                    transaction.Rollback(); 
                    throw;
                }
            }
        }

        public async Task<bool> Delete(int id)
        {
            var facilitie = await db.Facilities.FindAsync(id);
            if (facilitie == null)
            {
                return false;
            }
            db.Facilities.Remove(facilitie);
            await db.SaveChangesAsync();
            var imagePath = Path.Combine(facilitie.Image);
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }
            return true;
        }
        [NonAction]
        public async Task<string> SaveImage(IFormFile formFile)
        {
            string imageName = new string(Path.GetFileNameWithoutExtension(formFile.FileName).Take(10).ToArray()).Replace(" ", "_");
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(formFile.FileName);

            // Thêm tiền tố "Image/Article/" vào imageName
            string imageNameWithPath = "Image/facilities/" + imageName;

            // Cập nhật đường dẫn lưu trữ ảnh
            var imagePath = Path.Combine(HostEnvironment.ContentRootPath, "Image", "facilities", imageName);

            // Tạo thư mục nếu nó chưa tồn tại
            var directoryPath = Path.GetDirectoryName(imagePath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await formFile.CopyToAsync(fileStream);
            }

            return imageNameWithPath;
        }
    }
}
