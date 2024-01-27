using Microsoft.EntityFrameworkCore;
using System;

namespace backend_app.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ArticleCategory> ArticleCategories { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<Courses> Courses { get; set; }
        public DbSet<Faculty> Faculty { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Facilities> Facilities { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Students> Students { get; set; }
        public DbSet<Semester> semesters { get; set; }
        public DbSet<Admission> Admissions { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<StudentFacultySemesters> StudentFacultySemesters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>(c =>
            {
                c.HasKey(x => x.Id);
                c.HasIndex(a => a.Name).IsUnique();
            });
            modelBuilder.Entity<Article>(c =>
            {
                c.HasKey(x => x.Id);
                c.HasIndex(a => a.Title).IsUnique();
            });
            modelBuilder.Entity<ArticleCategory>(c =>
            {
                c.HasKey(x => x.Id);
                c.HasOne(a => a.Category).WithMany(b => b.ArticleCategories).HasForeignKey(c => c.CategoryId);
                c.HasOne(a => a.Article).WithMany(b => b.ArticleCategories).HasForeignKey(c => c.ArticleId);
            });
            modelBuilder.Entity<Feedback>(c =>
            {
                c.HasKey(x => x.Id);
            });
            modelBuilder.Entity<ContactUs>(c =>
            {
                c.HasKey(x => x.Id);
                c.HasData(new ContactUs[]
                {
                    new ContactUs
                    {
                        Id = 1,
                        Email="univercity@gmail.com",
                        Address = "391 Nam Ky Khoi Nghia,Quan 3",
                        Description = "Welcome to [TQVC University] - a place that fosters personal development and academic success. We take pride in providing a diverse and positive learning environment where students not only gain knowledge but also develop essential life skills.\r\n\r\nMission:\r\nWe are committed to educating and developing individuals with deep knowledge, flexible skills, and compassion. Our mission is to create a motivated, innovative, and globally-minded community of students.\r\n\r\nEducation:\r\nAt [TQVC University], we offer a diverse range of educational programs from undergraduate to postgraduate levels, training students to become visionaries ready to face the challenges of the contemporary world.\r\n\r\nFacilities:\r\nWith modern and comprehensive facilities, we have created an international-standard environment for learning and research. Our library, laboratories, and sports facilities are well-equipped to support students in their academic and research endeavors.\r\n\r\nCore Values:\r\n\r\nQuality: We are dedicated to ensuring high quality in education and research.\r\nInnovation: We encourage creativity and innovative thinking in all aspects.\r\nDiversity: We respect and promote diversity within our academic and social community.\r\nSocial Interaction: We foster collaboration and social interaction between students and faculty.\r\n\r\nWe hope that [TQVC University] becomes your choice for personal development and preparation for a successful future. Let's work together to build new dreams and achievements!",
                        YouTubeLink = "https://www.youtube.com/watch?v=LlCwHnp3kL4",
                        Phone="0905028073",
                    }
                });
            });
            modelBuilder.Entity<Courses>(c =>
            {
                c.HasKey(x => x.Id);
                c.HasData(new Courses[]
                {
                    new Courses{Id = 1, Name = "K22", Description = "2022-2026"},
                    new Courses{Id = 2, Name = "K23", Description = "2023-2027"}
                });
            });
            modelBuilder.Entity<Faculty>(c =>
            {
                c.HasKey(x => x.Id);
                c.HasOne(a => a.Courses).WithMany(b => b.Faculty).HasForeignKey(c => c.Course_id);
                c.HasData(new Faculty[]
                {
                    new Faculty{Id = 1, Code = "C01", Title = "IT", Slug="IT", Description="123", EntryScore=100, Skill_learn = "ABC", Opportunities = "DEF", Course_id = 1},
                    new Faculty{Id = 2, Code = "C02", Title = "Philosophy", Slug="Philosophy", Description="123", EntryScore=100, Skill_learn = "XYZ", Opportunities = "123", Course_id = 1},
                    new Faculty{Id = 3, Code = "C03", Title = "Advanced Math", Slug="Advanced-Math", Description="123", EntryScore=100, Skill_learn = "Math", Opportunities = "456", Course_id = 2}
                });
            });
            modelBuilder.Entity<Department>(c =>
            {
                c.HasKey(x => x.Id);
                c.HasData(new Department[]
                {
                    new Department{Id = 1, Code = "ABC", Subject = "ADB", Description = "123123"},
                    new Department{Id = 2, Code = "DEF", Subject = "DEF", Description = "23424"},
                    new Department{Id = 3, Code = "XYA", Subject = "XYA", Description = "1"}
                });
            });
            modelBuilder.Entity<Facilities>(f =>
            {
                f.HasKey(f => f.Id);
                f.HasData(new Facilities[]
                {
                     new Facilities {Id=1, Title="Canteen", Description="123",Image="123"},
                });
            });
            modelBuilder.Entity<Staff>(c =>
            {
                c.HasKey(x => x.Id);
                c.HasData(new Staff[]
                {
                    new Staff {Id = 2, FirstName = "Chuong", LastName = "Chuong", Email = "namechuong19@gmail.com", Address = "391 Nam Ky Khoi Nghia,Quan 3", Gender = 0, Phone = "0974671412", FileAvatar = "Image/Staff/1.png", Qualification = "Admin", Experience = "Admin", Password = BCrypt.Net.BCrypt.HashPassword("chuong123"), Role="Admin"},
                    new Staff {Id = 1, FirstName = "Tlee", LastName = "Tlee", Email = "thienle255@gmail.com", Address = "391 Nam Ky Khoi Nghia,Quan 3", Gender = 0, Phone = "0905028073", FileAvatar = "Image/Staff/1.png", Qualification = "Admin", Experience = "Admin", Password = BCrypt.Net.BCrypt.HashPassword("Tlee2210"), Role="Admin"}
                });
            });
            modelBuilder.Entity<Session>(c =>
            {
                c.HasKey(x => x.Id);
                c.HasData(new Session[]
                {
                     new Session{Id = 1, Code = "21UniStu", YearStart = new DateTime(2021, 8, 1), YearEnd = new DateTime(2024, 7, 31), Status = SessionStatus.Completed,IsCurrentYear = true,},
                     new Session{Id = 2, Code = "22UniStu", YearStart = new DateTime(2022, 8, 1), YearEnd = new DateTime(2025, 7, 31), Status = SessionStatus.Active},
                     new Session{Id = 3, Code = "23UniStu", YearStart = new DateTime(2023, 8, 1), YearEnd = new DateTime(2026, 7, 31), Status = SessionStatus.Active},
                     new Session{Id = 4, Code = "24UniStu", YearStart = new DateTime(2024, 8, 1), YearEnd = new DateTime(2027, 7, 31), IsCurrentYear = true, Status = SessionStatus.Active},
                     new Session{Id = 5, Code = "25UniStu", YearStart = new DateTime(2025, 8, 1), YearEnd = new DateTime(2028, 7, 31), Status = SessionStatus.Inactive},
                });
            });
            modelBuilder.Entity<Semester>(s =>
            {
                s.HasKey(k => k.Id);
                s.HasData(new Semester[]
                {
                    new Semester{Id = 1, AcademicYear = 1, SemesterNumber = 1},
                    new Semester{Id = 2, AcademicYear = 1, SemesterNumber = 2},
                    new Semester{Id = 3, AcademicYear = 2, SemesterNumber = 1},
                    new Semester{Id = 4, AcademicYear = 2, SemesterNumber = 2},
                    new Semester{Id = 5, AcademicYear = 3, SemesterNumber = 1},
                    new Semester{Id = 6, AcademicYear = 3, SemesterNumber = 2},
                    new Semester{Id = 7, AcademicYear = 4, SemesterNumber = 1},
                    new Semester{Id = 8, AcademicYear = 4, SemesterNumber = 2},
                });
            });
            modelBuilder.Entity<Students>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.StudentCode).IsRequired();
                entity.Property(e => e.FirstName).HasMaxLength(50);
                entity.Property(e => e.LastName).HasMaxLength(50);
                entity.Property(e => e.Email).IsRequired();
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.DateOfBirth).HasColumnType("date");
                entity.HasOne(s => s.StudentFacultySemesters).WithOne().HasForeignKey<StudentFacultySemesters>(s => s.StudentId);
                entity.Property(e => e.Gender).HasConversion<string>();
                entity.HasData(new Students[]
                {
                    new Students {
                        Id = 1,
                        StudentCode = "Student584199",
                        FirstName = "Tlee",
                        LastName = "Say hi",
                        Email = "thienle255@gmail.com",
                        Phone = "0905028073",
                        Address = "391 Nam Ky Khoi Nghia,Quan 3",
                        Gender = 0,
                        FatherName = "Connor",
                        MotherName = "Alvin",
                        Avatar = "Image/Staff/1.png",
                        DateOfBirth = new DateTime(2006, 01, 17),
                        Password = BCrypt.Net.BCrypt.HashPassword("T123456") }
                });
            });
            modelBuilder.Entity<StudentFacultySemesters>(sfs =>
            {
                sfs.HasKey(x => x.Id);

               // sfs.HasOne(s => s.Student).WithOne().HasForeignKey<StudentFacultySemesters>(s => s.StudentId);
                //sfs.HasOne(f => f.Faculty).WithOne().HasForeignKey<StudentFacultySemesters>(s => s.FacultyId);
                //sfs.HasOne(f => f.Semester).WithMany(a => a.).HasForeignKey<StudentFacultySemesters>(s => s.SemesterId);
                //sfs.HasOne(f => f.Session).WithOne().HasForeignKey<StudentFacultySemesters>(s => s.SessionId);
                sfs.HasOne(f => f.Session).WithMany(s => s.StudentFacultySemesters).HasForeignKey(sfs => sfs.SessionId);
                sfs.HasOne(f => f.Semester).WithMany(s => s.StudentFacultySemesters).HasForeignKey(sfs => sfs.SemesterId);
                sfs.HasOne(f => f.Faculty).WithMany(s => s.StudentFacultySemesters).HasForeignKey(sfs => sfs.FacultyId);
                sfs.HasData(new StudentFacultySemesters[]
                {
                    new StudentFacultySemesters 
                    { 
                        Id = 1,
                        StudentId =  1,
                        FacultyId = 1,
                        SemesterId = 1,
                        SessionId = 1,
                    }
                });

            });
            modelBuilder.Entity<Admission>(c =>
            {
                c.HasKey(x => x.Id);
                c.HasData(new Admission[]
                {
                    new Admission{Id = 1, FirstName = "Nguyen", LastName = "Quan", Email = "abc@gmail.com", Phone = "1213123", FatherName = "ABC", MotherName = "DEF", DOB = new DateTime(2004, 08, 25), Address = "HCM", Gender = true, HighSchool = "FPT", EnrollmentNumber = "C123", GPA = 5.0, Status = "Process", FacultyId = 1},
                    new Admission{Id = 2, FirstName = "ABC", LastName = "XYZ", Email = "abc2@gmail.com", Phone = "345345", FatherName = "ABC2", MotherName = "DEF2", DOB = new DateTime(2004, 08, 29), Address = "HCM2", Gender = false, HighSchool = "FPT2", EnrollmentNumber = "C345", GPA = 4.0, Status = "Process", FacultyId = 2}
                });
            });
        }
    }
}
