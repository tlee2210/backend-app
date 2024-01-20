using backend_app.DTO;
using backend_app.IRepository.dashboard;
using backend_app.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace backend_app.Services.dashboard
{
    public class FacultyService : IFaculty
    {
        private readonly DatabaseContext db;
        public FacultyService(DatabaseContext db)
        {
            this.db = db;
        }
        public async Task<IEnumerable<SelectOption>> GetCreate()
        {
            var options = await db.Courses
                .Select(x => new SelectOption
                {
                    label = x.Name,
                    value = x.Id
                })
                .ToListAsync();

            return options;
        }
        public async Task<Faculty> AddFaculties(Faculty faculty)
        {
            var faculti = await GetOnetitle(faculty.Title);
            if (faculti == null)
            {
                faculty.Slug = GenerateSlug(faculty.Title);
                db.Faculty.Add(faculty);
                await db.SaveChangesAsync();
                return faculty;
            }
            return null;
        }
       
        public async Task<GetEditSelectOption<Faculty>> GetEditFaculty(int id)
        {
            var faculti = await GetOneFaculty(id);
            if(faculti == null)
            {
                return null;
            }
            var options = await db.Courses
               .Select(x => new SelectOption
               {
                   label = x.Name,
                   value = x.Id
               })
               .ToListAsync();
            var Edit = new GetEditSelectOption<Faculty>
            {
                model = faculti,
                //articleDTO = articleDto,
                SelectOption = options
            };
            return Edit;
        }

        private string GenerateSlug(string phrase)
        {
            string str = phrase.ToLowerInvariant();

            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens   

            return str;
        }
        public async Task<bool> DeleteFaculties(int id)
        {
            var faculti = await GetOneFaculty(id);
            if (faculti == null)
            {
                return false;
            }
            db.Faculty.Remove(faculti);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<FacultyDTO>> GetAllFaculties()
        {
            var faculties = await db.Faculty.Include(c => c.Courses).ToListAsync();
            return faculties.Select(a => new FacultyDTO
            {
                Id = a.Id,
                Title = a.Title,
                Code = a.Code,
                Description = a.Description,
                Slug = a.Slug,
                Skill_learn = a.Skill_learn,
                Opportunities = a.Opportunities,
                EntryScore = a.EntryScore,
                Course_id = a.Course_id,
                CoursesName = a.Courses?.Name
            });
        }
        public async Task<Faculty> UpdateFaculty(Faculty faculty)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var old = await db.Faculty.FindAsync(faculty.Id);
                    old.Title = faculty.Title;
                    old.Slug = GenerateSlug(faculty.Title);
                    old.Code = faculty.Code;
                    old.Description = faculty.Description;
                    old.Course_id = faculty.Course_id;
                    old.Skill_learn = faculty.Skill_learn;
                    old.EntryScore = faculty.EntryScore;
                    old.Opportunities = faculty.Opportunities;
                    await db.SaveChangesAsync();
                    transaction.Commit(); 
                    return old;
                }
                catch (Exception)
                {
                    transaction.Rollback(); 
                    throw; 
                }
            }

            db.Entry(faculty).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return faculty;
        }
        public async Task<Faculty> GetOneFaculty(int id)
        {
            return await db.Faculty.SingleOrDefaultAsync(c => c.Id == id);
        }
        //check title
        public async Task<Faculty> GetOnetitle(string Title)
        {
            return await db.Faculty.FirstOrDefaultAsync(c => c.Title == Title);
        }
        public async Task<bool> checkCode(Faculty faculty)
        {
            if (faculty != null)
            {
                if (faculty.Id != null)
                {
                    return await db.Faculty.AnyAsync(a => a.Code == faculty.Code && a.Id != faculty.Id);
                }
                else
                {
                    return await db.Faculty.AnyAsync(a => a.Code == faculty.Code);
                }
            }

            return false;
        }

        public async Task<bool> checkTitle(Faculty faculty)
        {
            if(faculty != null)
            {
                if (faculty.Id != null)
                {
                    return await db.Faculty.AnyAsync(a => a.Title == faculty.Title && a.Id != faculty.Id);
                }
                else
                {
                    return await db.Faculty.AnyAsync(a => a.Title == faculty.Title);
                }
            }

            return false;
        }
    }
}
