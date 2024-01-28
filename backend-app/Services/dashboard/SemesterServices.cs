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
                    label = s.AcademicYear.ToString() + "year " +  s.SemesterNumber + " Semester"
                })
                .ToListAsync()
            };

            return allOptions;
        }
    }
}
