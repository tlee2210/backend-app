using backend_app.DTO;
using backend_app.IRepository.home;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend_app.Controllers.home
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentLoginController : ControllerBase
    {
        private readonly IStudentLogin service;
        private IConfiguration _configuration;

        public StudentLoginController(IStudentLogin service, IConfiguration configuration)
        {
            this.service=service;
            _configuration=configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Login(EmailLogin studentLogin)
        {
            try
            {
                var result = await service.Login(studentLogin);
                if (result != null)
                {
                    return Ok(result);
                }
                return BadRequest("Login failed");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
