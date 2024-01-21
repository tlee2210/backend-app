using Microsoft.EntityFrameworkCore;

namespace backend_app.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<adminAccount> AdminAccounts { get; set; }
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
        public DbSet<Students> students { get; set; }
        public DbSet<Semester> semesters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<adminAccount>(c =>
            {
                c.HasKey(x => x.Id);
                c.HasData(new adminAccount[]
                {
                    new adminAccount{Id=1, Name = "Tlee", Email = "Tlee2210@gmail.com", Password = BCrypt.Net.BCrypt.HashPassword("tlee123"), Role="Admin"}
                });
            });
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
            });
            modelBuilder.Entity<StudentFacultySemesters>(sfs =>
            {
                sfs.HasKey(x => x.Id);

                sfs.HasOne(s => s.Student).WithOne(sfs => sfs.StudentFacultySemesters).HasForeignKey<StudentFacultySemesters>(s => s.StudentId);

                sfs.HasOne(f => f.Faculty)
                   .WithMany(faculty => faculty.StudentFacultySemesters)
                   .HasForeignKey(s => s.FacultyId);

                sfs.HasOne(s => s.Semester)
                   .WithMany(semester => semester.StudentFacultySemesters)
                   .HasForeignKey(s => s.SemesterId);
            });

            modelBuilder.Entity<Students>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.StudentCode).IsRequired(); 
                entity.Property(e => e.FirstName).HasMaxLength(50);
                entity.Property(e => e.LastName).HasMaxLength(50);
                entity.Property(e => e.Email).IsRequired(); 
                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Gender).HasConversion<string>();
            });
            modelBuilder.Entity<Semester>(s =>
            {
                s.HasKey(k => k.Id);
            });
        }
    }
}
