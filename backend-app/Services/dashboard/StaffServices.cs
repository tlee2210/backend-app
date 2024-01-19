using backend_app.IRepository.dashboard;
using backend_app.Models;
using Microsoft.EntityFrameworkCore;

namespace backend_app.Services.dashboard
{
    public class StaffServices : IStaff
    {
        private readonly DatabaseContext db;
        public StaffServices(DatabaseContext db)
        {
            this.db = db;
        }
        public async Task<Staff> AddStaff(Staff staff)
        {
            var staf = await GetOneStaff(staff.Id);
            if (staf == null)
            {
                db.Staffs.Add(staff);
                await db.SaveChangesAsync();
                return staff;
            }
            return null;
        }

        public async Task<Staff> DeleteStaff(int id)
        {
            var staf = await GetOneStaff(id);
            if (staf != null)
            {
                db.Staffs.Remove(staf);
                await db.SaveChangesAsync();
            }
            return null;
        }

        public async Task<IEnumerable<Staff>> GetAllStaffs()
        {
            return await db.Staffs.ToListAsync();
        }

        public async Task<Staff> GetOneStaff(int id)
        {
            return await db.Staffs.SingleOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Staff> UpdateStaff(Staff staff)
        {
            db.Entry(staff).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return staff;
        }
    }
}
