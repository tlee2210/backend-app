using backend_app.IRepository.home;
using backend_app.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<Feedback>> SendFeedback(Feedback feedback)
        {
            var result = await services.SendFeedback(feedback);
            if (result != null)
            {
                return Ok(new { message = "Feedback submitted successfully", data = result });
            }
            return BadRequest("Feedback submission failed");
        }
    }
}
