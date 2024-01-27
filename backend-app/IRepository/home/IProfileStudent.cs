using backend_app.Models;
using System.Security.Claims;

namespace backend_app.IRepository.home
{
    public interface IProfileStudent
    {
        Task<Students> GetAuth(ClaimsPrincipal user);
    }
}
