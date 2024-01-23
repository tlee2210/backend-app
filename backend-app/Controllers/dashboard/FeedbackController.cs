using backend_app.DTO;
using backend_app.IRepository.dashboard;
using backend_app.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend_app.Controllers.dashboard
{
    [Route("api/dashboard/Feedback")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private IFeedbackServices service;

        public FeedbackController(IFeedbackServices service)
        {
            this.service=service;
        }
        [HttpGet("{id}/detail")]
        public async Task<Feedback> GetFeedback(int id)
        {
            return await service.GetFeedback(id);
        }

        [HttpGet("GetListProcess")]
        public async Task<IEnumerable<Feedback>> GetListProcess()
        {
            return await service.GetListProcess();
        }

        [HttpGet("GetListUnprocess")]
        public async Task<IEnumerable<Feedback>> GetListUnprocess()
        {
            return await service.GetListUnprocess();
        }

        [HttpPut]
        public async Task<Feedback> UpdateFeedback(Feedback feedback)
        {
            return await service.UpdateFeedback(feedback);
        }
    }
}
