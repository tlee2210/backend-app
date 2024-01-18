using backend_app.DTO;
using backend_app.IRepository;
using backend_app.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace backend_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminLoginController : ControllerBase
    {
        private readonly IAdminLogin adminLogin;
        private IConfiguration _configuration;


        public AdminLoginController(IAdminLogin adminLogin, IConfiguration configuration)
        {
            this.adminLogin = adminLogin;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> PostUser(UserLogin userLogin)
        {
            try
            {
                var result = await adminLogin.Login(userLogin);
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
