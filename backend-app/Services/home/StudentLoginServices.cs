using backend_app.DTO;
using backend_app.IRepository.home;
using backend_app.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace backend_app.Services.home
{
    public class StudentLoginServices : IStudentLogin
    {
        private readonly DatabaseContext db;
        private IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StudentLoginServices(DatabaseContext db, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            this.db=db;
            _configuration=configuration;
            _httpContextAccessor=httpContextAccessor;
        }

        private async Task<Students> Authentication(EmailLogin studentLogin)
        {
            var listUser = await db.Students.ToListAsync();
            if (listUser != null && listUser.Any())
            {
                var currenUser = listUser.FirstOrDefault(
                  x => x.Email.ToLower() == studentLogin.Email.ToLower() && BCrypt.Net.BCrypt.Verify(studentLogin.Password, x.Password));
                return currenUser;
            }
            return null;

        }
        private string GenerateToken(Students student)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
            var request = _httpContextAccessor.HttpContext.Request;

            var claims = new[]
            {
                new Claim("Id", student.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier, student.Email),
                new Claim("StudentCode", student.StudentCode),
                new Claim("FatherName", student.FatherName),
                new Claim("MotherName", student.MotherName),
                new Claim("FirstName", student.FirstName),
                new Claim("LastName", student.LastName),
                new Claim(ClaimTypes.DateOfBirth, string.Format("{0}", student.DateOfBirth)),
                new Claim(ClaimTypes.Uri, string.Format("{0}://{1}{2}/{3}", request.Scheme, request.Host, request.PathBase, student.Avatar)),
                new Claim(ClaimTypes.Gender, string.Format("{0}", student.Gender)),
                new Claim("Address", student.Address),
                new Claim(ClaimTypes.MobilePhone, student.Phone)
            };

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<StudentLoginResult> Login(EmailLogin studentLogin)
        {
            var user_ = await Authentication(studentLogin);
            if (user_ != null)
            {
                var student = await db.Students.SingleOrDefaultAsync(s => s.Id == user_.Id);
                var request = _httpContextAccessor.HttpContext.Request;

                var auth = new Students
                {
                    Id = student.Id,
                    Email = student.Email,
                    StudentCode = student.StudentCode,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    FatherName = student.FatherName,
                    MotherName = student.MotherName,
                    DateOfBirth = student.DateOfBirth,
                    Gender = student.Gender,
                    Address = student.Address,
                    Phone = student.Phone,
                    Avatar = string.Format("{0}://{1}{2}/{3}", request.Scheme, request.Host, request.PathBase, student.Avatar)
                };
                var token = GenerateToken(user_);
                return new StudentLoginResult { Token = token, student = auth };
            }
            return null;
        }
    }
}
