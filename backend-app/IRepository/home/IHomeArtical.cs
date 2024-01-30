using backend_app.DTO;
using backend_app.Models;

namespace backend_app.IRepository.home
{
    public interface IHomeArtical
    {
        Task<HomeDTO<ArticleDTO, Category>> GetList();
        Task<DetailsWithRelatedDTO<ArticleDTO, ArticleDTO>> GetDetail(int id);
        Task<IEnumerable<ArticleDTO>> Search(string searchTerm);
        Task<IEnumerable<ArticleDTO>> SearchByCategory(int categoryId);
    }
}
