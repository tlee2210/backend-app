using backend_app.DTO;
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
        /*[Authorize(Roles = "Admin")]*/
        [HttpGet]
        [Route("GetStaffAdmin")]
        public IActionResult GetData()
        {
            var getCurrentUser = GetCurrentStaff();
            var message = $"Hi {getCurrentUser.LastName}, you are {getCurrentUser.Role}, {getCurrentUser.Id}. My avatar: {getCurrentUser.FileAvatar}, {getCurrentUser.FirstName}, {getCurrentUser.Gender}, {getCurrentUser.Phone}, {getCurrentUser.Email}, {getCurrentUser.Address}, {getCurrentUser.Experience}, {getCurrentUser.Qualification}";
            return Ok(message);
        }

        private Staff GetCurrentStaff()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;
                return new Staff
                {
                    Id = int.Parse(userClaims.FirstOrDefault(o => o.Type == "Id")?.Value),
                    Email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                    FirstName = userClaims.FirstOrDefault(o => o.Type == "FirstName")?.Value,
                    LastName = userClaims.FirstOrDefault(o => o.Type == "LastName")?.Value,
                    Role = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value,
                    Address = userClaims.FirstOrDefault(o => o.Type == "Address")?.Value,
                    Experience = userClaims.FirstOrDefault(o => o.Type == "Experience")?.Value,
                    FileAvatar = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Uri)?.Value,
                    /*Gender = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Gender)?.Value,*/
                    Phone = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.MobilePhone)?.Value,
                    Qualification = userClaims.FirstOrDefault(o => o.Type == "Qualification")?.Value,
                };
            }
            return null;
        }
    }
}
