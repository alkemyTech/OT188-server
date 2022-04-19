using System.Collections.Generic;
using System.Threading.Tasks;
using OngProject.Core.Interfaces;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;

namespace OngProject.Core.Business
{
    public class CategoriesBusiness : ICategoriesBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public CategoriesBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<Category>> GetCategories(bool listEntity)
        {
            throw new System.NotImplementedException();
        }

        public Task<Category> GetCategory(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Category> InsertCategory(Category entity)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateCategory(int id, Category entity)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteCategory(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}