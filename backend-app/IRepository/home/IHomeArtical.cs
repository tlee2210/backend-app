using backend_app.DTO;
using backend_app.Models;

namespace backend_app.IRepository.home
{
    public interface IHomeArtical
    {
        Task<IEnumerable<Article>> GetList();
        Task<DetailsWithRelatedDTO<Article, ArticleDTO>> GetDetail(int id);
    }
}
