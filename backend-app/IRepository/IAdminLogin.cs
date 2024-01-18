using backend_app.DTO;
using Microsoft.AspNetCore.Mvc;

namespace backend_app.IRepository
{
    public interface IAdminLogin
    {
        Task<LoginResult> Login(UserLogin userLogin);
    }
}
