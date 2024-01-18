using backend_app.Models;

namespace backend_app.IRepository.dashboard
{
    public interface ICategory
    {
        Task<IEnumerable<Category>> GetAllCategory();
        Task<Category> getCategoryEdit(int id);
        Task<Category> CategoryDelete(int id);
        Task<Category> UpdateCategory(Category category);
        Task<Category> CreateCategory(string name);
    }
}
