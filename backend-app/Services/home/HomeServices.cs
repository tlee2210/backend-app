using backend_app.DTO;
using backend_app.IRepository.home;
using backend_app.Models;
using Microsoft.EntityFrameworkCore;

namespace backend_app.Services.home
{
    public class HomeServices : IHome
    {
        private readonly DatabaseContext db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HomeServices(DatabaseContext db, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            _httpContextAccessor=httpContextAccessor;
        }

        public async Task<HomeDTO<Faculty, Article>> GetAll()
        {
            var request = _httpContextAccessor.HttpContext.Request;

            var facultyList = await db.Faculty.Take(4).ToListAsync();
            var articleList = await db.Articles.Take(4).ToListAsync();

            facultyList.ForEach(faculty =>
                faculty.Image = string.Format("{0}://{1}{2}/{3}", request.Scheme, request.Host, request.PathBase, faculty.Image));

            articleList.ForEach(article =>
                article.image = string.Format("{0}://{1}{2}/{3}", request.Scheme, request.Host, request.PathBase, article.image));

            var homeDTO = new HomeDTO<Faculty, Article>
            {
                data = facultyList,
                data2 = articleList,
            };

            return homeDTO;
        }

    }
}
