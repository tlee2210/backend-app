using backend_app.DTO;
using backend_app.Models;

namespace backend_app.IRepository.home
{
    public interface IHome
    {
         Task<HomeDTO<Faculty, Article>> GetAll();
    }
}
