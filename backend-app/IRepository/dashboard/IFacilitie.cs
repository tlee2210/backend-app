using backend_app.Models;

namespace backend_app.IRepository.dashboard
{
    public interface IFacilitie
    {
        Task<bool> AddFaciliti(Facilities faci);
        Task<bool> UpdateFaciliti(Facilities faci);
        Task<IEnumerable<Facilities>> GetAll();
        Task<bool> DeleteFaciliti(int Id);
    }
}
