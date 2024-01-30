using backend_app.DTO;
using backend_app.IRepository.dashboard;
using backend_app.IRepository.home;
using backend_app.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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

        public async Task<DetailsWithRelatedDTO<ArticleDTO, ArticleDTO>> GetDetail(int id)
        {

            var request = _httpContextAccessor.HttpContext.Request;

            var articleDetailEntity = await db.Articles
                .Include(a => a.ArticleCategories)
                .ThenInclude(ac => ac.Category)
                .SingleOrDefaultAsync(a => a.Id == id);

            if (articleDetailEntity == null)
            {
                return null;
            }

            var articleDetail = new ArticleDTO
            {
                Id = articleDetailEntity.Id,
                Title = articleDetailEntity.Title,
                Content = articleDetailEntity.Content,
                image = string.Format("{0}://{1}{2}/{3}", request.Scheme, request.Host, request.PathBase, articleDetailEntity.image),
                PublishDate = articleDetailEntity.PublishDate,
                Categories = articleDetailEntity.ArticleCategories.Select(ac => new CategoryDTO
                {
                    Id = ac.Category.Id,
                    Name = ac.Category.Name
                }).ToList()
            };

            var categoryIds = articleDetailEntity.ArticleCategories.Select(ac => ac.CategoryId).ToList();

            var articles = await db.Articles
                .Where(a => a.Id != articleDetail.Id)
                .Include(a => a.ArticleCategories)
                .ThenInclude(ac => ac.Category)
                .Where(a => a.ArticleCategories.Any(ac => categoryIds.Contains(ac.CategoryId)))
                .Take(4)
                .ToListAsync();

              var newListArticle = articles.Select(a => new ArticleDTO
            {
                Id = a.Id,
                Title = a.Title,
                Content = TruncateContent(a.Content),
                image = string.Format("{0}://{1}{2}/{3}", request.Scheme, request.Host, request.PathBase, a.image),
                PublishDate = a.PublishDate,
                Categories = a.ArticleCategories.Select(ac => new CategoryDTO
                {
                    Id = ac.Category.Id,
                    Name = ac.Category.Name
                }).ToList()
            }).ToList();

            var detailAndRelated = new DetailsWithRelatedDTO<ArticleDTO, ArticleDTO>
            {
                data = articleDetail,
                listData = newListArticle,
            };
            return detailAndRelated;
        }
        public async Task<HomeDTO<ArticleDTO, Category>> GetList()
        {
            var articles = await db.Articles
                .Include(a => a.ArticleCategories)
                .ThenInclude(ac => ac.Category)
                .ToListAsync();

            var request = _httpContextAccessor.HttpContext.Request;

            var newarticles =  articles.Select(a => new ArticleDTO
            {
                Id = a.Id,
                Title = a.Title,
                Content = TruncateContent(a.Content),
                image = string.Format("{0}://{1}{2}/{3}", request.Scheme, request.Host, request.PathBase, a.image),
                PublishDate = a.PublishDate,
                Categories = a.ArticleCategories.Select(ac => new CategoryDTO
                {
                    Id = ac.Category.Id,
                    Name = ac.Category.Name
                }).ToList()
            }).ToList();
            var Category = await db.Categories.ToListAsync();
            var homeDTO = new HomeDTO<ArticleDTO, Category>
            {
                data = newarticles,
                data2 = Category,
            };

            return homeDTO;
        }
        public async Task<IEnumerable<ArticleDTO>> Search(string searchTerm)
        {
            var articles = await db.Articles
                .Include(a => a.ArticleCategories)
                .ThenInclude(ac => ac.Category)
                .Where(a => a.Title.Contains(searchTerm))
                .ToListAsync();

            var request = _httpContextAccessor.HttpContext.Request;

            return articles.Select(a => new ArticleDTO
            {
                Id = a.Id,
                Title = a.Title,
                Content = TruncateContent(a.Content),
                image = string.Format("{0}://{1}{2}/{3}", request.Scheme, request.Host, request.PathBase, a.image),
                PublishDate = a.PublishDate,
                Categories = a.ArticleCategories.Select(ac => new CategoryDTO
                {
                    Id = ac.Category.Id,
                    Name = ac.Category.Name
                }).ToList()
            }).ToList();
        }
        public async Task<IEnumerable<ArticleDTO>> SearchByCategory(int categoryId)
        {
            var articles = await db.Articles
                .Include(a => a.ArticleCategories)
                .ThenInclude(ac => ac.Category)
                .Where(a => a.ArticleCategories.Any(ac => ac.CategoryId == categoryId)) // Filtering based on CategoryId
                .ToListAsync();

            var request = _httpContextAccessor.HttpContext.Request;

            return articles.Select(a => new ArticleDTO
            {
                Id = a.Id,
                Title = a.Title,
                Content = TruncateContent(a.Content),
                image = string.Format("{0}://{1}{2}/{3}", request.Scheme, request.Host, request.PathBase, a.image),
                PublishDate = a.PublishDate,
                Categories = a.ArticleCategories.Select(ac => new CategoryDTO
                {
                    Id = ac.Category.Id,
                    Name = ac.Category.Name
                }).ToList()
            }).ToList();
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
