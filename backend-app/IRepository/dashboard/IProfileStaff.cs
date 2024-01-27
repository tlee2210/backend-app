using backend_app.DTO;
using backend_app.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace backend_app.IRepository.dashboard
{
    public interface IProfileStaff
    {
        Task<Staff> GetAuth(ClaimsPrincipal user);
        Task<bool> ChangePassword(ClaimsPrincipal user, ChangePasswordDTO changePassword);
    }
}
