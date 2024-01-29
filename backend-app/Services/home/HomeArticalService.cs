using backend_app.IRepository.home;
using backend_app.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Text;

namespace backend_app.Services.home
{
    public class HomeArticalService : IHomeArtical
    {
        private readonly DatabaseContext db;
        public HomeArticalService(DatabaseContext db)
        {
            this.db = db;
        }
        public async  Task<IEnumerable<Article>> GetList()
        {
            return await db.Articles.Include(c=>c.ArticleCategories).ToListAsync();
          
        }
    }
}
