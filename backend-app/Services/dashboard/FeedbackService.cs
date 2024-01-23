using backend_app.DTO;
using backend_app.Models;
using backend_app.IRepository.dashboard;
using Microsoft.EntityFrameworkCore;
using backend_app.Settings;
using MailKit.Security;
using MimeKit;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;

namespace backend_app.Services.dashboard
{
    public class FeedbackService : IFeedbackServices
    {
        private readonly DatabaseContext db;
        private readonly MailSettings _mailSettings;
        public FeedbackService(DatabaseContext db, IOptions<MailSettings> mailSettings)
        {
            this.db = db;
            _mailSettings = mailSettings.Value;
        }
        public async Task<Feedback> GetFeedback(int id)
        {
            var fb = await db.Feedbacks.SingleOrDefaultAsync(f => f.Id == id);
            if (fb != null)
            {
                return fb;
            }
            return null;
        }

        public async Task<IEnumerable<Feedback>> GetListProcess()
        {
            return await db.Feedbacks.Where(f => f.Status == "Processed").ToListAsync();
        }

        public async Task<IEnumerable<Feedback>> GetListUnprocess()
        {
            return await db.Feedbacks.Where(f => f.Status == "Unprocessed").ToListAsync();
        }
        public async Task<Feedback> UpdateFeedback(Feedback feedback)
        {
            try
            {
                var feedOld = await db.Feedbacks.SingleOrDefaultAsync(f => f.Id == feedback.Id);
                if (feedOld == null)
                {
                    return null; // Feedback not found
                }

                feedOld.responses = feedback.responses;
                feedOld.Status = "Processed";

                await SendResponseEmail(feedOld);
                await db.SaveChangesAsync();

                return feedOld;
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                // Handle exception as needed
                return null;
            }
        }
        private async Task SendResponseEmail(Feedback feedback)
        {
            try
            {
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
                email.To.Add(MailboxAddress.Parse(feedback.Email));
                email.Subject = "Response";

                var builder = new BodyBuilder
                {
                    HtmlBody = feedback.responses
                };
                email.Body = builder.ToMessageBody();

                using var smtp = new SmtpClient();
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                // Handle email sending exceptions as needed
            }
        }
    }
}
