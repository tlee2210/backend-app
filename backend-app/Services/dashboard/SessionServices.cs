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
        public async Task<Session> AddSession(Session session)
        {
            var se = await GetOneSession(session.Id);
            if (se == null)
            {
                db.Sessions.Add(session);
                await db.SaveChangesAsync();
                return session;
            }
            return null;
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
