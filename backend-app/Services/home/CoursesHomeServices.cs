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

        public async Task<FacultyDetailsDTO> GetFacultyDetails(string facultySlug)
        {
            var request = _httpContextAccessor.HttpContext.Request;

            // Fetch faculty details
            var facultyDetails = await db.Faculty
               .Where(f => f.Slug == facultySlug)
               .Include(c => c.Courses)
               .FirstOrDefaultAsync();

            facultyDetails.Image = $"{request.Scheme}://{request.Host}{request.PathBase}/{facultyDetails.Image}";

            var currentSessionId = await db.Sessions
                    .Where(s => s.IsCurrentYear)
                    .Select(s => s.Id)
                    .FirstOrDefaultAsync();
            if(currentSessionId == null)
            {
                return null;
            }

            IQueryable<DepartmentSemesterSession> query = db.departmentSemesterSessions
                .Include(d => d.Department)
                .Include(s => s.Semester)
                .Include(a => a.session)
                .Where(dss => dss.FacultyId == facultyDetails.Id && dss.SessionId == currentSessionId);

            var groupedResults = await query.GroupBy(dss => dss.SemesterId).ToListAsync();

            var dividedResults = new List<List<DepartmentSemesterSession>>();

            for (int semesterIndex = 1; semesterIndex <= 8; semesterIndex++)
            {
                var semesterResults = groupedResults.SingleOrDefault(group => group.Key == semesterIndex);

                if (semesterResults != null)
                {
                    dividedResults.Add(semesterResults.ToList());
                }
                else
                {
                    dividedResults.Add(new List<DepartmentSemesterSession>());
                }
            }

            // Construct the result DTO
            var result = new FacultyDetailsDTO
            {
                Faculty = facultyDetails,
                DepartmentSemesterSessionsGrouped = dividedResults
            };

            return result;
        }

    }
}
