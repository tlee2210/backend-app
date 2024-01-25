using backend_app.DTO;
using backend_app.IRepository;
using backend_app.IRepository.home;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend_app.Controllers.home
{
    [Route("api/home/stafflogin")]
    [ApiController]
    public class StaffLoginController : ControllerBase
    {
        private readonly IStaffLogin service;
        private IConfiguration _configuration;

        public StaffLoginController(IStaffLogin service, IConfiguration configuration)
        {
            this.service = service;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Login(EmailLogin staffLogin)
        {
            try
            {
                var result = await service.Login(staffLogin);
                if (result != null)
                {
                    return Ok(result);
                }
                return BadRequest(new { message = "Login failed: The provided credentials are incorrect or the user does not exist." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}