using backend_app.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace backend_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpController : ControllerBase
    {
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("GetAdmin")]
        public IActionResult GetData()
        {
            var getCurrentUser = GetCurrentUser();
            var message = $"Hi {getCurrentUser.Name}, you are {getCurrentUser.Role}, {getCurrentUser.Id}";
            return Ok(message);
        }

        private Account GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;
                return new Account
                {
                    Id = int.Parse(userClaims.FirstOrDefault(o => o.Type == "Id")?.Value),
                    Email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                    Name = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Name)?.Value,
                    Role = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value,
                };
            }
            return null;
        }

    }
}
