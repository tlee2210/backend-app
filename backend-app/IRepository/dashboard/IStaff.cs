using backend_app.Models;

namespace backend_app.IRepository.dashboard
{
    public interface IStaff
    {
        Task<IEnumerable<Staff>> GetAllStaffs();
        Task<Staff> GetOneStaff(int id);
        Task<Staff> AddStaff(Staff staff);
        Task<Staff> UpdateStaff(Staff staff);
        Task<Staff> DeleteStaff(int id);
    }
}
