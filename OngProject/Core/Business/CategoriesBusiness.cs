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

        public async Task<Response<NewCategoryDTO>> GetCategory(int id)
        {
            var response = new Response<NewCategoryDTO>();
            try
            {
                var category = await _unitOfWork.CategoryRepository.GetById(id);
                if (category != null && category.IsDeleted == false )
                {
                    var categoryDto = _entityMapper.CategoryNewsDTOtoCategory(category);
                    response.Data = categoryDto;
                    response.Succeeded = true;
                    response.Message = "Success.";
                }
                else
                {
                    response.Succeeded = false;
                    response.Message = "Category is deleted or not exist.";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

        public async Task<Response<NewCategoryDTO>> InsertCategory(NewCategoryDTO categoriesNewsDTO)
        {
            var response = new Response<NewCategoryDTO>();
            try
            {   
                if((categoriesNewsDTO.Name is String) == true) 
                {
                    var categoriesNews = _entityMapper.CategoryToCategoryNewsDTO(categoriesNewsDTO);
                    var category = await _unitOfWork.CategoryRepository.AddAsync(categoriesNews);
                    await _unitOfWork.SaveChangesAsync();
                    response.Data = categoriesNewsDTO;
                    response.Succeeded = true;
                    response.Message = "Categoria creada correctamente";
                }
                else
                {
                    response.Succeeded = false;
                response.Message = "Datos incorrectos";
                }
            }
            catch (Exception e)
            {
                var listErrors = new string[] {e.Message, e.StackTrace};
                response.Errors = listErrors;
            }
            return response;
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