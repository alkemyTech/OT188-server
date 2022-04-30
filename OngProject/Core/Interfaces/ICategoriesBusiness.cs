using System.Collections.Generic;
using System.Threading.Tasks;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;

namespace OngProject.Core.Interfaces
{
    public interface ICategoriesBusiness
    {
        Task<IEnumerable<Category>> GetCategories(bool listEntity);
        Task<Category> GetCategory(int id);
        Task<Response<NewCategoryDTO>> InsertCategory(NewCategoryDTO categoriesNewsDTO);
        Task UpdateCategory(int id, Category entity);
        Task DeleteCategory(int id);
        Task<IEnumerable<CategoriesNameDTO>> GetNameList();
    }
}