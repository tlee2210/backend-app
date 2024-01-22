using backend_app.DTO;
using backend_app.Models;

namespace backend_app.IRepository.dashboard
{
    public interface IStudent
    {
        Task<IEnumerable<StudentDTO>> GetAllStudents();
        Task<StudentDTO> GetEdit(int id);
        Task<Students> AddStudent(StudentImage student);
        Task<Students> UpdateStudent(StudentImage student);
        Task<bool> DeleteStudent(int id);
        Task<IEnumerable<SelectOption>> GetCreate();
    }
}
