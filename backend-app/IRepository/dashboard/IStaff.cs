using backend_app.DTO;
using backend_app.Models;

namespace backend_app.IRepository.dashboard
{
    public interface IStaff
    {
        Task<IEnumerable<StaffDTO>> GetAllStaffs();
        Task<StaffDTO> GetEdit(int id);
        Task<Staff> AddStaff(StaffImage staffimage);
        Task<Staff> UpdateStaff(StaffImage staffImage);
        Task<bool> DeleteStaff(int id);
    }
}
