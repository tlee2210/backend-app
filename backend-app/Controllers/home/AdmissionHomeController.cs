using backend_app.DTO;
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
        [Route("register")]
        public async Task<ActionResult<AdmissionDTO>> PostAdmission([FromForm]AdmissionDTO admission)
        {
            //return BadRequest( new {message = "register Admission false"});
            var ad = await service.AddAdmission(admission);
            if (ad != null)
            {
                return Ok(new
                {
                    message = "register Admission Successfully"
                });
            }
            return BadRequest("Register Admission faile");
        }
    }
}
