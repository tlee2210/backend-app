using backend_app.DTO;
using backend_app.Models;

namespace backend_app.IRepository.home
{
    public interface ICoursesHome
    {
        Task<IEnumerable<Courses>> GetAllCourses();
        Task<List<homeFacultyDTO>> GetFacultyByCourseSlug(string courseSlug);
        Task<IEnumerable<homeFacultyDTO>> SearchFacultyByTitle(string Title);
        Task<Faculty> GetFacultyDetails(string facultySlug);
    }
}
