using backend_app.DTO;
using backend_app.IRepository.home;
using backend_app.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.X509;
using System.Security.Claims;

namespace backend_app.Controllers.home
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileStudentController : ControllerBase
    {
        private readonly IProfileStudent service;

        public ProfileStudentController(IProfileStudent service)
        {
            this.service = service;
        }

        [HttpGet]
        [Route("GetStudentProfile")]
        public async Task<IActionResult> GetData()
        {
            var getCurrentUser = await service.GetAuth(User);
            if (getCurrentUser != null)
            {
                return Ok(getCurrentUser);
            }
            return NotFound();
        }
        [HttpPost]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO changePassword)
        {
            var result = await service.ChangePassword(User, changePassword);
            if (result)
            {
                return Ok(new { message = "Password has been successfully changed." });
            }
            return BadRequest(new { message = "Unable to change password. Please check the old password." });
        }
    }
}
