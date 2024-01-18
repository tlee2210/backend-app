using backend_app.IRepository;
using backend_app.IRepository.dashboard;
using backend_app.Models;
using Microsoft.EntityFrameworkCore;

namespace backend_app.Services.dashboard
{
    public class CategoryServices : ICategory
    {
        private readonly DatabaseContext db;
        public CategoryServices(DatabaseContext db)
        {
            this.db = db;
        }
        public async Task<Category> CategoryDelete(int id)
        {
            var Category = await db.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (Category == null)
            {
                return null;
            }

            db.Categories.Remove(Category);
            await db.SaveChangesAsync();

            return Category; 
        }

        public async Task<Category> getCategoryEdit(int id)
        {
            var Category = await db.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (Category == null)
            {
                return null;
            }
            return Category;
        }

        public async Task<IEnumerable<Category>> GetAllCategory()
        {
            return await db.Categories.ToListAsync();
        }

        public async Task<Category> UpdateCategory(Category category)
        {
            var existingCategory = await db.Categories.FirstOrDefaultAsync(c => c.Id == category.Id);

            if (existingCategory == null)
            {
                return null; 
            }

            existingCategory.Name = category.Name;

            await db.SaveChangesAsync();

            return existingCategory; 
        }

        public async Task<Category> CreateCategory(string name)
        {
            var existingCategory = await db.Categories.FirstOrDefaultAsync(c => c.Name == name);
            if (existingCategory != null)
            {
                return null;
            }

            var newCategory = new Category { Name = name };

            db.Categories.Add(newCategory);
            await db.SaveChangesAsync();

            return newCategory;
        }

    }
}
