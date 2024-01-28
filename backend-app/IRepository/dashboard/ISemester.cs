using backend_app.DTO;

namespace backend_app.IRepository.dashboard
{
    public interface ISemester
    {
        Task<AllSelectOptionsDTO> GetCteateSemester(); 
    }
}
