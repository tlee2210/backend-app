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
                    new Courses{Id = 1, Name = "Applied Innovation", Description = "Don't just graduate, innovate. The Bachelor of Applied Innovation aims to make you think like an innovator, explore bold ideas, and create unprecedented solutions."},
                    new Courses{Id = 2, Name = "Arts, Humanities and Social Sciences", Description = "Become the innovative thinker our world needs. Explore the relationships between individuals, societies and cultures and be ready to impact the challenges faced by our rapidly evolving world today and in the future."},
                    new Courses{Id = 3, Name = "Aviation", Description = "The thrill of taking to the skies is one of humankind’s greatest achievements. A hundred years ago we barely knew how. Now we depend on it every day."},
                    new Courses{Id = 4, Name = "Built Environment and Architecture", Description = "Learn how to harness your spatial creativity in design and bring together the environment and architecture to create innovative spaces for us all to enjoy.6"},
                    new Courses{Id = 5, Name = "Business", Description = "Plug straight into innovation with our tech-led business courses and degrees. With seamless industry connections, you could follow grads and land a role in a profit or purpose-based business."},
                    new Courses{Id = 6, Name = "Design", Description = "Define the spaces we live in, the products we purchase, and the online and real-life worlds we explore. Bottle your imagination and creativity and pour it into a career in design."},
                    new Courses{Id = 7, Name = "Engineering", Description = "Engineers have the power to change how we live — and with that power comes great responsibility."},
                    new Courses{Id = 8, Name = "Film and Television", Description = "The fundamentals of storytelling through film are evolving, the industry is changing and so are the audiences. "},
                    new Courses{Id = 9, Name = "Games and Animation", Description = "Open your mind to creating for the digital space and play a role bringing new stories, characters and worlds to life with a course in games and animation."},
                    new Courses{Id = 10, Name = "Information Technology", Description = "There are two types of people in the world: those who understand binary, and those who don't. In a modern economy, you want to be one that does."},
                    new Courses{Id = 11, Name = "Media and Communication", Description = "We live in a world that’s more connected than ever before. Understanding how that world works is essential to creating sustainable news and entertainment for modern media."},
                    new Courses{Id = 12, Name = "Psychology", Description = "We know more about the mind than ever before, yet many of its mysteries remain unsolved and therefore, so too, do many aspects of human behaviour. "},
                    new Courses{Id = 13, Name = "Science", Description = "Science is the pursuit of truth and understanding of our world and beyond. Science never stops evolving and you’ll never stop learning."},
                    new Courses{Id = 14, Name = "Education", Description = "In our lives, we are surrounded by teachers, from parents to friends and neighbours, but few get the opportunity to impart daily wisdom as trained educators do."}
                });
            });
            modelBuilder.Entity<Faculty>(c =>
            {
                c.HasKey(x => x.Id);
                c.HasOne(a => a.Courses).WithMany(b => b.Faculty).HasForeignKey(c => c.Course_id);
                c.HasData(new Faculty[]
                {
                    new Faculty{
                        Id = 1, 
                        Code = "3400234641", 
                        Title = "Bachelor of Business Analytics and Analysis", 
                        Slug="IT", 
                        Description="Become a sought-after agent of change in the breakneck business world. Learn how to interpret and analyse business data, discover patterns and spot opportunity where others see tumult. Up your leadership game and emerge ready to solve people, process, technology and strategy challenges. ", 
                        EntryScore=100, 
                        Skill_learn = "Business operations optimisation skills, Digital literacy, Critical thinking, Evaluate and analyse data", 
                        Opportunities = "Systems analyst or architect, UX analyst, Business analyst, Technical business analyst, Requirements analyst, Process consultant", 
                        Course_id = 1
                    },
                    new Faculty{Id = 2, Code = "C02", Title = "Philosophy", Slug="Philosophy", Description="123", EntryScore=100, Skill_learn = "XYZ", Opportunities = "123", Course_id = 1},
                    new Faculty{Id = 3, Code = "C03", Title = "Advanced Math", Slug="Advanced-Math", Description="123", EntryScore=100, Skill_learn = "Math", Opportunities = "456", Course_id = 2}
                });
            });
            modelBuilder.Entity<Department>(c =>
            {
                c.HasKey(x => x.Id);
                c.HasData(new Department[]
                {
                    new Department{
                        Id = 1, 
                        Code = "ICT10013", 
                        Subject = "Programming Concepts", 
                        Description = "Students are introduced to basic structured programming concepts needed for programming development in a variety of environments such as spreadsheets, web, desktop and mobile applications. Students will apply basic design and useability concepts to simple applications."
                    },
                    new Department{
                        Id = 2, 
                        Code = "INF10024", 
                        Subject = "Business Digitalisation", 
                        Description = "This unit aims to instill an appreciation of how technology can be used to assist business in the era of digitalization, without the technology becoming an end in itself. In particular, the unit aims to generate an awareness of the importance of digital technologies and information to organisational decision-making. Further, we examine how managers and practitioners can ensure the fitness-for-purpose of digital technologies and information to the decision makers such that business might gain a competitive advantage in digitalized world. Students gain a strong foundation of business systems fundamentals and an appreciation of how digital technologies impact business stakeholders, customers, suppliers, manufacturers, service makers, regulators, managers and employees."},
                    new Department{
                        Id = 3, 
                        Code = "INF10025", 
                        Subject = "Data Management and Analytics", 
                        Description = "This unit will provide a solid foundation for the design, implementation and management of organisational databases. Organisational data and data modelling is introduced, focusing on structured and unstructured data, and entity-relationship (ER) modelling. The skills required to construct ER diagrams will be taught, with a focus on ensuring that the logic of the model reflects the real-world industry case it is representing. Relational databases will be introduced and the functionality they afford organisations will be explored through real world industry cases. The process of designing, building and retrieving information from a database using SQL will be a focus of this unit. The unit also introduces students to the role databases play in data analytics and helping organisations harness the power of and insights from their data.\r\n\r\n"
                    },
                    new Department{
                        Id = 4, 
                        Code = "AVA10012", 
                        Subject = "First Year Industry Project", 
                        Description = "This unit introduces students to the challenges faced by industry professionals as they intervene in problems that require analytical thinking, creativity, interpersonal skills and resourcefulness to solve. This unit challenges students to explore different approaches to analysing and solving real-world problems in an organisational context. Students will develop the ability to apply analysis techniques to unfamiliar business problems and present their investigation through the use of a wide range of innovative Information Communication Technologies [ICT], including prototyping, cloud-based tools, report writing and presentations. In addition, students are encouraged to reflect upon the learning taking place throughout the unit."},
                    new Department{
                        Id = 5, 
                        Code = "BUS10012", 
                        Subject = "Innovative Business Practice", 
                        Description = "This unit has originated from a desire to give students an inspirational and highly engaging educational experience. It is infused with real-world examples and will provide a connection to industry professionals. The unit is designed to prepare students for their studies and work. Innovative Business Practice focuses on self-awareness, the development of a professional identity, communication and the development of effective teamwork skills. The role of innovation and how it can be leveraged to effectively achieve organisational objectives and positive social impact is a core theme with students encouraged to use curiosity and creativity to explore opportunities and to evaluate these, whilst displaying awareness of organisational and societal needs."},
                    new Department{
                        Id = 6, 
                        Code = "INF20029", 
                        Subject = "Digital Business Analysis and Design", 
                        Description = "This unit provides students with the systems analysis and design methods, tools and practices to stand out as effective business analysts in digitally enabled organisations. It covers various systems development lifecycles, methodologies, techniques and tools, exploring real-world industry cases in which they succeed and fail. Factors affecting the success of these methods in contemporary organisations are examined, along with comparisons of the values and principles that underlie these methods. After completing this unit, students will be able to understand and analyse real world digitalisation problems using modelling techniques to identify system requirements.\r\n\r\n"},
                    new Department{
                        Id = 7, 
                        Code = "MGT10009", 
                        Subject = "Contemporary Management Principles", 
                        Description = "This unit provides students with the foundational knowledge and skills concerning the role and functions of management. These frameworks support a critical analysis of individual or organisational operations and performance in the light of business opportunities and pressures, societal expectations and environmental contingencies. These insights enable students to identify their role as future managers, and to map their contribution to creating value at both an individual and organisational level."},
                    new Department{
                        Id = 8, 
                        Code = "INF20030", 
                        Subject = "Cloud Approaches for Enterprise Systems", 
                        Description = "This unit introduces students to the critical role cloud-based Enterprise Resource Planning (ERP) platforms play in supporting efficient and agile business processes. Organisations use ERPs for Customer Relationship Management, Supply Chain Management, Financial Management, as well as for bespoke business functions. The design, configuration, integration and operation of these â€˜software ecosystemsâ€™ is complicated by their scale and the complexity. Through real-world cases, this unit provides an overview of the global, social and economic motivations for cloud-based ERPs and addresses the strategic and managerial issues faced by organisations as they manage their virtual value chains. Students will examine the different types of cloud-based ERP service models (SaaS, PaaS) and explore the key challenges surrounding the management of cloud-based ERPs particularly process integration and data analytics enablement."},
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
            modelBuilder.Entity<DepartmentSemesterSession>(dss =>
            {
                dss.HasKey(x => x.Id);
                dss.HasOne(d => d.Faculty).WithMany().HasForeignKey(dss => dss.FacultyId);
                dss.HasOne(d => d.Department).WithMany().HasForeignKey(dss => dss.DepartmentId);
                dss.HasOne(d => d.Semester).WithMany().HasForeignKey(dss => dss.SemesterId);
                dss.HasOne(d => d.Session).WithMany().HasForeignKey(dss => dss.SessionId);
            });
        }
    }
}
