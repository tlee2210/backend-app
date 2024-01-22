using backend_app.DTO;
using backend_app.IRepository.dashboard;
using backend_app.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Org.BouncyCastle.Security;
using System.Data;

namespace backend_app.Services.dashboard
{
    public class StaffServices : IStaff
    {
        private readonly DatabaseContext db;
        private readonly IWebHostEnvironment HostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StaffServices(DatabaseContext db, IWebHostEnvironment hostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            this.db=db;
            HostEnvironment=hostEnvironment;
            _httpContextAccessor=httpContextAccessor;
        }

        public async Task<Staff> AddStaff(StaffImage staffimage)
        {
            var staffimg = new Staff
            {
                Id = staffimage.Id,
                Address = staffimage.Address,
                Email = staffimage.Email,
                Experience = staffimage.Experience,
                FirstName = staffimage.FirstName,
                LastName = staffimage.LastName,
                Gender = staffimage.Gender,
                Password = BCrypt.Net.BCrypt.HashPassword(staffimage.Password),
                Phone = staffimage.Phone,
                Qualification = staffimage.Qualification,
                Role = "Teacher",
                FileAvatar = await SaveImage(staffimage.FileAvatar),
            };
            db.Staffs.Add(staffimg);
            await db.SaveChangesAsync();
            return staffimg;
        }

        public async Task<int?> DeleteStaff(int id)
        {
            var staf = await db.Staffs.SingleOrDefaultAsync(d => d.Id == id);
            if (staf != null)
            {
                var imagePath = Path.Combine(staf.FileAvatar);
                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                }
                db.Staffs.Remove(staf);
                await db.SaveChangesAsync();
                return staf.Id;
            }
            return null;
        }

        public async Task<IEnumerable<StaffDTO>> GetAllStaffs()
        {
            var request = _httpContextAccessor.HttpContext.Request;
            return await db.Staffs.Select(a => new StaffDTO
            {
                Id = a.Id,
                Address = a.Address,
                Email = a.Email,
                FirstName = a.FirstName,
                LastName = a.LastName,
                Gender = a.Gender,
                Experience = a.Experience,
                Phone = a.Phone,
                Qualification = a.Qualification,
                FileAvatar = string.Format("{0}://{1}{2}/{3}", request.Scheme, request.Host, request.PathBase, a.FileAvatar)
            }).ToListAsync();
        }

        public async Task<Staff> UpdateStaff(StaffImage staffImage)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var staff = await db.Staffs.SingleOrDefaultAsync(s => s.Id == staffImage.Id);
                    if (staff == null)
                    {
                        return null;
                    }

                    staff.Address = staffImage.Address;
                    staff.Email = staffImage.Email;
                    staff.FirstName = staffImage.FirstName;
                    staff.LastName = staffImage.LastName;
                    staff.Gender = staffImage.Gender;
                    staff.Experience = staffImage.Experience;
                    staff.Phone = staffImage.Phone;
                    staff.Qualification = staffImage.Qualification;

                    if (staffImage.FileAvatar != null)
                    {
                        if (!string.IsNullOrEmpty(staff.FileAvatar))
                        {
                            var imagePath = Path.Combine(staff.FileAvatar);
                            if (File.Exists(imagePath))
                            {
                                File.Delete(imagePath);
                            }
                        }
                        staff.FileAvatar = await SaveImage(staffImage.FileAvatar);
                    }

                    await db.SaveChangesAsync();
                    transaction.Commit();
                    return staff;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw; 
                }
            }
        }
        public async Task<StaffDTO> GetEdit(int id)
        {
            var staf = await db.Staffs.SingleOrDefaultAsync(a => a.Id == id);
            if (staf == null)
            {
                return null;
            }
            var stafDTO = new StaffDTO 
            {
                Id = staf.Id,
                Address = staf.Address,
                Email = staf.Email,
                Experience = staf.Experience,
                Qualification = staf.Qualification,
                FirstName = staf.FirstName,
                LastName = staf.LastName,
                Gender = staf.Gender,
                Phone = staf.Phone
            };
            var request = _httpContextAccessor.HttpContext.Request;
            stafDTO.FileAvatar = string.Format("{0}://{1}{2}/{3}", request.Scheme, request.Host, request.PathBase, staf.FileAvatar);
            return stafDTO;
        }

        [NonAction]
        public async Task<string> SaveImage(IFormFile formFile)
        {
            string imageName = new string(Path.GetFileNameWithoutExtension(formFile.FileName).Take(10).ToArray()).Replace(" ", "_");
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(formFile.FileName);

            // Thêm tiền tố "Image/Article/" vào imageName
            string imageNameWithPath = "Image/Staff/" + imageName;

            // Cập nhật đường dẫn lưu trữ ảnh
            var imagePath = Path.Combine(HostEnvironment.ContentRootPath, "Image", "Staff", imageName);

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
