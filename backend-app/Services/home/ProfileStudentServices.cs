using backend_app.DTO;
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

        public async Task<StudentprofileDTO> GetAuth(ClaimsPrincipal user)
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

                var facultyId = studentWithDetails.StudentFacultySemesters.FacultyId;
                var SessionId = studentWithDetails.StudentFacultySemesters.SessionId;

                IQueryable<DepartmentSemesterSession> query = db.departmentSemesterSessions
                .Include(d => d.Department)
                .Include(s => s.Semester)
                .Include(a => a.session)
                .Where(dss => dss.FacultyId == facultyId && dss.SessionId == SessionId);

                var groupedResults = await query.GroupBy(dss => dss.SemesterId).ToListAsync();

                var dividedResults = new List<List<DepartmentSemesterSession>>();

                for (int semesterIndex = 1; semesterIndex <= 8; semesterIndex++)
                {
                    var semesterResults = groupedResults.SingleOrDefault(group => group.Key == semesterIndex);

                    if (semesterResults != null)
                    {
                        dividedResults.Add(semesterResults.ToList());
                    }
                    else
                    {
                        dividedResults.Add(new List<DepartmentSemesterSession>());
                    }
                }

                studentWithDetails.Avatar = string.Format("{0}://{1}{2}/{3}", request.Scheme, request.Host, request.PathBase, studentWithDetails.Avatar);

                var result = new StudentprofileDTO
                {
                    students = studentWithDetails,
                    DepartmentSemesterSessionsGrouped = dividedResults
                };
                return result;

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

                var Student = await db.Students.FindAsync(Id);

                if (Student != null)
                {
                    if (changePassword.NewPassword != changePassword.ConfirmPassword)
                    {
                        return false;
                    }

                    if (BCrypt.Net.BCrypt.Verify(changePassword.OldPassword, Student.Password))
                    {
                        Student.Password = BCrypt.Net.BCrypt.HashPassword(changePassword.NewPassword);
                        db.Students.Update(Student);
                        await db.SaveChangesAsync();
                        return true;
                    }
                }
            }
            return false;
        }

    }
}
