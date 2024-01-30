using backend_app.DTO;
using backend_app.IRepository.dashboard;
using backend_app.IRepository.home;
using backend_app.Models;
using Microsoft.EntityFrameworkCore;

namespace backend_app.Services.home
{
    public class CoursesHomeServices : ICoursesHome
    {
        private readonly DatabaseContext db;
        public CoursesHomeServices(DatabaseContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<Courses>> GetAllCourses()
        {
            return await db.Courses.ToListAsync();
        }

        public async Task<List<homeFacultyDTO>> GetFacultyByCourseSlug(string courseSlug)
        {
            var facultyOptions = await db.Courses
                .Where(c => c.Slug == courseSlug)
                .SelectMany(c => c.Faculty) 
                .Select(f => new homeFacultyDTO
                {
                    Title = f.Title,
                    Code = f.Code,
                    Slug = f.Slug
                })
                .Distinct() 
                .ToListAsync();

            return facultyOptions;
        }

        public async Task<IEnumerable<homeFacultyDTO>> SearchFacultyByTitle(string Title)
        {
            var faculties = await db.Faculty
                .Where(f => EF.Functions.Like(f.Title, $"%{Title}%"))
                .Select(f => new homeFacultyDTO
                {
                    Title = f.Title,
                    Code = f.Code,
                    Slug = f.Slug
                })
                .Distinct()
                .ToListAsync();

            return faculties;
        }


    }
}
