using backend_app.DTO;
using backend_app.IRepository.dashboard;
using backend_app.IRepository.home;
using backend_app.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace backend_app.Services.home
{
    public class CoursesHomeServices : ICoursesHome
    {
        private readonly DatabaseContext db;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CoursesHomeServices(DatabaseContext db, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            _httpContextAccessor = httpContextAccessor;
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

        public async Task<Faculty> GetFacultyDetails(string facultySlug)
        {
            var request = _httpContextAccessor.HttpContext.Request;

            var facultyDetails = await db.Faculty
               .Where(f => f.Slug == facultySlug)
               .Include(c => c.Courses)
               .FirstOrDefaultAsync();
            facultyDetails.Image = string.Format("{0}://{1}{2}/{3}", request.Scheme, request.Host, request.PathBase, facultyDetails.Image);

            return facultyDetails;
        }
    }
}
