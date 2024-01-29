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
    }
}
