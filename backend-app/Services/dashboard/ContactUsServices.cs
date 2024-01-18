using backend_app.IRepository.dashboard;
using backend_app.Models;
using Microsoft.EntityFrameworkCore;

namespace backend_app.Services.dashboard
{
    public class ContactUsServices : IContactUs
    {
        private readonly DatabaseContext db;

        public ContactUsServices(DatabaseContext db)
        {
            this.db = db;
        }

        public async Task<ContactUs> GetContact()
        {
            int contactId = 1;
            return await db.ContactUs.FirstOrDefaultAsync(c => c.Id == contactId);
        }


        public async Task<ContactUs> UpdateContact(ContactUs contactUs)
        {
            int contactId = 1;
            var existingAboutUsInfo = await db.ContactUs.FirstOrDefaultAsync(c => c.Id == contactId);

            if (existingAboutUsInfo != null)
            {
                existingAboutUsInfo.Email = contactUs.Email;
                existingAboutUsInfo.Address = contactUs.Address;
                existingAboutUsInfo.Phone = contactUs.Phone;
                existingAboutUsInfo.Description = contactUs.Description;
                existingAboutUsInfo.YouTubeLink = contactUs.YouTubeLink;

                await db.SaveChangesAsync();
                return existingAboutUsInfo; 
            }
            else
            {
                return null;
            }
        }

    }
}
