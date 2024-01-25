using backend_app.DTO;
using backend_app.IRepository;
using backend_app.IRepository.home;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend_app.Controllers.home
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffLoginController : ControllerBase
    {
        private readonly IStaffLogin service;
        private IConfiguration _configuration;

        public StaffLoginController(IStaffLogin service, IConfiguration configuration)
        {
            this.service=service;
            _configuration=configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLogin userLogin)
        {
            try
            {
                var result = await service.Login(userLogin);
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
