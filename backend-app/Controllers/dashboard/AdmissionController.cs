using backend_app.IRepository.dashboard;
using backend_app.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend_app.Controllers.dashboard
{
    [Route("api/dashboard/admission")]
    [ApiController]
    public class AdmissionController : ControllerBase
    {
        private readonly IAdmission service;
        public AdmissionController(IAdmission service)
        {
            this.service = service;
        }

        [HttpGet("{id}")]
        public async Task<Admission> GetOneAdmission(int id)
        {
            return await service.GetOneAdmission(id);
        }

        [HttpGet("GetList")]
        public async Task<IEnumerable<Admission>> GetList()
        {
            return await service.GetAllAdmissions();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Admission>> DeleteAdmission(int id)
        {
            var ad = await service.DeleteAdmission(id);
            if (ad != null)
            {
                return Ok(new
                {
                    message = "New Admission Deleted Successfully"
                });
            }
            return BadRequest("false");
        }

        [HttpPut("AcceptAdmission")]
        public async Task<ActionResult<Admission>> AcceptAdmission(int id)
        {
            var ad = await service.AcceptAdmission(id);
            if (ad != null)
            {
                return Ok(new
                {
                    message = "New Admission Update Successfully"
                });
            }
            return BadRequest("false");
        }

        [HttpPut("RejectAdmission")]
        public async Task<ActionResult<Admission>> RejectAdmission(int id)
        {
            var ad = await service.RejectAdmission(id);
            if (ad != null)
            {
                return Ok(new
                {
                    message = "New Admission Update Successfully"
                });
            }
            return BadRequest("false");
        }
    }
}
