using backend_app.DTO;
using backend_app.Models;

namespace backend_app.IRepository.dashboard
{
    public interface IStudent
    {
        Task<IEnumerable<StudentDTO>> GetAllStudents();
        Task<StudentDTO> GetEdit(int id);
        Task<Students> AddStudent(Students student);
        Task<Students> UpdateStudent(Students student);
        Task<bool> DeleteStudent(int id);
    }
}
