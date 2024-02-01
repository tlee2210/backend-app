using backend_app.IRepository.home;
using backend_app.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace backend_app.Services.home
{
    public class HomeFeedbackService : IHomeFeedbackServices
    {
        private readonly DatabaseContext db;
        private object userClaims;

        public HomeFeedbackService(DatabaseContext db)
        {
            this.db = db;
        }
        public async Task<Feedback> SendFeedback(ClaimsPrincipal users, string feedbackContent)
        {
            var identity = users.Identity as ClaimsIdentity;
            if (identity == null)
            {
                throw new Exception("User identity is not valid.");
            }

            var userClaims = identity.Claims;

            var studentIdClaim = userClaims.FirstOrDefault(o => o.Type == "Id")?.Value;
            if (studentIdClaim == null)
            {
                throw new Exception("Student ID claim is not present.");
            }

            if (!int.TryParse(studentIdClaim, out int studentId))
            {
                throw new Exception("Student ID claim is not a valid integer.");
            }

            var student = await db.Students.FirstOrDefaultAsync(s => s.Id == studentId);
            if (student == null)
            {
                throw new Exception("Student not found.");
            }

            var feedback = new Feedback
            {
                Name = student.FirstName, 
                Email = student.Email,   
                Status = "Unprocessed",
                CreateAt = DateTime.Now,
                Description = feedbackContent
            };

            db.Feedbacks.Add(feedback);
            await db.SaveChangesAsync();

            return feedback;
        }

    }
}
