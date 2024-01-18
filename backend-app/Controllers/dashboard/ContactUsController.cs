using backend_app.DTO;
using backend_app.IRepository.dashboard;
using backend_app.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace backend_app.Controllers.dashboard
{
    [Route("api/dashboard/ContactUs")]
    [ApiController]
    public class ContactUsController : ControllerBase
    {
        private readonly DatabaseContext db;
        private readonly IContactUs contact;

        public ContactUsController(IContactUs contact)
        {
            this.contact = contact;
        }
        [HttpGet]
        public async Task<ContactUs> GetAboutUs()
        {
            return await contact.GetContact();
        }
        [HttpPut]
        [Route("Update")]
        public async Task<ActionResult<ContactUs>> UpdateAboutUs([FromForm] ContactUs contactUs)
        {
            var result = await contact.UpdateContact(contactUs);
            if (result != null)
            {
                return Ok(new
                {
                    message = "Contact information updated successfully."
                }) ;
            }
            return BadRequest("Update failed. The specified contact information could not be found or was invalid.");
        }
    }
}
