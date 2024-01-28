using backend_app.DTO;
using backend_app.IRepository.dashboard;
using backend_app.Models;
using Microsoft.EntityFrameworkCore;

namespace backend_app.Services.dashboard
{
    public class SemesterServices : ISemester
    {
        private readonly DatabaseContext db;

        public SemesterServices(DatabaseContext db)
        {
            this.db = db;
        }
        public async Task<AllSelectOptionsDTO> GetCteateSemester()
        {
            var currentDate = DateTime.Now;
            var allOptions = new AllSelectOptionsDTO
            {
                FacultyOptions = await db.Faculty
                .Select(f => new SelectOption
                {
                    value = f.Id,
                    label = f.Title
                })
                .ToListAsync(),

                DepartmentOptions = await db.Departments
                .Select(d => new SelectOption
                {
                    value = d.Id,
                    label = d.Subject
                })
                .ToListAsync(),

                SessionOptions = await db.Sessions
                .Where(s => s.YearStart > currentDate)
                .Select(s => new SelectOption
                {
                    value = s.Id,
                    label = s.YearStart.ToString("MM-dd-yyyy")
                })
                .ToListAsync(),

                SemesterOptions = await db.semesters
                .Select(s => new SelectOption
                {
                    value = s.Id,
                    label = s.AcademicYear.ToString() + " year " +  s.SemesterNumber + " Semester"
                })
                .ToListAsync()
            };

            return allOptions;
        }
        public async Task<AllSelectOptionsDTO> GetParameters()
        {
            var allOptions = new AllSelectOptionsDTO
            {
                FacultyOptions = await db.Faculty
                .Select(f => new SelectOption
                {
                    value = f.Id,
                    label = f.Title
                })
                .ToListAsync(),

                DepartmentOptions = await db.Departments
                .Select(d => new SelectOption
                {
                    value = d.Id,
                    label = d.Subject
                })
                .ToListAsync(),

                SessionOptions = await db.Sessions
                .Select(s => new SelectOption
                {
                    value = s.Id,
                    label = s.YearStart.ToString("yyyy")
                })
                .ToListAsync(),

                SemesterOptions = await db.semesters
                .Select(s => new SelectOption
                {
                    value = s.Id,
                    label = s.AcademicYear.ToString() + " year " + s.SemesterNumber + " Semester"
                })
                .ToListAsync()
            };

            return allOptions;
        }
        public async Task<DepartmentSemesterSession> Store(DepartmentSemesterSession departmentSemesterSession)
        {
            db.departmentSemesterSessions.Add(departmentSemesterSession);
            await db.SaveChangesAsync();
            return departmentSemesterSession;
        }

        public async Task<bool> Exists(int facultyId, int sessionId, int departmentId)
        {
            return await db.departmentSemesterSessions
                .AnyAsync(dss =>
                dss.FacultyId == facultyId &&
                dss.SessionId == sessionId &&
                dss.DepartmentId == departmentId
                );
        }
        public async Task<IQueryable<DepartmentSemesterSession>> Search(SearchParameters searchParameters)
        {
            IQueryable<DepartmentSemesterSession> query = db.departmentSemesterSessions.Include(d => d.Department);

            if (searchParameters.DepartmentId.HasValue)
            {
                query = query.Where(dss => dss.DepartmentId == searchParameters.DepartmentId.Value);
            }

            if (searchParameters.SemesterId.HasValue)
            {
                query = query.Where(dss => dss.SemesterId == searchParameters.SemesterId.Value);
            }

            if (searchParameters.SessionId.HasValue)
            {
                query = query.Where(dss => dss.SessionId == searchParameters.SessionId.Value);
            }

            if (searchParameters.FacultyId.HasValue)
            {
                query = query.Where(dss => dss.FacultyId == searchParameters.FacultyId.Value);
            }
            return query;
        }


    }
}
