using backend_app.DTO;
using backend_app.IRepository.dashboard;
using backend_app.IRepository.home;
using backend_app.Models;
using Microsoft.AspNetCore.Mvc;
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
            {
                faculty.Image = string.Format("{0}://{1}{2}/{3}", request.Scheme, request.Host, request.PathBase, faculty.Image);
                int indexOfSecondPeriod = IndexOfNth(faculty.Description, '.', 2);
                if (indexOfSecondPeriod != -1)
                {
                    faculty.Description = faculty.Description.Substring(0, indexOfSecondPeriod + 1);
                }
            });

            articleList.ForEach(article =>
            {
                article.image = string.Format("{0}://{1}{2}/{3}", request.Scheme, request.Host, request.PathBase, article.image);

                int indexOfSecondPeriod = IndexOfNth(article.Content, '.', 2);
                if (indexOfSecondPeriod != -1)
                {
                    article.Content = article.Content.Substring(0, indexOfSecondPeriod + 1);
                }
            });
            var options = await db.Faculty
                .Select(x => new SelectOption
                {
                    label = x.Title,
                    value = x.Id
                })
                .ToListAsync();

            var homeDTO = new HomeDTO<Faculty, Article>
            {
                data = facultyList,
                data2 = articleList,
                SelectOption = options
            };

            return homeDTO;
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
