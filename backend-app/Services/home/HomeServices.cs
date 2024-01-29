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
            var faculty = await db.Faculty.ToListAsync();
            var article = await db.Articles.ToListAsync();
            var home = new HomeDTO<Faculty, Article>
            {
                data = await db.Faculty.ToListAsync(),
                data2 = await db.Articles.ToListAsync(),
            };

            return home;
        }
    }
}
