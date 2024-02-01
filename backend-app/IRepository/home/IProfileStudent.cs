using backend_app.DTO;
using backend_app.Models;
using System.Security.Claims;

namespace backend_app.IRepository.home
{
    public interface IProfileStudent
    {
        Task<StudentprofileDTO> GetAuth(ClaimsPrincipal user);
        Task<bool> ChangePassword(ClaimsPrincipal user, ChangePasswordDTO changePassword);

    }
}
