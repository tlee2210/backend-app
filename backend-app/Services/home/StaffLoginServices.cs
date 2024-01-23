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
    public class StaffLoginServices : IStaffLogin
    {
        private readonly DatabaseContext db;
        private IConfiguration _configuration;

        public StaffLoginServices(DatabaseContext db, IConfiguration configuration)
        {
            this.db = db;
            _configuration = configuration;
        }
        private async Task<StaffAccount> Authentication(StaffLogin staffLogin)
        {
            var listUser = await db.StaffAccounts.ToListAsync();
            if (listUser != null && listUser.Any())
            {
                var currenUser = listUser.FirstOrDefault(
                  x => x.Email.ToLower() == staffLogin.Email.ToLower() &&
                  BCrypt.Net.BCrypt.Verify(staffLogin.Password, x.Password));

                return currenUser;
            }
            return null;

        }
        private string GenerateToken(StaffAccount staff)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("Id", staff.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier, staff.Email),
                new Claim(ClaimTypes.Name, staff.FirstName),
                new Claim(ClaimTypes.Name, staff.LastName),
                new Claim(ClaimTypes.Role, staff.Role),
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
        public async Task<StaffLoginResult> Login(StaffLogin staffLogin)
        {
            var user_ = await Authentication(staffLogin);
            if (user_ != null)
            {
                var auth = new StaffAccountDTO
                {
                    Id = user_.Id,
                    Email = user_.Email,
                    FirstName = user_.FirstName,
                    LastName = user_.LastName,
                    Role = user_.Role
                };
                var token = GenerateToken(user_);
                return new StaffLoginResult { Token = token, staff = auth };
            }
            return null;
        }
    }
}
