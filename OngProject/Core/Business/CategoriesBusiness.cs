using System.Collections.Generic;
using System.Threading.Tasks;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System.Linq;
using System;
using OngProject.Core.Models;

namespace OngProject.Core.Business
{
    public class CategoriesBusiness : ICategoriesBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityMapper _entityMapper;

        public CategoriesBusiness(IUnitOfWork unitOfWork, IEntityMapper entityMapper)
        {
            _unitOfWork = unitOfWork;
            _entityMapper = entityMapper;
        }

        public Task<IEnumerable<Category>> GetCategories(bool listEntity)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Category> GetCategory(int id)
        {
            var category = new Category();
            category = (Category)await _unitOfWork.CategoryRepository.GetById(id);
            if(category != null)
            {
                return category;
            }
            throw new Exception();
        }

        public Task<Category> InsertCategory(Category entity)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateCategory(int id, Category entity)
        {
            throw new System.NotImplementedException();
        }

       

        public async Task<IEnumerable<CategoriesNameDTO>> GetNameList()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAll(true);
            var categoriesName = categories.Select(cat => _entityMapper.CategoriesNameDTO(cat)).ToList();
            return categoriesName;
        }

        public async Task<Response<string>> DeleteCategory(int id)
        {
            try
            {
                await _unitOfWork.CategoryRepository.Delete(id);
            }
            catch (System.InvalidOperationException e)
            {
                return new Response<string>("Error", succeeded: false, message: e.Message);
            }
            _unitOfWork.SaveChanges();
            return new Response<string>("Succes", message: "Entity Deleted");
        }
    }
}