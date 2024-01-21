using backend_app.IRepository.home;
using backend_app.Models;
using Microsoft.EntityFrameworkCore;

namespace backend_app.Services.home
{
    public class AdmissionServicesHome : IAdmissionHome
    {
        private readonly DatabaseContext db;
        public AdmissionServicesHome(DatabaseContext db)
        {
            this.db = db;
        }
        public async Task<Admission> AddAdmission(Admission admission)
        {
            var ad = await db.Admissions.SingleOrDefaultAsync(a => a.Id == admission.Id);
            if (ad == null)
            {
                admission.Status = "Process";
                db.Admissions.Add(admission);
                await db.SaveChangesAsync();
                return admission;
            }
            return null;
        }
    }
}
