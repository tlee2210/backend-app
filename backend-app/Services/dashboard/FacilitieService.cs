using backend_app.DTO;
using backend_app.IRepository.dashboard;
using backend_app.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend_app.Services.dashboard
{
    public class FacilitieService : IFacilitie
    {
        private readonly DatabaseContext db;
        private readonly IWebHostEnvironment HostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public FacilitieService(DatabaseContext db, IWebHostEnvironment HostEnvironment, IHttpContextAccessor httpContextAccessor)
        {

            this.db = db;
            this.HostEnvironment = HostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<bool> DeleteFaciliti(int Id)
        {
            var Faci = await db.Facilities.SingleOrDefaultAsync(f => f.Id == Id);
            if (Faci != null)
            {
                db.Facilities.Remove(Faci);
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Facilities>> GetAll()
        {
            return await db.Facilities.ToListAsync();
        }

        public async Task<Facilities> store(FacilitieImage facilitieImage)
        {
            var Facilitie = new Facilities
            {
                Title = facilitieImage.Title,
                Description = facilitieImage.Desciption,
                Image = await SaveImage(facilitieImage.Image),
            };
            db.Facilities.Add(Facilitie);
            await db.SaveChangesAsync();
          
            return Facilitie;
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
                    transaction.Commit(); // Commit the transaction if everything is successful
                    return facilitie;
                }
                catch (Exception)
                {
                    transaction.Rollback(); // Rollback the transaction if an exception occurs
                    throw; // Re-throw the exception to handle it at a higher level
                }
            }
        }
        [NonAction]
        public async Task<string> SaveImage(IFormFile formFile)
        {
            string imageName = new string(Path.GetFileNameWithoutExtension(formFile.FileName).Take(10).ToArray()).Replace(" ", "_");
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(formFile.FileName);

            // Thêm tiền tố "Image/Article/" vào imageName
            string imageNameWithPath = "Image/Facilitie/" + imageName;

            // Cập nhật đường dẫn lưu trữ ảnh
            var imagePath = Path.Combine(HostEnvironment.ContentRootPath, "Image", "Facilitie", imageName);

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

        public async Task<bool> checktitle(string title)
        {
            return await db.Facilities.AnyAsync(a => a.Title == title);
        }

        public async Task<bool> checkUpdate(string title, int id)
        {
            return await db.Facilities.AnyAsync(a => a.Title == title && a.Id != id);
        }

      
    } 
}
