using backend_app.DTO;
using backend_app.Models;

namespace backend_app.IRepository.home
{
    public interface ICoursesHome
    {
        Task<IEnumerable<Courses>> GetAllCourses();
        Task<DetailsWithRelatedDTO<Courses, homeFacultyDTO>> GetFacultyByCourseSlug(string courseSlug);
        Task<FacultyDetailsDTO> GetFacultyDetails(string facultySlug);
        Task<IEnumerable<homeFacultyDTO>> SearchFacultyByTitle(string Title);
    }
}
