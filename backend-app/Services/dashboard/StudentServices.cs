using backend_app.DTO;
using backend_app.IRepository.dashboard;
using backend_app.Models;
using Microsoft.EntityFrameworkCore;

namespace backend_app.Services.dashboard
{
    public class StudentServices : IStudent
    {
        private readonly DatabaseContext db;
        public StudentServices(DatabaseContext db)
        {
            this.db = db;
        }
        public async Task<Students> AddStudent(Students student)
        {
            var stu = await db.Students.SingleOrDefaultAsync(s => s.Id == student.Id);
            if (stu == null)
            {
                student.Password = BCrypt.Net.BCrypt.HashPassword(student.Password);
                db.Students.Add(student);
                await db.SaveChangesAsync();
                return student;
            }
            return null;
        }

        public async Task<bool> DeleteStudent(int id)
        {
            var stu = await db.Students.SingleOrDefaultAsync(s => s.Id == id);
            if (stu != null)
            {
                db.Students.Remove(stu);
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<StudentDTO>> GetAllStudents()
        {
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
                StudentFacultySemesters = a.StudentFacultySemesters,
                StudentFacultySemestersId = a.StudentFacultySemestersId
            }).ToListAsync();
        }

        public async Task<StudentDTO> GetEdit(int id)
        {
            var stu = await db.Students.SingleOrDefaultAsync(a => a.Id == id);
            if (stu == null)
            {
                return null;
            }
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
                StudentFacultySemestersId = stu.StudentFacultySemestersId
            };
            return stuDTO;
        }

        public async Task<Students> UpdateStudent(Students student)
        {
            db.Entry(student).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return student;
        }
    }
}
