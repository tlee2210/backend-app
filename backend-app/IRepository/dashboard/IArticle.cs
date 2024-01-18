using backend_app.DTO;
using backend_app.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend_app.IRepository.dashboard
{
    public interface IArticle
    {
        Task<IEnumerable<ArticleDTO>> GetAllarticle();
        Task<IEnumerable<SelectOption>> GetCreate();
        Task<Article> store(ArticleImage articleImage);
        Task<Article> UpdateArticle(ArticleImage articleImage);
        Task<GetEditSelectOption<ArticleDTO>> GetEditArticle(int id);
        Task<bool> DeleteArticle(int id);
        Task<bool> checktitle(string title);
        Task<bool> checkUpdate(string title, int id);
    }
}
