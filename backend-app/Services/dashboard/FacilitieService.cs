using backend_app.IRepository.dashboard;
using backend_app.Models;
using Microsoft.EntityFrameworkCore;

namespace backend_app.Services.dashboard
{
    public class FacilitieService : IFacilitie
    {
        private readonly DatabaseContext db;
        public FacilitieService(DatabaseContext db)
        {
            this.db = db;
        }
        public async Task<bool> AddFaciliti(Facilities faci)
        {
            if (faci != null)
            {
                db.Facilities.Add(faci);
                var result = await db.SaveChangesAsync();
                if (result == 1)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public async Task<bool> DeleteFaciliti(int Id)
        {
            var Faci = await db.Facilities.SingleOrDefaultAsync(f => f.Id == Id);
            if (Faci != null)
            {
                db.Facilities.Remove(Faci);
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Facilities>> GetAll()
        {
            return await db.Facilities.ToListAsync();
        }

        public async Task<bool> UpdateFaciliti(Facilities faci)
        {
            var faciliti = await db.Facilities.SingleOrDefaultAsync(f => f.Id == faci.Id);
            if (faciliti != null)
            {
                faciliti.Title = faci.Title;
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
