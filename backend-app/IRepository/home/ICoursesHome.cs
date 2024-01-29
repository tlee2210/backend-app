using backend_app.Models;

namespace backend_app.IRepository.home
{
    public interface ICoursesHome
    {
        Task<IEnumerable<Courses>> GetAllCourses();

    }
}
