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
        private readonly IAmazonS3Helper _amazonS3;

        public CategoriesBusiness(IUnitOfWork unitOfWork, IEntityMapper entityMapper, IAmazonS3Helper amazonS3)
        {
            _unitOfWork = unitOfWork;
            _entityMapper = entityMapper;
            _amazonS3 = amazonS3;
        }

        public Task<IEnumerable<Category>> GetCategories(bool listEntity)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<CategoryOutDTO>> GetCategory(int id)
        {
            var response = new Response<CategoryOutDTO>();
            try
            {
                var category = await _unitOfWork.CategoryRepository.GetById(id);
                
                if (category != null && category.IsDeleted == false )
                {
                    var categoryDto = _entityMapper.CategoryToCategoryOutDTO(category);
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

        public async Task<Response<CategoryOutDTO>> InsertCategory(NewCategoryDTO categoriesNewsDTO)
        {
            var response = new Response<CategoryOutDTO>();
            try
            {
                var listCategories = await _unitOfWork.CategoryRepository.GetAll(x => x.Name == categoriesNewsDTO.Name);
                if (!listCategories.Any())
                {
                    if(categoriesNewsDTO.Name != null) 
                    {
                        var categoriesNews = _entityMapper.CategoryNewDTOToCategory(categoriesNewsDTO);
                        await _unitOfWork.CategoryRepository.AddAsync(categoriesNews);
                        await _unitOfWork.SaveChangesAsync();
                        var categoryDto = _entityMapper.CategoryToCategoryOutDTO(categoriesNews);
                        response.Data = categoryDto;
                        response.Succeeded = true;
                        response.Message = "Categoria creada correctamente";
                    }
                    else
                    {
                        response.Succeeded = false;
                        response.Message = "Datos incorrectos";
                    }
                }
                else
                {
                    response.Succeeded = false;
                    response.Message = "Ya existe una categoria con el mismo nombre";
                }

                
                
               
            }
            catch (Exception e)
            {
                var listErrors = new[] {e.Message};
                response.Errors = listErrors;
            }
            return response;
        }

        public async Task<Response<string>> UpdateCategory(int id, NewCategoryDTO entity)
        {
            try
            {
                var category = await _unitOfWork.CategoryRepository.GetById(id);
            
                if (category != null && category.IsDeleted == false)
                {
                    /*if (!category.Image.Contains("sin imagen"))
                    {
                        await _amazonS3.DeleteFile(category.Image);
                    }*/

                    if (category.Name == entity.Name)
                    {
                        return new Response<string>("Error", false,null,"Categoria ya existente");
                    }
                    
                    category.Name = entity.Name;
                    category.Description = entity.Description;
                    category.Image = entity.Image != null ? await _amazonS3.UploadFileAsync(entity.Image) : "sin imagen";
                    category.ModifiedAt = DateTime.Now;

                    await _unitOfWork.CategoryRepository.Update(category);
                    await _unitOfWork.SaveChangesAsync();
                
                    return new Response<string>("Success", true, null, "Categoria Actualizada");
                
                }
                return new Response<string>("Error", false,null,"Categoria no existente");
            }
            catch (Exception e)
            {
                throw;
            }
           
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
            catch (InvalidOperationException e)
            {
                return new Response<string>("Error", succeeded: false, message: e.Message);
            }
            await _unitOfWork.SaveChangesAsync();
            return new Response<string>("Succes", message: "Entity Deleted");
        }
    }
}