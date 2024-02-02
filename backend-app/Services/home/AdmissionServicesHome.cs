using backend_app.DTO;
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
        public async Task<Admission> AddAdmission(AdmissionDTO admission)
        {
            //var ad = await db.Admissions.SingleOrDefaultAsync(a => a.Id == admission.Id);
            var ad = new Admission
            {
                FirstName = admission.FirstName,
                LastName = admission.LastName,
                FatherName = admission.FatherName,
                MotherName = admission.MotherName,
                Email = admission.Email,
                Phone = admission.Phone,
                DOB = admission.DOB,
                Gender = admission.Gender,
                Address = admission.Address,
                HighSchool = admission.HighSchool,
                GPA = admission.GPA,
                FacultyId = admission.FacultyId,
                Status = "Process",
            };

            db.Admissions.Add(ad);
            await db.SaveChangesAsync();
            return ad;
        }
    }
}
