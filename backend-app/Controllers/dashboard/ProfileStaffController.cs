using backend_app.DTO;
using backend_app.IRepository.dashboard;
using backend_app.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace backend_app.Controllers.dashboard
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileStaffController : ControllerBase
    {
        private readonly IProfileStaff service;

        public ProfileStaffController(IProfileStaff service)
        {
            this.service = service;
        }

        [HttpGet]
        [Route("GetStaffProfile")]
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
