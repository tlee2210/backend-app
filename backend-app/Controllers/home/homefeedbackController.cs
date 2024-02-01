using backend_app.IRepository.home;
using backend_app.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace backend_app.Controllers.home
{
    [Route("api/home/feedback")]
    [ApiController]
    public class homefeedbackController : ControllerBase
    {
        private readonly IHomeFeedbackServices services;
        public homefeedbackController(IHomeFeedbackServices services)
        {
            this.services = services;
        }
        //nguoi dung gui feedback
        [HttpPost]
        public async Task<ActionResult> SendFeedback(string Description)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 

            var result = await services.SendFeedback(User, Description);

            if (result != null)
            {
                return Ok(new { message = "Feedback submitted successfully", data = result });
            }
            return BadRequest("Feedback submission failed");
        }

    }
}
