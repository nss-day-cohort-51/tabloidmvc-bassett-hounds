using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface ICategoryRepository
    {
        List<Category> GetAll();
        void Add(Category category);
        void UpdateCategory(Category category);
        public Category GetCategoryById(int id);
        void DeleteCategory(int categoryId);
    }
}