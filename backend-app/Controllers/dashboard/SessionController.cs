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
                    message = "New Session Added Successfully",
                    data= result
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

        [HttpGet("UpdateSessions")]
        public async Task<ActionResult<Department>> UpdateSessions()
        {
            try
            {
                var updatedSessions = await service.UpdateSessionsStatusAndCurrentYear();
                return Ok(new
                {
                    message = "Sessions updated successfully",
                    data = updatedSessions
                });
            }
            catch (Exception ex)
            {
                return BadRequest($"Update failed: {ex.Message}");
            }
        }
    }
}
