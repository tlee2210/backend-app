using backend_app.IRepository.home;
using backend_app.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend_app.Controllers.home
{
    [Route("api/home/admission")]
    [ApiController]
    public class AdmissionHomeController : ControllerBase
    {
        private readonly IAdmissionHome service;
        public AdmissionHomeController(IAdmissionHome service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<ActionResult<Admission>> PostAdmission(Admission admission)
        {
            var ad = await service.AddAdmission(admission);
            if (ad != null)
            {
                return Ok(new
                {
                    message = "New Admission Added Successfully"
                });
            }
            return BadRequest("false");
        }
    }
}
