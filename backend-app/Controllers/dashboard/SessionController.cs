using backend_app.IRepository.dashboard;
using backend_app.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend_app.Controllers.dashboard
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private ISessions service;
        public SessionController(ISessions services)
        {
            this.service = services;
        }

        [HttpGet("{id}/edit")]
        public async Task<Session> GetOneSession(int id)
        {
            return await service.GetOneSession(id);
        }

        [HttpGet("GetList")]
        public async Task<IEnumerable<Session>> GetList()
        {
            return await service.GetAllSessions();
        }

        [HttpGet]
        [Route("create")]
        public async Task<ActionResult> createSession()
        {
            var result = await service.AddSession();
            if (result != null)
            {
                return Ok(new
                {
                    message = "New Session Added Successfully"
                });
            }
            return BadRequest(new { message = "Failed to create a new session: exceeded the allowed number of sessions" });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSession(int id)
        {
            var result = await service.DeleteSession(id);
            if (result != null)
            {
                return Ok(new
                {
                    message = "Session Deleted Successfully",
                    data = result.Id
                });
            }
            return BadRequest("Delete fail");
        }

        [HttpPost("update")]
        public async Task<ActionResult<Department>> PutSession(Session session)
        {
            var result = await service.UpdateSession(session);
            if (result != null)
            {
                return Ok(new
                {
                    message = "Session Update Successfully",
                    data = result
                });
            }
            return BadRequest("Update fail");
        }
    }
}
