using backend_app.IRepository.dashboard;
using backend_app.Models;
using backend_app.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MimeKit;
using Org.BouncyCastle.Asn1.IsisMtt.X509;

namespace backend_app.Services.dashboard
{
    public class AdmissionServices : IAdmission
    {
        private readonly DatabaseContext db;
        private readonly MailSettings _mailSettings;

        public AdmissionServices(DatabaseContext db, IOptions<MailSettings> mailSettings)
        {
            this.db=db;
            _mailSettings=mailSettings.Value;
        }

        public async Task<Admission> AcceptAdmission(int id)
        {
            var ad = await GetOneAdmission(id);
            if (ad != null)
            {
                ad.Status = "Accept";
                await db.SaveChangesAsync();
                return ad;
            }
            return null;
        }

        public async Task<Admission> DeleteAdmission(int id)
        {
            var ad = await GetOneAdmission(id);
            if (ad != null)
            {
                db.Admissions.Remove(ad);
                await db.SaveChangesAsync();
            }
            return null;
        }

        public async Task<IEnumerable<Admission>> GetAllProcess()
        {
            return await db.Admissions.Where(a => a.Status == "Process").ToListAsync();
        }

        public async Task<IEnumerable<Admission>> GetAllAccept()
        {
            return await db.Admissions.Where(a => a.Status == "Accept").ToListAsync();
        }

        public async Task<Admission> GetOneAdmission(int id)
        {
            return await db.Admissions.SingleOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Admission> GetEdit(int id)
        {
            return await db.Admissions.Include(c => c.Faculty).SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Admission> RejectAdmission(int id)
        {
            var ad = await GetOneAdmission(id);
            if (ad != null)
            {
                ad.Status = "Reject";
                await db.SaveChangesAsync();
                return ad;
            }
            return null;
        }

        public async Task<IEnumerable<Admission>> GetAllReject()
        {
            return await db.Admissions.Where(a => a.Status == "Reject").ToListAsync();
        }

        public async Task SendMail(int id)
        {
            var admissionOld = await db.Admissions.SingleOrDefaultAsync(a => a.Id == id);
            if (admissionOld == null)
            {
                return;
            }
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(admissionOld.Email));
            email.Subject = "Response";

            var builder = new BodyBuilder
            {
                HtmlBody = "\r\nThật là một sự ngạc nhiên thú vị khi biết rằng bạn đã đạt được hạng nhất trong kỳ thi B.com năm thứ nhất của lớp. Rất nhiều lời chúc mừng cho sự thành công đáng chú ý của bạn! Tin tức tuyệt vời này đã làm tất cả chúng tôi vui mừng và chúng tôi rất tự hào về bạn.\r\n\r\nSẽ phải nỗ lực rất nhiều để học tập chăm chỉ trong năm đầu tiên nếu học đại học khi bạn có quá nhiều điều phiền nhiễu. Nơi ở mới, bạn bè mới, lớp học và nhiều hơn thế nữa. Đó hẳn là một chặng đường thực sự khó khăn với bạn nhưng bạn đã vượt qua nó rất tốt. Với kết quả xuất sắc như vậy khi bắt đầu học đại học, bạn đã sẵn sàng ghi dấu ấn trong quá trình học tập sau này của mình. Nếu bạn tiếp tục thực hiện với lòng nhiệt thành và quyết tâm như vậy thì bạn sẽ sớm chạm tới bầu trời.\r\n\r\nCha mẹ bạn chắc phải tự hào về bạn lắm. Tôi nghĩ bạn sẽ sớm sẵn sàng đảm nhận trách nhiệm của cha bạn và sẽ là tài sản cho công việc kinh doanh của ông ấy. Với trình độ chuyên môn của mình, bạn sẽ có thể phát triển công việc kinh doanh của gia đình mình rất nhiều."
            };
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
