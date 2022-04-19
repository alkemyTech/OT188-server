using System.Collections.Generic;
using System.Threading.Tasks;
using OngProject.Entities;

namespace OngProject.Core.Interfaces
{
    public interface ICategoriesBusiness
    {
        Task<IEnumerable<Category>> GetCategories(bool listEntity);
        Task<Category> GetCategory(int id);
        Task<Category> InsertCategory(Category entity);
        Task UpdateCategory(int id, Category entity);
        Task DeleteCategory(int id);
    }
}