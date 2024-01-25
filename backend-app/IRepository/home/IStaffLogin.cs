using backend_app.DTO;

namespace backend_app.IRepository.home
{
    public interface IStaffLogin
    {
        Task<LoginResult> Login(EmailLogin staffLogin);
    }
}
