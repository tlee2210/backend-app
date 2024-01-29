using backend_app.DTO;
using backend_app.IRepository.home;
using backend_app.Models;
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

        public async  Task<IEnumerable<Article>> GetList()
        {
            return await db.Articles.Include(c=>c.ArticleCategories).ToListAsync();
          
        }
    }
}
