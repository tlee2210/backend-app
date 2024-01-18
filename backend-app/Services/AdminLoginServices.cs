using backend_app.DTO;
using backend_app.IRepository;
using Microsoft.AspNetCore.Authorization;
using backend_app.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace backend_app.Services
{
    public class AdminLoginServices : IAdminLogin
    {
        private readonly DatabaseContext db;
        private IConfiguration _configuration;

        public AdminLoginServices(DatabaseContext db, IConfiguration configuration)
        {
            this.db = db;
            _configuration = configuration;
        }
        private async Task<adminAccount> Authentication(UserLogin user)
        {
            var listUser = await db.AdminAccounts.ToListAsync();
            if (listUser != null && listUser.Any())
            {
                var currenUser = listUser.FirstOrDefault(
                  x => x.Email.ToLower() == user.Email.ToLower() &&
                  BCrypt.Net.BCrypt.Verify(user.Password, x.Password));

                return currenUser;
            }
            return null;

        }
        private string GenerateToken(adminAccount adminAccount)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("Id", adminAccount.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier, adminAccount.Email),
                new Claim(ClaimTypes.Name, adminAccount.Name),
                new Claim(ClaimTypes.Role, adminAccount.Role),
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
        public async Task<LoginResult> Login(UserLogin userLogin)
        {
            var user_ = await Authentication(userLogin);
            if (user_ != null)
            {
                var auth = new Account
                {
                    Id = user_.Id,
                    Email = user_.Email,
                    Name = user_.Name,
                    Role = user_.Role
                };
                var token = GenerateToken(user_);
                return new LoginResult { Token = token, user = auth };
            }
            return null;
        }
    }
}
