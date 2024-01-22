using backend_app.DTO;
using backend_app.IRepository.dashboard;
using backend_app.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Org.BouncyCastle.Asn1.X509;

namespace backend_app.Services.dashboard
{
    public class StudentServices : IStudent
    {
        private readonly DatabaseContext db;
        private readonly IWebHostEnvironment HostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StudentServices(DatabaseContext db, IWebHostEnvironment hostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            this.db=db;
            HostEnvironment=hostEnvironment;
            _httpContextAccessor=httpContextAccessor;
        }

        public async Task<Students> AddStudent(StudentImage student)
        {
            var studentOld = await db.Students.SingleOrDefaultAsync(s => s.Id == student.Id);
            if (studentOld == null)
            {
                var stu = new Students
                {
                    Id = student.Id,
                    Address = student.Address,
                    Avatar = await SaveImage(student.Avatar),
                    Password = BCrypt.Net.BCrypt.HashPassword(student.Password),
                    DateOfBirth = student.DateOfBirth,  
                    Email = student.Email,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    FatherName = student.FatherName,
                    Gender = student.Gender,
                    MotherName = student.MotherName,
                    Phone = student.Phone,
                    StudentCode = student.StudentCode
                };
                db.Students.Add(stu);
                await db.SaveChangesAsync();
                return stu;
            }
            return null;
        }

        public async Task<bool> DeleteStudent(int id)
        {
            var stu = await db.Students.SingleOrDefaultAsync(s => s.Id == id);
            if (stu != null)
            {
                var imagePath = Path.Combine(stu.Avatar);
                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                }
                db.Students.Remove(stu);
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<StudentDTO>> GetAllStudents()
        {
            var request = _httpContextAccessor.HttpContext.Request;
            return await db.Students.Select(a => new StudentDTO
            {
                Id = a.Id,
                Address = a.Address,
                Email = a.Email,
                FirstName = a.FirstName,
                LastName = a.LastName,
                Gender = a.Gender,
                Phone = a.Phone,
                DateOfBirth = a.DateOfBirth,
                FatherName = a.FatherName,
                MotherName = a.MotherName,
                StudentCode = a.StudentCode,
                Avatar = string.Format("{0}://{1}{2}/{3}", request.Scheme, request.Host, request.PathBase, a.Avatar)
            }).ToListAsync();
        }

        public async Task<StudentDTO> GetEdit(int id)
        {
            var stu = await db.Students.SingleOrDefaultAsync(a => a.Id == id);
            if (stu == null)
            {
                return null;
            }
            var request = _httpContextAccessor.HttpContext.Request;
            var stuDTO = new StudentDTO
            {
                Id = stu.Id,
                Address = stu.Address,
                Email = stu.Email,
                FirstName = stu.FirstName,
                LastName = stu.LastName,
                Gender = stu.Gender,
                Phone = stu.Phone,
                DateOfBirth = stu.DateOfBirth,
                FatherName =  stu.FatherName,
                MotherName = stu.MotherName,
                StudentCode = stu.StudentCode,
                /*StudentFacultySemesters = stu.StudentFacultySemesters,
                StudentFacultySemestersId = stu.StudentFacultySemestersId*/
            };
            stuDTO.Avatar = string.Format("{0}://{1}{2}/{3}", request.Scheme, request.Host, request.PathBase, stu.Avatar);

            return stuDTO;
        }

        public async Task<Students> UpdateStudent(StudentImage student)
        {
            var stu = await db.Students.SingleOrDefaultAsync(a => a.Id == student.Id);
            if (stu == null)
            {
                return null;
            }
            stu.Address = student.Address;
            stu.Email = student.Email;
            stu.FirstName = student.FirstName;
            stu.LastName = student.LastName;
            stu.Gender = student.Gender;
            stu.Phone = student.Phone;
            stu.DateOfBirth = student.DateOfBirth;
            stu.FatherName =  student.FatherName;
            stu.MotherName = student.MotherName;
            stu.StudentCode = student.StudentCode;
            stu.Password = BCrypt.Net.BCrypt.HashPassword(student.Password);
            if (student.Avatar != null)
            {
                var imagePath = Path.Combine(stu.Avatar);
                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                }
                stu.Avatar = await SaveImage(student.Avatar);
            }
            await db.SaveChangesAsync();
            return stu;
        }

        public async Task<IEnumerable<SelectOption>> GetCreate()
        {
            var options = await db.Faculty
                .Select(x => new SelectOption
                {
                    label = x.Title,
                    value = x.Id
                })
                .ToListAsync();

            return options;
        }

        [NonAction]
        public async Task<string> SaveImage(IFormFile formFile)
        {
            string imageName = new string(Path.GetFileNameWithoutExtension(formFile.FileName).Take(10).ToArray()).Replace(" ", "_");
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(formFile.FileName);

            // Thêm tiền tố "Image/Article/" vào imageName
            string imageNameWithPath = "Image/Student/" + imageName;

            // Cập nhật đường dẫn lưu trữ ảnh
            var imagePath = Path.Combine(HostEnvironment.ContentRootPath, "Image", "Student", imageName);

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
