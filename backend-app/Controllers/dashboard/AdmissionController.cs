using backend_app.IRepository.dashboard;
using backend_app.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend_app.Controllers.dashboard
{
    [Route("api/dashboard/admission")]
    [ApiController]
    [Authorize(Roles = "Admin")]
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
            return await service.GetEdit(id);
        }

        [HttpGet("GetAllProccess")]
        public async Task<IEnumerable<Admission>> GetAllProcess()
        {
            return await service.GetAllProcess();
        }

        [HttpGet("GetAllAccept")]
        public async Task<IEnumerable<Admission>> GetAllAccept()
        {
            return await service.GetAllAccept();
        }

        [HttpGet("GetAllReject")]
        public async Task<IEnumerable<Admission>> GetAllReject()
        {
            return await service.GetAllReject();
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

        [HttpPost("{id}/accept")]
        public async Task<ActionResult<Admission>> AcceptAdmission(int id)
        {
            var ad = await service.AcceptAdmission(id);
            if (ad != null)
            {
                await service.SendMail(id);
                return Ok(new
                {
                    message = "Admission accepted successfully and notification email sent."
                });
            }
            return BadRequest(new
            {
                message = "Admission acceptance failed or admission not found."
            });
        }

        [HttpPost("{id}/reject")]
        public async Task<ActionResult<Admission>> RejectAdmission(int id)
        {
            var ad = await service.RejectAdmission(id);
            if (ad != null)
            {
                return Ok(new
                {
                    message = "Admission Reject successfully"
                });
               
            }
            return BadRequest(new
            {
                message = "Admission Reject failed or admission not found."
            });
        }

        /*[HttpPost("SendMail")]
        public async Task<ActionResult<Admission>> SendFeedback(Admission admission)
        {
            var result = await service.SendMail(admission);
            if (result != null)
            {
                return Ok(new { message = "Confirmation email was sent successfully", data = result });
            }
            return BadRequest("Send mail failed");
        }*/
    }
}
