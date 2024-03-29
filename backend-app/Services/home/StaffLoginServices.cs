﻿using backend_app.DTO;
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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StaffLoginServices(DatabaseContext db, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        private async Task<Staff> Authentication(EmailLogin staffLogin)
        {
            var listUser = await db.Staffs.ToListAsync();
            if (listUser != null && listUser.Any())
            {
                var currenUser = listUser.FirstOrDefault(
                  x => x.Email.ToLower() == staffLogin.Email.ToLower() && BCrypt.Net.BCrypt.Verify(staffLogin.Password, x.Password));

                return currenUser;
            }
            return null;

        }
        private string GenerateToken(Staff staff)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
            /*var staff = db.Staffs.FirstOrDefault(s => s.Id == staf.Id);*/
            var request = _httpContextAccessor.HttpContext.Request;

            var claims = new[]
            {
                new Claim("Id", staff.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier, staff.Email),
                new Claim("FirstName", staff.FirstName),
                new Claim("LastName", staff.LastName),
                new Claim(ClaimTypes.Role, staff.Role),
                new Claim("Address", staff.Address),
                new Claim("Experience", staff.Experience),
                new Claim(ClaimTypes.Gender, string.Format("{0}", staff.Gender)),
                new Claim("Qualification", staff.Qualification),
                new Claim(ClaimTypes.MobilePhone, staff.Phone),
                new Claim(ClaimTypes.Uri, string.Format("{0}://{1}{2}/{3}", request.Scheme, request.Host, request.PathBase, staff.FileAvatar))
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
        public async Task<LoginResult> Login(EmailLogin staffLogin)
        {
            var user_ = await Authentication(staffLogin);
            if (user_ != null)
            {
                var staf = await db.Staffs.SingleOrDefaultAsync(s => s.Id == user_.Id);
                var request = _httpContextAccessor.HttpContext.Request;

                var auth = new Account
                {
                    Id = staf.Id,
                    Email = staf.Email,
                    Name = staf.FirstName,
                    Role = staf.Role,
                    Avatar = string.Format("{0}://{1}{2}/{3}", request.Scheme, request.Host, request.PathBase, staf.FileAvatar)
                };
                var token = GenerateToken(user_);
                return new LoginResult { Token = token, user = auth };
            }
            return null;
        }
    }
}