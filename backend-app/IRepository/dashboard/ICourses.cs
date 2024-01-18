using backend_app.Models;

namespace backend_app.IRepository.dashboard
{
    public interface ICourses
    {
        Task<IEnumerable<Courses>> GetAllCourses();
        Task<Courses> GetOneCourse(int id);
        Task<Courses> AddCourses(Courses courses);
        Task<Courses> UpdateCourses(Courses courses);
        Task<Courses> DeleteCourses(int id);
    }
}
