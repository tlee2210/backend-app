using backend_app.DTO;
using backend_app.IRepository.dashboard;
using backend_app.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace backend_app.Services.dashboard
{
    public class FacultyService : IFaculty
    {
        private readonly DatabaseContext db;
        private readonly IWebHostEnvironment HostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FacultyService(DatabaseContext db, IWebHostEnvironment hostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            this.db=db;
            HostEnvironment=hostEnvironment;
            _httpContextAccessor=httpContextAccessor;
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
        public async Task<Faculty> AddFaculties(FacultyImage facultyImg)
        {
            var faculti = await GetOnetitle(facultyImg.Title);
            if (faculti == null)
            {
                var faculty = new Faculty
                {
                    Code = facultyImg.Code,
                    Title = facultyImg.Title,
                    Course_id = facultyImg.Course_id,
                    Description = facultyImg.Description,
                    EntryScore = facultyImg.EntryScore,
                    Image = await SaveImage(facultyImg.Image),
                    Opportunities = facultyImg.Opportunities,
                    Skill_learn = facultyImg.Skill_learn,
                    Slug = GenerateSlug(facultyImg.Title),
                };
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
            var request = _httpContextAccessor.HttpContext.Request;

            var faculty = new Faculty
            {
                Code = faculti.Code,
                Title = faculti.Title,
                Course_id = faculti.Course_id,
                Description = faculti.Description,
                EntryScore = faculti.EntryScore,
                Opportunities = faculti.Opportunities,
                Skill_learn = faculti.Skill_learn,
                Slug = faculti.Title,
                Image = string.Format("{0}://{1}{2}/{3}", request.Scheme, request.Host, request.PathBase, faculti.Image)
            };
            var Edit = new GetEditSelectOption<Faculty>
            {
                model = faculty,
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
            var faculty = await db.Faculty.SingleOrDefaultAsync(f => f.Id == id);
            if (faculty != null)

            {
                var imagePath = Path.Combine(faculty.Image);
                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                }
                db.Faculty.Remove(faculty);
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<FacultyDTO>> GetAllFaculties()
        {
            var faculties = await db.Faculty.Include(c => c.Courses).ToListAsync();
            var request = _httpContextAccessor.HttpContext.Request;
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
                CoursesName = a.Courses?.Name,
                Image = string.Format("{0}://{1}{2}/{3}", request.Scheme, request.Host, request.PathBase, a.Image)
            });
        }
        public async Task<Faculty> UpdateFaculty(FacultyImage facultyImg)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var faculty = await db.Faculty.SingleOrDefaultAsync(s => s.Id == facultyImg.Id);
                    if (faculty == null)
                    {
                        return null;
                    }

                    faculty.Code = facultyImg.Code;
                    faculty.Title = facultyImg.Title;
                    faculty.Course_id = facultyImg.Course_id;
                    faculty.Description = facultyImg.Description;
                    faculty.EntryScore = facultyImg.EntryScore;
                    faculty.Opportunities = facultyImg.Opportunities;
                    faculty.Skill_learn = facultyImg.Skill_learn;
                    faculty.Slug = GenerateSlug(facultyImg.Title);

                    if (facultyImg.Image != null)
                    {
                        if (!string.IsNullOrEmpty(faculty.Image))
                        {
                            var imagePath = Path.Combine(faculty.Image);
                            if (File.Exists(imagePath))
                            {
                                File.Delete(imagePath);
                            }
                        }
                        faculty.Image = await SaveImage(facultyImg.Image);
                    }

                    await db.SaveChangesAsync();
                    transaction.Commit();
                    return faculty;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public async Task<FacultyDTO> GetOneFaculty(int id)
        {
            var faculty = await db.Faculty.SingleOrDefaultAsync(c => c.Id == id);
            if (faculty == null)
            {
                return null;
            }
            var facultyDTO = new FacultyDTO
            {
                Code = faculty.Code,
                Title = faculty.Title,
                Course_id = faculty.Course_id,
                Description = faculty.Description,
                EntryScore = faculty.EntryScore,
                Opportunities = faculty.Opportunities,
                Skill_learn = faculty.Skill_learn,
                Slug = faculty.Title,
                Image = faculty.Image,
            };
            /*var request = _httpContextAccessor.HttpContext.Request;
            facultyDTO.Image = string.Format("{0}://{1}{2}/{3}", request.Scheme, request.Host, request.PathBase, faculty.Image);*/
            return facultyDTO;
        }
        //check title
        public async Task<Faculty> GetOnetitle(string Title)
        {
            return await db.Faculty.FirstOrDefaultAsync(c => c.Title == Title);
        }
        public async Task<bool> checkCode(FacultyImage faculty)
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

        public async Task<bool> checkTitle(FacultyImage faculty)
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

        public async Task<IEnumerable<Faculty>> SearchFaculty(string title)
        {
            var listFaculty = await db.Faculty.Where(c => c.Title.Contains(title)).ToListAsync();
            return listFaculty;
        }

        [NonAction]
        public async Task<string> SaveImage(IFormFile formFile)
        {
            string imageName = new string(Path.GetFileNameWithoutExtension(formFile.FileName).Take(10).ToArray()).Replace(" ", "_");
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(formFile.FileName);

            // Thêm tiền tố "Image/Article/" vào imageName
            string imageNameWithPath = "Image/Faculty/" + imageName;

            // Cập nhật đường dẫn lưu trữ ảnh
            var imagePath = Path.Combine(HostEnvironment.ContentRootPath, "Image", "Faculty", imageName);

            // Tạo thư mục nếu nó chưa tồn tại
            var directoryPath = Path.GetDirectoryName(imagePath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await formFile.CopyToAsync(fileStream);
            }

            return imageNameWithPath;
        }
    }
}
