using backend_app.DTO;
using backend_app.IRepository.dashboard;
using backend_app.IRepository.home;
using backend_app.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Text;
using System.Linq;

namespace backend_app.Services.home
{
    public class HomeArticalService : IHomeArtical
    {
        private readonly DatabaseContext db;

        private readonly IHttpContextAccessor _httpContextAccessor;
        public HomeArticalService(DatabaseContext db, IHttpContextAccessor _httpContextAccessor)
        {
            this.db = db;
            this._httpContextAccessor = _httpContextAccessor;
        }

        public async Task<DetailsWithRelatedDTO<Article, ArticleDTO>> GetDetail(int id)
        {
            var articleDetail = await db.Articles.Include(a => a.ArticleCategories).ThenInclude(ac => ac.Category).SingleOrDefaultAsync(a => a.Id == id);
            var articles = await db.Articles.Where(a => a.Id != articleDetail.Id).Include(a => a.ArticleCategories).ThenInclude(ac => ac.Category).ToListAsync();
            var request = _httpContextAccessor.HttpContext.Request;
            var newListArticle = articles.Select(a => new ArticleDTO
            {
                Id = a.Id,
                Title = a.Title,
                Content = a.Content,
                image = string.Format("{0}://{1}{2}/{3}", request.Scheme, request.Host, request.PathBase, a.image),

                PublishDate = a.PublishDate,
                Categories = a.ArticleCategories.Select(ac => new CategoryDTO
                {
                    Id = ac.Category.Id,
                    Name = ac.Category.Name
                }).ToList()
            }).ToList();
            var detailAndRelated = new DetailsWithRelatedDTO<Article, ArticleDTO>
            {
                data = articleDetail,
                listData = newListArticle,

            };
            return detailAndRelated;
        }

        public async Task<IEnumerable<ArticleDTO>> GetList()
        {
            var articles = await db.Articles
                .Include(a => a.ArticleCategories)
                .ThenInclude(ac => ac.Category)
                .ToListAsync();

            var request = _httpContextAccessor.HttpContext.Request;

            return articles.Select(a => new ArticleDTO
            {
                Id = a.Id,
                Title = a.Title,
                Content = TruncateContent(a.Content), // Use the new method here
                image = string.Format("{0}://{1}{2}/{3}", request.Scheme, request.Host, request.PathBase, a.image),
                PublishDate = a.PublishDate,
                Categories = a.ArticleCategories.Select(ac => new CategoryDTO
                {
                    Id = ac.Category.Id,
                    Name = ac.Category.Name
                }).ToList()
            }).ToList(); // Don't forget to call ToList() if you want to return a List
        }

        [NonAction]
        private string TruncateContent(string content)
        {
            int indexOfSecondPeriod = IndexOfNth(content, '.', 2);
            return indexOfSecondPeriod != -1 ? content.Substring(0, indexOfSecondPeriod + 1) : content;
        }
        [NonAction]
        private int IndexOfNth(string str, char c, int n)
        {
            int s = -1;
            for (int i = 0; i < n; i++)
            {
                s = str.IndexOf(c, s + 1);
                if (s == -1) break;
            }
            return s;
        }
    }
}
