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
                var studentCode = await GenerateStudentCode();
                var stu = new Students
                {
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
                    StudentCode = studentCode,
                };
                db.Students.Add(stu);
                await db.SaveChangesAsync();

                var currentSessionId = await db.Sessions
                    .Where(c => c.IsCurrentYear)
                    .Select(s => s.Id)
                    .FirstOrDefaultAsync();

                var stufas = new StudentFacultySemesters
                {
                    StudentId = stu.Id,
                    FacultyId = student.FacultyId,
                    SessionId = currentSessionId
                };

                db.StudentFacultySemesters.Add(stufas);
                await db.SaveChangesAsync();

                return stu;
            }
            return null;
        }
        [NonAction]
        public async Task<string> GenerateStudentCode()
        {
            var randomNumber = new Random();
            while (true)
            {
                var number = randomNumber.Next(100000, 999999);
                var studentCode = $"Student{number}";

                var existingStudent = await db.Students
                    .SingleOrDefaultAsync(s => s.StudentCode == studentCode);

                if (existingStudent == null)
                {
                    return studentCode;
                }
                await Task.Delay(10);
            }
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
            return await db.Students
                .Include(a => a.StudentFacultySemesters)
                .ThenInclude(sfs => sfs.Session)
                .Include(a => a.StudentFacultySemesters)
                .ThenInclude(sfs => sfs.Faculty)
                .Include(a => a.StudentFacultySemesters)
                .ThenInclude(sfs => sfs.Semester).Select(a => new StudentDTO
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
                Avatar = string.Format("{0}://{1}{2}/{3}", request.Scheme, request.Host, request.PathBase, a.Avatar),
                StudentFacultySemesters = a.StudentFacultySemesters,
            }).ToListAsync();
        }

        public async Task<GetEditSelectOption<StudentDTO>> GetEdit(int id)
        {
            var stu = await db.Students
                .Include(a => a.StudentFacultySemesters)
                .ThenInclude(sfs => sfs.Session)
                .Include(a => a.StudentFacultySemesters)
                .ThenInclude(sfs => sfs.Faculty)
                .Include(a => a.StudentFacultySemesters)
                .ThenInclude(sfs => sfs.Semester)
                .SingleOrDefaultAsync(a => a.Id == id);
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
                StudentFacultySemesters = stu.StudentFacultySemesters,
            };
            stuDTO.Avatar = string.Format("{0}://{1}{2}/{3}", request.Scheme, request.Host, request.PathBase, stu.Avatar);
            var options = await db.Faculty
                .Select(x => new SelectOption
                {
                    label = x.Title,
                    value = x.Id
                })
                .ToListAsync();
            var EditDTO = new GetEditSelectOption<StudentDTO>
            {
                model = stuDTO,
                //articleDTO = articleDto,
                SelectOption = options
            };

            return EditDTO;
        }
        public async Task<Students> UpdateStudent(StudentImage student)
        {
            using (var transaction = await db.Database.BeginTransactionAsync())
            {
                try
                {
                    var stu = await db.Students.SingleOrDefaultAsync(a => a.Id == student.Id);
                    if (stu == null)
                    {
                        await transaction.RollbackAsync();
                        return null;
                    }

                    stu.Address = student.Address;
                    stu.Email = student.Email;
                    stu.FirstName = student.FirstName;
                    stu.LastName = student.LastName;
                    stu.Gender = student.Gender;
                    stu.Phone = student.Phone;
                    stu.DateOfBirth = student.DateOfBirth;
                    stu.FatherName = student.FatherName;
                    stu.MotherName = student.MotherName;

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

                    await transaction.CommitAsync();

                    return stu;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
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
