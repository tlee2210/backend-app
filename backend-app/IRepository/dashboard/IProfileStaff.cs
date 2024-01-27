using backend_app.Models;
using System.Security.Claims;

namespace backend_app.IRepository.dashboard
{
    public interface IProfileStaff
    {
        Task<Staff> GetAuth(ClaimsPrincipal user);
    }
}
