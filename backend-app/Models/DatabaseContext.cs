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
        public DbSet<DepartmentSemesterSession> departmentSemesterSessions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>(c =>
            {
                c.HasKey(x => x.Id);
                c.HasIndex(a => a.Name).IsUnique();
                c.HasData(new Category[]
                {
                    new Category { Id = 1, Name = "Astronomy news"},
                    new Category { Id = 2, Name = "Aviation news"},
                    new Category { Id = 3, Name = "Business news"},
                    new Category { Id = 4, Name = "Design and architecture news"},
                    new Category { Id = 5, Name = "Education news"},
                    new Category { Id = 6, Name = "Engineering news"},
                    new Category { Id = 7, Name = "Film and television news"},
                    new Category { Id = 8, Name = "Health news"},
                    new Category { Id = 9, Name = "Law news"},
                    new Category { Id = 10, Name = "Politics news"},
                    new Category { Id = 11, Name = "Science news"},
                    new Category { Id = 12, Name = "Social affairs news"},
                    new Category { Id = 13, Name = "Sustainability news"},
                    new Category { Id = 14, Name = "Technology news"},
                    new Category { Id = 15, Name = "Trades news"},
                    new Category { Id = 16, Name = "University news"}
                });
            });
            modelBuilder.Entity<Article>(c =>
            {
                c.HasKey(x => x.Id);
                c.HasIndex(a => a.Title).IsUnique();
                c.HasData(new Article[]
                { 
                    new Article
                    { 
                        Id = 1, 
                        Title = "What does the ‘common good’ actually mean? Our research found common ground across the political divide",
                        Content = "Some topics are hard to define. They are nebulous; their meanings are elusive. Topics relating to morality fit this description. So do those that are subjective, meaning different things to different people in different contexts.\r\n\r\nIn our recently published paper, we targeted the nebulous concept of the “common good”.\r\n\r\nLike moral issues that elicit strong arguments for and against, conceptualisations of the common good can vary according to the different needs of individuals and the different values they hold. One factor that divides people is political orientation. Those on the far left hold very different opinions on moral and social issues than those on the far right.\r\n\r\nHow can we expect people across the political spectrum to agree on a moral topic when they have such different perspectives?\r\n\r\nIf we set aside the specific moral issues and focus instead on the broader aspects of the common good as a concept, we may well find foundational principles – ideas that are shared between people, ideas that are perhaps even universal.\r\n\r\nFolk theory\r\nTo find such underlying commonalities, we used a social psychological folk theory approach. Folk theories are non-academic or lay beliefs that comprise individuals’ informal and subjective understandings of their world.\r\n\r\nThe concept of the common good bleeds into cultural perceptions and worldviews. The currency of such ideas influences how we think and what we talk about with other people. By asking people to write about or define elusive concepts, social psychologists can search for frequently expressed words and phrases and derive a shared cultural understanding from the collection of individual texts.\r\n\r\nWe asked 14,303 people who participated in a larger study for the Australian Leadership Index to provide a definition of the common good, also sometimes called the greater good or the public good.\r\n\r\nThe sample was nationally representative, meaning it reflected the demographics of the Australian population at the time the data was collected. We then used a linguistic analysis tool, called the Linguistic Inquiry and Word Count program, to analyse the responses.\r\n\r\nThe program has a new function called the Meaning Extraction Method, which processes large bodies of text to identify prevalent themes or concepts by analysing words that frequently occur in close proximity.\r\n\r\nUsing this method, we explored Australians’ definitions of the common good. From the word clusters derived from this analysis, we identified nine main themes:\r\n\r\nOutcomes that are in the best interest of the majority\r\nDecisions and actions that benefit the majority\r\nThat which is in the best interest of the general public\r\nThat which serves the general national population rather than individual interests\r\nThat which serves the majority rather than minority interests\r\nThat which serves group rather than individual interests\r\nThat which serves citizens’ interests\r\nConcern for and doing the right thing for all people\r\nMoral principles required to achieve the common good\r\nInterestingly, these broad themes did not differ for the most part between right-leaning and left-leaning participants, meaning they were shared by liberals and conservatives alike. There is indeed common ground in people’s understanding of the common good.\r\n\r\nA working definition\r\nThese nine themes thus reflect a deeper conceptual structure. They can be distilled into three core aspects of the common good. These relate to outcomes, principles and stakeholders.\r\n\r\nThe first describes the objectives and outcomes associated with the common good – for example, the decisions and actions that are seen to be in the best interests of most people.\r\n\r\nThe second refers to the principles associated with the common good and the processes and practices through which the common good is realised.\r\n\r\nThe final aspect relates to the stakeholders who make up the community or communities that are entitled to the common good and its benefits.\r\n\r\nFrom this we arrived at a working definition of the common good:\r\n\r\nThe common good refers to achieving the best possible outcome for the largest number of people, which is underpinned by decision-making that is ethically and morally sound and varies by the context in which the decisions are made.\r\n\r\nIn the definition above, you will detect the nine components, as well as the three broader themes.\r\n\r\nWhile we identified a shared understanding of the common good, it is important to acknowledge that people may share the “big picture” of the common good, but differ when it comes to the social and moral issues they prioritise and the practical ways in which they think the common good should be achieved.\r\n\r\nFor instance, recent research suggests that people care deeply about fairness, but society is divided by how they view fairness concerns.\r\n\r\nOn one side, you have the social order perspective, which focuses on processes or how justice is achieved. On the other side, the social justice worldview is concerned with outcomes and what justice looks like as a result. Both sides share a disdain for inequality, but don’t often see eye to eye about naming or fixing societal inequality.\r\n\r\nIf the two sides were willing to start by finding their common ground, using our working definition to probe for areas of convergence first, then moving on to discuss areas of divergence with an openness to learn from each other’s strengths might become possible. Intractable conflicts could be broken down and systematically addressed. Of course, this requires a willingness from both sides to lower their defences and listen.\r\n\r\nCommunity leaders will encounter challenges when they unite to advance the common good. Leaders from different industries bring different backgrounds, education and priorities to the table. In order to integrate their efforts, it becomes essential to set aside contextual (and often biased or partisan) understandings of the common good to focus on the “big picture”.",
                        image = "Image/Article/1.jpeg",
                        PublishDate = DateTime.Now
                    }, 
                    new Article
                    {
                        Id = 2,
                        Title = "The Solar System used to have nine planets. Maybe it still does? Here’s your catch-up on space today",
                        Content = "Some of us remember August 24 2006 like it was yesterday. It was the day Pluto got booted from the exclusive “planets club”.\r\n\r\nI (Sara) was 11 years old, and my entire class began lunch break by passionately chanting “Pluto is a planet” in protest of the information we’d just received. It was a touching display. At the time, 11-year-old me was outraged – even somewhat inconsolable. Now, a much older me wholeheartedly accepts: Pluto is not a planet.\r\n\r\nSimilar to Sara, I (Rebecca) vividly remember Pluto’s re-designation to dwarf status. For me, it wasn’t so much that the celestial body had been reclassified. That is science, after all, and things change with new knowledge. Rather, what got to me was how the astronomy community handled the PR.\r\n\r\nEven popular astronomers known for their public persona stumbled through mostly unapologetic explanations. It was a missed opportunity. What was poorly communicated as a demotion was actually the discovery of new exciting members of our Solar System, of which Pluto was the first.\r\n\r\nThe good news is astronomers have better media support now, and there’s a lot of amazing science to catch up on. Let’s go over what you might have missed.\r\n\r\nImage of the surface on Pluto\r\nPluto didn’t meet the criteria of a fully fledged planet. But there may still be a 9th planet in our Solar System waiting to be found. Image: Shutterstock\r\n\r\nA throwback to a shocking demotion\r\nPluto’s fate was almost certainly sealed the day Eris was discovered in 2005. Like Pluto, Eris orbits in the outskirts of our Solar System. Although it has a smaller radius than Pluto, it has more mass.\r\n\r\nAstronomers concluded that discovering objects such as Pluto and Eris would only become more common as our telescopes became more powerful. They were right. Today there are five known dwarf planets in the Solar System.\r\n\r\nThe conditions for what classifies a “planet” as opposed to a “dwarf planet” were set by the International Astronomical Union. To cut a long story short, Pluto wasn’t being targeted back in 2006. It just didn’t meet all three criteria for a fully fledged planet:\r\n\r\nit must orbit a star (in our Solar System this would be the Sun)\r\nit must be big enough that gravity has forced it into a spherical shape\r\nit must be big enough that its own gravity has cleared away any other objects of a similar size near its orbit.\r\nThe third criterion was Pluto’s downfall. It hasn’t cleared its neighbouring region of other objects.\r\n\r\nSo is our Solar System fated to have just eight planets? Not necessarily. There may be another one waiting to be found.\r\n\r\nIs there a Planet Nine out there?\r\nWith the discovery of new and distant dwarf planets, astronomers eventually realised the dwarf planets’ motions around the Sun didn’t quite add up.\r\n\r\nWe can use complicated simulations in supercomputers to model how gravitational interactions would play out in a complex environment such as our Solar System.\r\n\r\nIn 2016, California Institute of Technology astronomers Konstantin Batygin and Mike Brown concluded – after modelling the dwarf planets and their observed paths – that mathematically there ought be a ninth planet out there.\r\n\r\nTheir modelling determined this planet would have to be about ten times the mass of Earth, and located some 90 billion kilometres away from the Sun (about 15 times farther then Pluto). It’s a pretty bold claim, and some remain sceptical.\r\n\r\nOne might assume it’s easy to determine whether such a planet exists. Just point a telescope towards where you think it is and look, right? If we can see galaxies billions of light years away, shouldn’t we be able to spot a ninth planet in our own Solar System?\r\n\r\nWell, the issue lies in how (not) bright this theoretical planet would be. Best estimates suggest it sits at the depth limit of Earth’s largest telescopes. In other words, it could be 600 times fainter than Pluto.\r\n\r\nThe other issue is we don’t know exactly where to look. Our Solar System is really big, and it would take a significant amount of time to cover the entire sky region in which Planet Nine might be hiding. To further complicate things, there’s only a small window each year during which conditions are just right for this search.\r\n\r\nThat isn’t stopping us from looking, though. In 2021, a team using the Atacama Cosmology Telescope (a millimetre-wave radio telescope) published the results from their search for a ninth planet’s movement in the outskirts of the Solar System.\r\n\r\nWhile they weren’t able to confirm its existence, they provided ten candidates for further follow-up. We may only be a few years from knowing what lurks in the outskirts of our planetary neighbourhood.\r\n\r\nThe ACT sits at an altitude of 5,190 meters in Chile’s Atacama desert. Here, the lack of atmospheric water vapour helps to increase its accuracy.\r\nThe ACT sits at an altitude of 5,190 meters in Chile’s Atacama desert. Here, the lack of atmospheric water vapour helps to increase its accuracy. Image: NIST/ACT Collaboration\r\n\r\nFinding exoplanets\r\nEven though we have telescopes that can reveal galaxies from the universe’s earliest years, we still can’t easily directly image planets outside of our Solar System, also called exoplanets.\r\n\r\nThe reason can be found in fundamental physics. Planets emit very dim red wavelengths of light, so we can only see them clearly when they’re reflecting the light of their star. The farther away a planet is from its star, the harder it is to see.\r\n\r\nAstronomers knew they’d have to find other ways to look for planets in foreign star systems. Before Pluto was reclassified they had already detected the first exoplanet, 51 Pegasi B, using a radial velocity method.\r\n\r\nThis gas giant world is large enough, and close enough to its star, that the gravitational tug of war between the two can be detected all the way from Earth. However, this method of discovery is tedious and challenging from Earth’s surface.\r\n\r\nSo astronomers came up with another way to find exoplanets: the transit method. When Mercury or Venus pass in front of the Sun, they block a small amount of the Sun’s light. With powerful telescopes, we can look for this phenomenon in distant star systems as well.",
                        image = "Image/Article/2.jpeg",
                        PublishDate = DateTime.Now
                    },
                    new Article
                    {
                        Id = 3,
                        Title = "Is linking time in the office to career success the best way to get us back to work?",
                        Content = "Working from home introduced in response to the harsh pandemic lockdowns in 2020 was expected to be a short term arrangement with staff returning to the office as soon as restrictions were lifted.\r\n\r\nYet, almost four years later, most office workers are still following hybrid arrangements - splitting their week between home and office, with no plans to return full-time to the workplace anytime soon.\r\n\r\nIn what some employees consider an aggressive move by their bosses to get them back where they can be seen, some companies are now linking office attendance to pay, bonuses and even promotions.\r\n\r\nIt pays, for some, to return to the office\r\nLinking office attendance with pay has taken off after Citibank workers in the UK were told last September their bonuses could be affected if they didn’t work a minimum of three days per week from the office.\r\n\r\nIn Australia Origin and Suncorp, have done the same thing, as has ANZ where staff are required to work at least half their hours – averaged over a calendar month – in the office.\r\n\r\nIf these conditions are not met, it may be taken into consideration in performance and remuneration reviews at the end of the next year.\r\n\r\n“If you are one of our people who are yet to be spending more than half your time in the workplace, we need you to adjust your patterns unless you have a formal exception in place,” an internal email to ANZ staff said.\r\n\r\nIn the US, Amazon has told corporate employees they may miss out on promotion if they ignore the company’s return-to-office mandate, which requires employees to be in the office at least three days a week.\r\n\r\nA post on Amazon’s internal website viewed by CNBC said:\r\n\r\nManagers own the promotion process, which means it is their responsibility to support your growth through regular conversations and stretch assignments, and to complete all the required inputs for a promotion\r\n\r\nIf your role is expected to work from the office 3+ days a week and you are not in compliance, your manager will be made aware and VP approval will be required.\r\n\r\nNot everyone is happy\r\nTo say the reaction to these measures has been divisive is an understatement. Up to now, some hybrid work arrangements may have been ill-defined, and employee expectations confusing.\r\n\r\nSome employees will miss out on promotions and bonuses if they refuse to spend at least part of their working week in the office. \r\nSome employees will miss out on promotions and bonuses if they refuse to spend at least part of their working week in the office. Image: PressMaster/Shutterstock\r\n\r\nThe messaging offered here is clear, employees know what is expected of them in terms of office attendance, and the repercussions they may face if they don’t meet those expectations.\r\n\r\nAnd it’s important to remember that these initiatives are only aimed at incentivising workers to attend the office for part of the week, typically 2-3 days out of 5, which still represents a significant flexibility gain compared to what these firms offered before the pandemic.\r\n\r\nIs showing up the best measure of performance?\r\nHowever, critics have raised concerns that linking attendance to pay could hurt high achievers who don’t meet their in-office quotas - will they miss out on bonuses or a promotion simply because they don’t show up to the office enough, regardless of how well they are doing their job otherwise?\r\n\r\nIs office attendance really that important, compared to other performance and outcome metrics, and will employees feel they are being treated like school children?\r\n\r\nThere are also fears about the impact strict attendance requirements will have on diversity, with women, parents, and people with neurodiverse needs more likely to favour a higher proportion of remote working.\r\n\r\nAdditionally, monitoring and managing attendance creates additional work for managers, and could lead to regular awkward conversations about attendance expectations.\r\n\r\nMeasuring office attendance may not be as simple as it first sounds either.\r\n\r\nIf an employee is required to maintain an average of 50% office attendance and they are invited to visit a client interstate for a day, or travel overseas to present at a conference, do these count as “in office days” or “WFH” days? This needs to be established and communicated to staff in writing.\r\n\r\nOne-size doesn’t fit all\r\nWith hybrid work arrangements there is no one right or wrong strategy. Different companies will take different approaches, based on the specific needs of their particular organisation and staff, and only time will tell how successful their respective strategies prove to be.\r\n\r\nWhat we can be certain of is the fact hybrid work will not be disappearing anytime soon, so the focus for 2024 needs to be how to make this arrangement as efficient as possible, rather than trying to turn the clock back to 2019.\r\n\r\nThis article was originally published on The Conversation.",
                        image = "Image/Article/3.jpeg",
                        PublishDate = DateTime.Now
                    },
                    new Article
                    {
                        Id = 4,
                        Title = "Magazines were supposed to die in the digital age. Why haven’t they?",
                        Content = "n the classic comedy Ghostbusters (1984), newly hired secretary Janice raises the subject of reading, while idly flipping through the pages of a magazine. The scientist Egon Spengler responds with a brusque dismissal: “print is dead.”\r\n\r\nEgon’s words now seem prescient. The prevailing assumption of the past couple of decades is that print media is being slowly throttled by the rise of digital. Print magazines, in particular, are often perceived as being under threat.\r\n\r\nWhile not nearly as popular as they once were, magazines haven’t died. New ones have started since the dire predictions began, while others continue to attract loyal readerships.\r\n\r\nSo what’s the enduring appeal of the print magazine? Why didn’t it die, as so many predicted?\r\n\r\nPrinted words in an online world\r\nThe word “magazine” derives from the term for a warehouse or storehouse. In its essence, it is any publication that collects different types of writing for readers. Each instalment includes a range of voices, subjects and perspectives.\r\n\r\nPrint magazine culture has certainly seen a decline since its heyday in the 20th century. Once-popular print magazines have moved entirely online or are largely sustained by growing digital subscriptions.\r\n\r\nElsewhere, internet media sites, of the type pioneered by Buzzfeed and its imitators, increasingly fulfil the need for diverse and distracting short-form writing.\r\n\r\nThe explosion of social media has also cut into the advertising market on which print magazines have traditionally depended.\r\n\r\nOnline audiences have come to expect new content daily or even hourly. Casual readers are less willing to wait for a weekly or monthly print magazine to arrive in the post or on a newsstand. The ready availability of free, or significantly cheaper, digital content may deter them from purchasing print subscriptions or individual issues. Turning from screens to the page\r\nAnd yet print magazines refuse to die. Established periodicals, such as the New Yorker and Vogue, stubbornly cling to a global readership in both print and digital formats.\r\n\r\nNew titles are emerging as well – 2021 saw the launch of 122 new print magazines in the United States alone. The number is smaller than some previous years, and this perhaps reflects the generally shrinking market for print media.\r\n\r\nBut given the accepted wisdom, it is remarkable there are any new periodicals at all.\r\n\r\nIn Australia, print magazines sales have risen 4.1% in 2023 and previously axed publications – such as Girlfriend – are now receiving one-off, nostalgic returns to print.\r\n\r\nThe market for print magazines isn’t exactly thriving. But they haven’t vanished as quickly as anticipated.\r\n\r\nSome commentators have attributed the enduring appeal of print magazines to the physical experience of reading. We absorb information differently from the page than from the screen, perhaps in a less frantic and distractable way.\r\n\r\n“Digital fatigue” from the years of the pandemic has arguably resulted in a small pivot back to print media. The revived interest in print magazines has also been attributed to the “analog” preferences of Gen Z readers.\r\n\r\nAs the writer Hope Corrigan has noted, there is also something appealing about the aesthetics of print magazines. The care taken with layout, images and copy can’t always be replicated on as screen. Indeed, magazines with a significant focus on photography and visual design – such as fashion and travel magazines – are enduring in print.\r\n\r\nMagazine expert Samir Husni has observed that emerging independent print magazines are more focused on targeting a niche readership. Advances in printing technology have made smaller print runs more cost-effective. This allows new magazines to focus on quality over quantity.\r\n\r\nThe new wave of print magazines tend to have a higher cover price and standard of production. They are also published less frequently, with quarterly or biannual schedules becoming more common.\r\n\r\nWhat was old is cool again?\r\nThis trend moves away from the idea of magazines as cheap and disposable. Rather, it reframes them as a luxury product.\r\n\r\nPrint magazines cannot compete with digital media in providing constantly up-to-date content to a mass audience. But they can potentially maintain a dedicated readership with a meaningful and aesthetically pleasing publication.\r\n\r\nThis means print magazines may be spared some of the turbulence suffered by media websites that are solely dependent on digital advertising revenue. The past few years have seen staffing upheavals, mass resignations and shutdowns at popular magazine-style websites such as Deadspin, the Onion AV Club, the Escapist and Jezebel (although the latter has since returned). The original vision and standards for these sites have arguably suffered from the constant drive to increase daily traffic and reduce costs. Print magazines may also be seeing a revived interest from advertisers. Recent research indicates a strong preference for print advertising among consumers. Readers are far more likely to pay attention to a print advertisement and trust its content. By contrast, online advertising is more likely to be ignored or dismissed.\r\n\r\nIn a 2021 profile of magazine collector Steven Lomazow, Nathan Heller writes:\r\n\r\n[…] what made magazines appealing in 1720 is the same thing that made them appealing in 1920 and in 2020: a blend of iconoclasm and authority, novelty and continuity, marketability and creativity, social engagement and personal voice.\r\n\r\nWhile the circulation and influence of print magazines may have reduced, they are not necessarily dead or even dying. They can be seen as moving into a smaller, but sustainable, place in the media landscape.",
                        image = "Image/Article/4.jpeg",
                        PublishDate = DateTime.Now
                    }
                });
           });
            modelBuilder.Entity<ArticleCategory>(c =>
            {
                c.HasKey(x => x.Id);
                c.HasOne(a => a.Category).WithMany(b => b.ArticleCategories).HasForeignKey(c => c.CategoryId);
                c.HasOne(a => a.Article).WithMany(b => b.ArticleCategories).HasForeignKey(c => c.ArticleId);
                c.HasData(new ArticleCategory[]
                {
                    new ArticleCategory { Id = 1, ArticleId = 1, CategoryId=1 },
                    new ArticleCategory { Id = 2, ArticleId = 1, CategoryId=3 },
                    new ArticleCategory { Id = 3, ArticleId = 1, CategoryId=6 },
                    new ArticleCategory { Id = 4, ArticleId = 1, CategoryId=10 },
                    new ArticleCategory { Id = 5, ArticleId = 1, CategoryId=11 },
                    new ArticleCategory { Id = 6, ArticleId = 1, CategoryId=9 },

                    new ArticleCategory { Id = 7, ArticleId = 2, CategoryId=11 },
                    new ArticleCategory { Id = 8, ArticleId = 2, CategoryId=2 },
                    new ArticleCategory { Id = 9, ArticleId = 2, CategoryId=4 },
                    new ArticleCategory { Id = 10, ArticleId = 2, CategoryId=7 },
                    new ArticleCategory { Id = 11, ArticleId = 2, CategoryId=12 },

                    new ArticleCategory { Id = 12, ArticleId = 3, CategoryId=13 },
                    new ArticleCategory { Id = 13, ArticleId = 3, CategoryId=15 },
                    new ArticleCategory { Id = 14, ArticleId = 3, CategoryId=16 },
                    new ArticleCategory { Id = 15, ArticleId = 3, CategoryId=3 },
                    new ArticleCategory { Id = 16, ArticleId = 3, CategoryId=2 },

                    new ArticleCategory { Id = 17, ArticleId = 4, CategoryId=2 },
                    new ArticleCategory { Id = 18, ArticleId = 4, CategoryId=4 },
                    new ArticleCategory { Id = 19, ArticleId = 4, CategoryId=7 },
                    new ArticleCategory { Id = 20, ArticleId = 4, CategoryId=13 },
                    new ArticleCategory { Id = 21, ArticleId = 4, CategoryId=12 },
                });
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
                    new Courses
                    {
                        Id = 1, 
                        Name = "Applied Innovation", 
                        Slug = "Applied-Innovation",
                        Description = "Don't just graduate, innovate. The Bachelor of Applied Innovation aims to make you think like an innovator, explore bold ideas, and create unprecedented solutions."
                    },
                    new Courses
                    {
                        Id = 2, 
                        Name = "Humanities and Social Sciences",
                        Slug = "Arts-Humanities-and-Social-Sciences",
                        Description = "Become the innovative thinker our world needs. Explore the relationships between individuals, societies and cultures and be ready to impact the challenges faced by our rapidly evolving world today and in the future."
                    },
                    new Courses
                    {
                        Id = 3, 
                        Name = "Aviation", 
                        Slug = "Aviation",
                        Description = "The thrill of taking to the skies is one of humankind’s greatest achievements. A hundred years ago we barely knew how. Now we depend on it every day."
                    },
                    new Courses
                    {
                        Id = 4, 
                        Name = "Built Environment and Architecture", 
                        Slug = "Built-Environment-and-Architecture",
                        Description = "Learn how to harness your spatial creativity in design and bring together the environment and architecture to create innovative spaces for us all to enjoy.6"
                    },
                    new Courses
                    {
                        Id = 5, 
                        Name = "Business", 
                        Slug = "Business",
                        Description = "Plug straight into innovation with our tech-led business courses and degrees. With seamless industry connections, you could follow grads and land a role in a profit or purpose-based business."
                    },
                    new Courses
                    {
                        Id = 6, 
                        Name = "Design", 
                        Slug = "Design",
                        Description = "Define the spaces we live in, the products we purchase, and the online and real-life worlds we explore. Bottle your imagination and creativity and pour it into a career in design."
                    },
                    new Courses
                    {
                        Id = 7, 
                        Name = "Engineering", 
                        Slug = "Engineering",
                        Description = "Engineers have the power to change how we live — and with that power comes great responsibility."
                    },
                    new Courses
                    {
                        Id = 8, 
                        Name = "Film and Television", 
                        Slug = "Film-and-Television",
                        Description = "The fundamentals of storytelling through film are evolving, the industry is changing and so are the audiences. "
                    },
                    new Courses
                    {
                        Id = 9, 
                        Name = "Games and Animation", 
                        Slug = "Games-and-Animation",
                        Description = "Open your mind to creating for the digital space and play a role bringing new stories, characters and worlds to life with a course in games and animation."
                    },
                    new Courses
                    {
                        Id = 10, 
                        Name = "Information Technology", 
                        Slug = "Information-Technology",
                        Description = "There are two types of people in the world: those who understand binary, and those who don't. In a modern economy, you want to be one that does."
                    },
                    new Courses
                    {
                        Id = 11,
                        Name = "Media and Communication",
                        Slug = "Media-and-Communication",
                        Description = "We live in a world that’s more connected than ever before. Understanding how that world works is essential to creating sustainable news and entertainment for modern media."
                    },
                    new Courses
                    {
                        Id = 12, 
                        Name = "Psychology", 
                        Slug = "Psychology",
                        Description = "We know more about the mind than ever before, yet many of its mysteries remain unsolved and therefore, so too, do many aspects of human behaviour. "
                    },
                    new Courses
                    {
                        Id = 13, 
                        Name = "Science", 
                        Slug = "Science",
                        Description = "Science is the pursuit of truth and understanding of our world and beyond. Science never stops evolving and you’ll never stop learning."
                    },
                    new Courses
                    {
                        Id = 14,
                        Name = "Education", 
                        Slug = "Education",
                        Description = "In our lives, we are surrounded by teachers, from parents to friends and neighbours, but few get the opportunity to impart daily wisdom as trained educators do."
                    }
                });
            });
            modelBuilder.Entity<Faculty>(c =>
            {
                c.HasKey(x => x.Id);
                c.HasOne(a => a.Courses).WithMany(b => b.Faculty).HasForeignKey(c => c.Course_id);
                c.HasData(new Faculty[]
                {
                    new Faculty
                    {
                        Id = 1, 
                        Code = "3400234640", 
                        Title = "Bachelor of Business Analytics and Analysis", 
                        Slug="Bachelor-of-Aviation-Management/Bachelor of Applied Innovation", 
                        Description="Become a sought-after agent of change in the breakneck business world. Learn how to interpret and analyse business data, discover patterns and spot opportunity where others see tumult. Up your leadership game and emerge ready to solve people, process, technology and strategy challenges. ", 
                        EntryScore=65, 
                        Skill_learn = "Business operations optimisation skills, Digital literacy, Critical thinking, Evaluate and analyse data", 
                        Opportunities = "Systems analyst or architect, UX analyst, Business analyst, Technical business analyst, Requirements analyst, Process consultant", 
                        Course_id = 1,
                        Image = "Image/Faculty/1.jpeg"
                    },
                    new Faculty
                    {
                        Id = 2,
                        Code = "3400234642",
                        Title = "Bachelor of Business",
                        Slug="Bachelor-of-Business",
                        Description="Step into a world of opportunity with our Bachelor of Business degree. Tailor your path with a broad range of majors, ranging from Finance to Entrepreneurship. Experience hands-on learning and develop your digital skills across leading platforms like MYOB, Xero, and Google Analytics. Contribute to sustainability projects and make a positive impact on the world and others through ethical business practices. Connect with industry experts and mentors in your areas of interest for unparalleled personal growth and networking opportunities.​",
                        EntryScore=60,
                        Skill_learn = "Communication, Leadership, Critical thinking, Stakeholder management, Decision making, Teamwork & collaboration",
                        Opportunities = "Project manager, Administrator, Business manager, Customer service manager, Change manage, Planning manager",
                        Course_id = 1,
                        Image = "Image/Faculty/2.jpeg"
                    },
                    new Faculty
                    {
                        Id = 3,
                        Code = "3400212651",
                        Title = "Bachelor of Cyber Security",
                        Slug="Bachelor-of-Cyber-Security",
                        Description="Cybercrime is rapidly on the rise. Get qualified and help fill the global shortage of cyber security professionals. Learn the fundamentals, including encryption systems, access control and how network architecture and the internet is used to defend online data. Prepare for an in-demand and well-paid career across industries – from defence to media, health, business and more.",
                        EntryScore=70,
                        Skill_learn = "Cyber security testing, Problem solving, Cyber security strategy, Communication skills, Critical thinking, Teamwork",
                        Opportunities = "Security consultant, Information security analyst, Network or systems administrator, Cyber security penetration tester",
                        Course_id = 1,
                        Image = "Image/Faculty/3.jpeg"

                    },
                    new Faculty
                    {
                        Id = 4,
                        Code = "3400234641",
                        Title = "Bachelor of Computer Science (Professional)",
                        Slug="Bachelor-of-Computer-Science",
                        Description="Be paid to study your passion with a professional degree that includes a 12-month paid work placement. Hone your expertise in software development and computer science, with foundational skills in hardware and operating systems. Learn in state-of-the-art labs and with the most up-to-date technology to ensure you stay ahead of the digital revolution.",
                        EntryScore=80,
                        Skill_learn = "Computer software development, Problem solving, Critical thinking, Machine learning application",
                        Opportunities = "Data scientist, Software engineer,Software architect, Programmer or software developer,Systems architect",
                        Course_id = 1,
                        Image = "Image/Faculty/4.jpeg"

                    },
                    new Faculty
                    {
                        Id = 5,
                        Code = "3400234771",
                        Title = "Bachelor of Computer Science games development",
                        Slug="Bachelor-of-Computer-Science-games-development",
                        Description="Crack the code for a rewarding career at the fore of the digital revolution. With a focus on software development, this course is taught by some of the best computer minds in the business. Learn in industry standard labs and choose to specialise with a major in Artificial Intelligence, Cyber security, Data Science, Games Development, Software Development or Internet of Things.",
                        EntryScore=70,
                        Skill_learn = "Computer software development, Multimedia creative design, Interactive software design and development, Software development using an object-oriented approach",
                        Opportunities = "Digital content producer, Multimedia developer, Games designer or programmer, Software designer or developer",
                        Course_id = 1,
                        Image = "Image/Faculty/5.jpeg"

                    },
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
                    new Staff 
                    {
                        Id = 1, 
                        FirstName = "Tlee",
                        LastName = "Tlee", 
                        Email = "thienle255@gmail.com",
                        Address = "391 Nam Ky Khoi Nghia,Quan 3", 
                        Gender = 0, Phone = "0905028073", 
                        FileAvatar = "Image/Staff/1.png", 
                        Qualification = "Admin", 
                        Experience = "Admin", 
                        Password = BCrypt.Net.BCrypt.HashPassword("Tlee2210"), 
                        Role="Admin"},
                    new Staff 
                    {
                        Id = 2, 
                        FirstName = "Chuong", 
                        LastName = "Chuong", 
                        Email = "namechuong19@gmail.com", 
                        Address = "391 Nam Ky Khoi Nghia,Quan 3", 
                        Gender = 0, 
                        Phone = "0974671412", 
                        FileAvatar = "Image/Staff/1.png", 
                        Qualification = "Admin", 
                        Experience = "Admin", 
                        Password = BCrypt.Net.BCrypt.HashPassword("chuong123"), 
                        Role="Admin"}
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
