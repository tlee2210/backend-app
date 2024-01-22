using backend_app.IRepository.dashboard;
using backend_app.Models;
using Microsoft.EntityFrameworkCore;

namespace backend_app.Services.dashboard
{
    public class AdmissionServices : IAdmission
    {
        private readonly DatabaseContext db;
        public AdmissionServices(DatabaseContext db)
        {
            this.db = db;
        }
        public async Task<Admission> AcceptAdmission(int id)
        {
            var ad = await GetOneAdmission(id);
            if (ad != null)
            {
                ad.Status = "Accept";
                await db.SaveChangesAsync();
                return ad;
            }
            return null;
        }

        public async Task<Admission> DeleteAdmission(int id)
        {
            var ad = await GetOneAdmission(id);
            if (ad != null)
            {
                db.Admissions.Remove(ad);
                await db.SaveChangesAsync();
            }
            return null;
        }

        public async Task<IEnumerable<Admission>> GetAllProccess()
        {
            return await db.Admissions.Where(a => a.Status == "Proccess").ToListAsync();
        }

        public async Task<IEnumerable<Admission>> GetAllAccept()
        {
            return await db.Admissions.Where(a => a.Status == "Accept").ToListAsync();
        }

        public async Task<Admission> GetOneAdmission(int id)
        {
            return await db.Admissions.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Admission> RejectAdmission(int id)
        {
            var ad = await GetOneAdmission(id);
            if (ad != null)
            {
                ad.Status = "Reject";
                await db.SaveChangesAsync();
                return ad;
            }
            return null;
        }

        public async Task<IEnumerable<Admission>> GetAllReject()
        {
            return await db.Admissions.Where(a => a.Status == "Reject").ToListAsync();
        }
    }
}
