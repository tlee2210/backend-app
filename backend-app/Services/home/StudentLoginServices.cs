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

        public StudentLoginServices(DatabaseContext db, IConfiguration configuration)
        {
            this.db = db;
            _configuration = configuration;
        }
        private async Task<StudentAccount> Authentication(EmailLogin staffLogin)
        {
            var listUser = await db.StudentAccounts.ToListAsync();
            if (listUser != null && listUser.Any())
            {
                var currenUser = listUser.FirstOrDefault(
                  x => x.Email.ToLower() == staffLogin.Email.ToLower() &&
                  BCrypt.Net.BCrypt.Verify(staffLogin.Password, x.Password));

                return currenUser;
            }
            return null;

        }
        private string GenerateToken(StudentAccount student)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("Id", student.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier, student.Email),
                new Claim(ClaimTypes.Anonymous, student.StudentCode)
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
                var auth = new StudentAccountDTO
                {
                    Id = user_.Id,
                    Email = user_.Email,
                    StudentCode = user_.StudentCode
                };
                var token = GenerateToken(user_);
                return new StudentLoginResult { Token = token, student = auth };
            }
            return null;
        }
    }
}
