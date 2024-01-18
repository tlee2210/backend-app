using backend_app.IRepository.dashboard;
using backend_app.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace backend_app.Services.dashboard
{
    public class CoursesService : ICourses
    {

        private readonly DatabaseContext db;
        public CoursesService(DatabaseContext db)
        {
            this.db = db;
        }
        public async Task<Courses> AddCourses(Courses courses)
        {
            var course = await GetOneCourse(courses.Id);
            if (course == null)
            {
                courses.Slug = GenerateSlug(courses.Name);
                db.Courses.Add(courses);
                await db.SaveChangesAsync();
                return courses;
            }
            return null;
        }
        private string GenerateSlug(string phrase)
        {
            string str = phrase.ToLowerInvariant();

            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens   

            return str;
        }
        public async Task<Courses> DeleteCourses(int id)
        {
            var course = await GetOneCourse(id);
            if (course != null)
            {
                db.Courses.Remove(course);
                await db.SaveChangesAsync();
                return course;
            }
            return null;
        }

        public async Task<IEnumerable<Courses>> GetAllCourses()
        {
            return await db.Courses.ToListAsync();
        }

        public async Task<Courses> GetOneCourse(int id)
        {
            return await db.Courses.SingleOrDefaultAsync(c => c.Id == id);
        }
        public async Task<Courses> UpdateCourses(Courses courses)
        {
            var course = await db.Courses.FindAsync(courses.Id);
            if (course != null)
            {
                course.Name = courses.Name;
                course.Slug = GenerateSlug(courses.Name);
                course.Description = courses.Description;
                await db.SaveChangesAsync();
                return courses;
            }
            return null;
        }
    }
}
