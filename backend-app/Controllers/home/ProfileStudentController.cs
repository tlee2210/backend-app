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
        [HttpGet]
        [Route("GetStudentProfile")]
        public IActionResult GetData()
        {
            var getCurrentUser = GetCurrentStaff();
            var message = $"Hi {getCurrentUser.LastName}, id: {getCurrentUser.Id}. My avatar: {getCurrentUser.Avatar}, {getCurrentUser.FirstName}, {getCurrentUser.Gender}, {getCurrentUser.Phone}, {getCurrentUser.Email}, {getCurrentUser.Address}, {getCurrentUser.StudentCode}";
            return Ok(message);
        }

        private Students GetCurrentStaff()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;
                return new Students
                {
                    Id = int.Parse(userClaims.FirstOrDefault(o => o.Type == "Id")?.Value),
                    Email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                    FirstName = userClaims.FirstOrDefault(o => o.Type == "FirstName")?.Value,
                    LastName = userClaims.FirstOrDefault(o => o.Type == "LastName")?.Value,
                    StudentCode = userClaims.FirstOrDefault(o => o.Type == "StudentCode")?.Value,
                    Address = userClaims.FirstOrDefault(o => o.Type == "Address")?.Value,
                    FatherName = userClaims.FirstOrDefault(o => o.Type == "FatherName")?.Value,
                    MotherName = userClaims.FirstOrDefault(o => o.Type == "MotherName")?.Value,
                    Avatar = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Uri)?.Value,
                    /*Gender = Enum.Parse(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Gender)?.Value),*/
                    Phone = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.MobilePhone)?.Value,
                    DateOfBirth = DateTime.Parse(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.DateOfBirth)?.Value),
                };
            }
            return null;
        }
    }
}
