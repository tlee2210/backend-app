using backend_app.Models;

namespace backend_app.IRepository.dashboard
{
    public interface IContactUs
    {
        Task<ContactUs> GetContact();
        Task<ContactUs> UpdateContact(ContactUs contactUs);
    }
}
