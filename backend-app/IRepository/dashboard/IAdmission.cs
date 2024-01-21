using backend_app.Models;

namespace backend_app.IRepository.dashboard
{
    public interface IAdmission
    {
        Task<IEnumerable<Admission>> GetAllAdmissions();
        Task<Admission> GetOneAdmission(int id);
        Task<Admission> AcceptAdmission(int id);
        Task<Admission> RejectAdmission(int id);
        Task<Admission> DeleteAdmission(int id);
    }
}
