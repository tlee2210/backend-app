using backend_app.IRepository.dashboard;
using backend_app.Models;
using Microsoft.EntityFrameworkCore;

namespace backend_app.Services.dashboard
{
    public class SessionServices : ISessions
    {
        private readonly DatabaseContext db;
        public SessionServices(DatabaseContext db)
        {
            this.db = db;
        }
        public async Task<Session> AddSession()
        {
            var currentYear = DateTime.Now.Year;
            var maxYearLimit = currentYear + 4;

            Session session = null;

            for (int year = currentYear; year <= maxYearLimit; year++)
            {
                var yearCode = year % 100;
                var code = $"{yearCode:00}UniStu";

                var existingSession = await db.Sessions.FirstOrDefaultAsync(s => s.Code == code);
                if (existingSession == null)
                {
                    session = new Session
                    {
                        Code = code,
                        YearStart = new DateTime(year, 8, 1),
                        YearEnd = new DateTime(year + 4, 7, 31),
                    };

                    db.Sessions.Add(session);
                    await db.SaveChangesAsync();

                    break; 
                }
            }

            return session;
        }

        public async Task<Session> DeleteSession(int id)
        {
            var se = await GetOneSession(id);
            if (se != null)
            {
                db.Sessions.Remove(se);
                await db.SaveChangesAsync();
                return se;
            }
            return null;
        }

        public async Task<IEnumerable<Session>> GetAllSessions()
        {
            return await db.Sessions.ToListAsync();
        }

        public async Task<Session> GetOneSession(int id)
        {
            return await db.Sessions.SingleOrDefaultAsync(a => a.Id == id);
        }

       
        public async Task<IEnumerable<Session>> UpdateSessionsStatusAndCurrentYear()
        {
            var sessions = await db.Sessions.ToListAsync();
            var currentYear = DateTime.Now;
            var nextSession = sessions.Where(s => s.YearStart > currentYear)
                                      .OrderBy(s => s.YearStart)
                                      .FirstOrDefault();

            // Đặt tất cả các kỳ học là không phải năm hiện tại
            sessions.ForEach(session => session.IsCurrentYear = false);

            foreach (var session in sessions)
            {
                if (currentYear >= session.YearStart && currentYear <= session.YearEnd)
                {
                    session.Status = SessionStatus.Active;
                }
                else if (currentYear > session.YearEnd)
                {
                    session.Status = SessionStatus.Completed;
                }
                else
                {
                    session.Status = SessionStatus.Inactive;
                }
            }

            // Đặt năm tuyển sinh hiện tại
            if (nextSession != null && currentYear < nextSession.YearStart)
            {
                nextSession.IsCurrentYear = true;
            }
            else if (nextSession == null)
            {
                // Nếu không có phiên nào trong tương lai, hãy tìm phiên hiện tại hoặc gần nhất trong quá khứ
                var currentOrPastSession = sessions.Where(s => s.YearEnd <= currentYear)
                                                   .OrderByDescending(s => s.YearEnd)
                                                   .FirstOrDefault();
                if (currentOrPastSession != null)
                {
                    currentOrPastSession.IsCurrentYear = true;
                }
            }

            db.Sessions.UpdateRange(sessions);
            await db.SaveChangesAsync();
            return sessions;
        }


    }
}
