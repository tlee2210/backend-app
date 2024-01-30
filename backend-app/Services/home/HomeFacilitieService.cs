using backend_app.DTO;
using backend_app.IRepository.home;
using backend_app.Models;
using Microsoft.EntityFrameworkCore;

namespace backend_app.Services.home
{
    public class HomeFacilitieService : IHomeFacilities
    {
        private readonly DatabaseContext db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HomeFacilitieService(DatabaseContext db, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            this._httpContextAccessor = httpContextAccessor;
        }
        public async Task<IEnumerable<Facilities>> GetList()
        {
            var request = _httpContextAccessor.HttpContext.Request;
            return await db.Facilities.Select(x => new Facilities
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Image = string.Format("{0}://{1}{2}/{3}", request.Scheme, request.Host, request.PathBase, x.Image),
            }).ToListAsync();
        }
    }
}
