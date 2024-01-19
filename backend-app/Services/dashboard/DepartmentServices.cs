using backend_app.IRepository.dashboard;
using backend_app.Models;
using Microsoft.EntityFrameworkCore;

namespace backend_app.Services.dashboard
{
    public class DepartmentServices : IDepartment
    {
        private readonly DatabaseContext db;
        public DepartmentServices(DatabaseContext db)
        {
            this.db = db;
        }
        public async Task<Department> AddDepartment(Department department)
        {
            var depart = await GetOneDepartment(department.Id);
            if (depart == null)
            {
                db.Departments.Add(department);
                await db.SaveChangesAsync();
                return department;
            }
            return null;
        }

        public async Task<bool> checkCode(Department department)
        {
            if (department != null)
            {
                if (department.Id != null)
                {
                    return await db.Departments.AnyAsync(a => a.Code == department.Code && a.Id != department.Id);
                }
                else
                {
                    return await db.Departments.AnyAsync(a => a.Code == department.Code);
                }
            }

            return false;
        }

        public async Task<bool> checkSubject(Department department)
        {
            if (department != null)
            {
                if (department.Id != null)
                {
                    return await db.Departments.AnyAsync(a => a.Subject == department.Subject && a.Id != department.Id);
                }
                else
                {
                    return await db.Departments.AnyAsync(a => a.Subject == department.Subject);
                }
            }

            return false;
        }

        public async Task<Department> DeleteDepartment(int id)
        {
            var depart = await GetOneDepartment(id);
            if (depart != null)
            {
                db.Departments.Remove(depart);
                await db.SaveChangesAsync();
            }
            return null;
        }

        public async Task<IEnumerable<Department>> GetAllDepartments()
        {
            return await db.Departments.ToListAsync();
        }

        public async Task<Department> GetOneDepartment(int id)
        {
            return await db.Departments.SingleOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Department> UpdateDepartment(Department department)
        {
            db.Entry(department).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return department;
        }
    }
}
