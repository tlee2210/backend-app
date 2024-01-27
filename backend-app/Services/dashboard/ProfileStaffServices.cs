using backend_app.DTO;
using backend_app.IRepository.dashboard;
using backend_app.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace backend_app.Services.dashboard
{
    public class ProfileStaffService : IProfileStaff
    {
        private readonly DatabaseContext db;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProfileStaffService(DatabaseContext db, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Staff> GetAuth(ClaimsPrincipal user)
        {
            var identity = user.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;

                var IdClaim = userClaims.FirstOrDefault(o => o.Type == "Id")?.Value;
                if (IdClaim == null)
                {
                    return null;
                }
                var Id = int.Parse(IdClaim);
                var request = _httpContextAccessor.HttpContext.Request;

                var StaffDetails = await db.Staffs.Where(s => s.Id == Id).FirstOrDefaultAsync();


                StaffDetails.FileAvatar = string.Format("{0}://{1}{2}/{3}", request.Scheme, request.Host, request.PathBase, StaffDetails.FileAvatar);
                return StaffDetails;
            }
            return null;
        }
        public async Task<bool> ChangePassword(ClaimsPrincipal user, ChangePasswordDTO changePassword)
        {
            var identity = user.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;
                var IdClaim = userClaims.FirstOrDefault(o => o.Type == "Id")?.Value;

                if (IdClaim == null)
                {
                    return false;
                }

                var Id = int.Parse(IdClaim);

                var staff = await db.Staffs.FindAsync(Id);

                if (staff != null)
                {
                    if (changePassword.NewPassword != changePassword.ConfirmPassword)
                    {
                        return false;
                    }

                    if (BCrypt.Net.BCrypt.Verify(changePassword.OldPassword, staff.Password))
                    {
                        staff.Password = BCrypt.Net.BCrypt.HashPassword(changePassword.NewPassword);
                        db.Staffs.Update(staff);
                        await db.SaveChangesAsync();
                        return true;
                    }
                }
            }
            return false;
        }

    }
}
