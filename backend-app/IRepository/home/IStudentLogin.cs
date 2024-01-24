using backend_app.DTO;

namespace backend_app.IRepository.home
{
    public interface IStudentLogin
    {
        Task<StudentLoginResult> Login(EmailLogin studentLogin);
    }
}
