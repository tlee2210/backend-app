using backend_app.IRepository.home;
using backend_app.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace backend_app.Services.home
{
    public class ProfileStudentServices : IProfileStudent
    {
        private readonly DatabaseContext db;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProfileStudentServices(DatabaseContext db, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Students> GetAuth(ClaimsPrincipal user)
        {
            var identity = user.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;

                var studentIdClaim = userClaims.FirstOrDefault(o => o.Type == "Id")?.Value;
                if (studentIdClaim == null)
                {
                    return null;
                }
                var studentId = int.Parse(studentIdClaim);
                var request = _httpContextAccessor.HttpContext.Request;

                var studentWithDetails = await db.Students
                    .Where(s => s.Id == studentId)
                    .Include(a => a.StudentFacultySemesters)
                        .ThenInclude(sfs => sfs.Session)
                    .Include(a => a.StudentFacultySemesters)
                        .ThenInclude(sfs => sfs.Faculty)
                    .Include(a => a.StudentFacultySemesters)
                        .ThenInclude(sfs => sfs.Semester)
                    .FirstOrDefaultAsync();


                studentWithDetails.Avatar = string.Format("{0}://{1}{2}/{3}", request.Scheme, request.Host, request.PathBase, studentWithDetails.Avatar);
                return studentWithDetails;
            }
            return null;
        }

    }
}
