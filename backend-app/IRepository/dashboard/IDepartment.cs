using backend_app.Models;

namespace backend_app.IRepository.dashboard
{
    public interface IDepartment
    {
        Task<IEnumerable<Department>> GetAllDepartments();
        Task<Department> GetOneDepartment(int id);
        Task<Department> AddDepartment(Department department);
        Task<Department> UpdateDepartment(Department department);
        Task<Department> DeleteDepartment(int id);
    }
}
