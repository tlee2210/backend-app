using Azure.Core;
using backend_app.DTO;
using backend_app.IRepository.dashboard;
using backend_app.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Reflection;

namespace backend_app.Services.dashboard
{
    public class ArticleServices : IArticle
    {
        private readonly DatabaseContext db;
        private readonly IWebHostEnvironment HostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ArticleServices(DatabaseContext db, IWebHostEnvironment HostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            this.HostEnvironment = HostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IEnumerable<ArticleDTO>> GetAllarticle()
        {
            var articles = await db.Articles
                .Include(a => a.ArticleCategories)
                .ThenInclude(ac => ac.Category)
                .ToListAsync();
            var request = _httpContextAccessor.HttpContext.Request;
            return articles.Select(a => new ArticleDTO
            {
                Id = a.Id,
                Title = a.Title,
                Content = a.Content,
                image = string.Format("{0}://{1}{2}/{3}", request.Scheme, request.Host, request.PathBase, a.image),

                PublishDate = a.PublishDate,
                Categories = a.ArticleCategories.Select(ac => new CategoryDTO
                {
                    Id = ac.Category.Id,
                    Name = ac.Category.Name
                }).ToList()
            });
        }

        public async Task<IEnumerable<SelectOption>> GetCreate()
        {
            var options = await db.Categories
                .Select(x => new SelectOption
                {
                    label = x.Name,
                    value = x.Id
                })
                .ToListAsync();

            return options;
        }
        public async Task<Article> store(ArticleImage articleImage)
        {
            var Article = new Article
            {
                Title = articleImage.Title,
                Content = articleImage.Content,
                image = await SaveImage(articleImage.image),
                PublishDate = DateTime.Now,
            };
            db.Articles.Add(Article);
            await db.SaveChangesAsync();

            var categoryIds = articleImage.Category.Split(',');
            foreach (var categoryIdString in categoryIds)
            {
                if (int.TryParse(categoryIdString, out var categoryId))
                {
                    var articleCategory = new ArticleCategory
                    {
                        ArticleId = Article.Id,
                        CategoryId = categoryId
                    };
                    db.ArticleCategories.Add(articleCategory);
                }
            }
            await db.SaveChangesAsync();
            return Article;
        }
        public async Task<bool> checktitle(string title)
        {
            return await db.Articles.AnyAsync(a => a.Title == title);
        }
        public async Task<bool> checkUpdate(string title, int id)
        {
            return await db.Articles.AnyAsync(a => a.Title == title && a.Id != id);
        }
        public async Task<GetEditSelectOption<ArticleDTO>> GetEditArticle(int id)
        {
            var article = await db.Articles
                .Include(a => a.ArticleCategories)
                .ThenInclude(ac => ac.Category)
                .FirstOrDefaultAsync(a => a.Id == id); 

            if (article == null)
            {
                return null; 
            }

            var request = _httpContextAccessor.HttpContext.Request;

            var articleDto = new ArticleDTO
            {
                Id = article.Id,
                Title = article.Title,
                Content = article.Content,
                image = string.Format("{0}://{1}{2}/{3}", request.Scheme, request.Host, request.PathBase, article.image),
                PublishDate = article.PublishDate,
                Categories = article.ArticleCategories.Select(ac => new CategoryDTO
                {
                    Id = ac.Category.Id,
                    Name = ac.Category.Name
                }).ToList()
            };
            var options = await db.Categories
               .Select(x => new SelectOption
               {
                   label = x.Name,
                   value = x.Id
               })
               .ToListAsync();
            var EditArticleDTO = new GetEditSelectOption<ArticleDTO>
            {
                model = articleDto,
                //articleDTO = articleDto,
                SelectOption = options
            };

            return EditArticleDTO;
        }
        public async Task<Article> UpdateArticle(ArticleImage articleImage)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var article = await db.Articles.FindAsync(articleImage.Id);
                    article.Title = articleImage.Title;
                    article.Content = articleImage.Content;

                    if (articleImage.image != null)
                    {
                        var imagePath = Path.Combine(article.image);
                        if (File.Exists(imagePath))
                        {
                            File.Delete(imagePath);
                        }
                        article.image = await SaveImage(articleImage.image);
                    }

                    var articleCategories = db.ArticleCategories.Where(ac => ac.ArticleId == articleImage.Id);
                    db.ArticleCategories.RemoveRange(articleCategories);

                    var categoryIds = articleImage.Category.Split(',');
                    foreach (var categoryIdString in categoryIds)
                    {
                        if (int.TryParse(categoryIdString, out var categoryId))
                        {
                            var articleCategory = new ArticleCategory
                            {
                                ArticleId = articleImage.Id,
                                CategoryId = categoryId
                            };
                            db.ArticleCategories.Add(articleCategory);
                        }
                    }

                    await db.SaveChangesAsync();
                    transaction.Commit(); // Commit the transaction if everything is successful
                    return article;
                }
                catch (Exception)
                {
                    transaction.Rollback(); // Rollback the transaction if an exception occurs
                    throw; // Re-throw the exception to handle it at a higher level
                }
            }
        }

        public async Task<bool> DeleteArticle(int id)
        {
            var article = await db.Articles.FindAsync(id);
            if (article == null)
            {
                return false;
            }

            var articleCategories = db.ArticleCategories.Where(ac => ac.ArticleId == id);
            db.ArticleCategories.RemoveRange(articleCategories);

            db.Articles.Remove(article);
            await db.SaveChangesAsync();
            var imagePath = Path.Combine(article.image);
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }
            return true; 
        }

        [NonAction]
        public async Task<string> SaveImage(IFormFile formFile)
        {
            string imageName = new string(Path.GetFileNameWithoutExtension(formFile.FileName).Take(10).ToArray()).Replace(" ", "_");
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(formFile.FileName);

            // Thêm tiền tố "Image/Article/" vào imageName
            string imageNameWithPath = "Image/Article/" + imageName;

            // Cập nhật đường dẫn lưu trữ ảnh
            var imagePath = Path.Combine(HostEnvironment.ContentRootPath, "Image", "Article", imageName);

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
