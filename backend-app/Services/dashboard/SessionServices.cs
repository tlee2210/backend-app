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

        public async Task<Session> UpdateSession(Session session)
        {
            db.Entry(session).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return session;
        }
    }
}
