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
        Task<Response<CategoryOutDTO>> GetCategory(int id);

        Task<Response<CategoryOutDTO>> InsertCategory(NewCategoryDTO entity);
        Task<Response<string>> UpdateCategory(int id, NewCategoryDTO entity);       
        Task<Response<string>> DeleteCategory(int id);
                       
        Task<IEnumerable<CategoriesNameDTO>> GetNameList();
    }
}