using backend_app.Models;

namespace backend_app.IRepository.home
{
    public interface IAdmissionHome
    {
        Task<Admission> AddAdmission(Admission admission);
    }
}
