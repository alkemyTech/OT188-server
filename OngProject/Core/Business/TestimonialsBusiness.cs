using OngProject.Core.Interfaces;
using OngProject.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using OngProject.Repositories.Interfaces;
using OngProject.Core.Models;

using OngProject.Core.Models.DTOs;

using System;
using Microsoft.AspNetCore.Http;
using OngProject.Core.Models.Pagination;
using System.Linq;
using OngProject.Core.Helper;

namespace OngProject.Core.Business
{
    public class TestimonialsBusiness : ITestimonialsBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityMapper _entityMapper;
        private readonly IHttpContextAccessor _httpContext;

        public TestimonialsBusiness(IUnitOfWork unitOfWork, IEntityMapper entityMapper, IHttpContextAccessor httpContext)
        {
            _unitOfWork = unitOfWork;
            _entityMapper = entityMapper;
            _httpContext = httpContext;
        }

        public async Task<Response<PagedListResponse<TestimonyOutDto>>> GetAll(PagedListParams pagedParams)
        {
            var response = new Response<PagedListResponse<TestimonyOutDto>>();
            try
            {
                var testimonials = await _unitOfWork.TestimonyRepository.FindAllAsync(null, null, null, pagedParams.PageNumber, pagedParams.PageSize = 10);
                var totalCount = await _unitOfWork.TestimonyRepository.Count();

                if (totalCount == 0)
                {
                    response.Message = "There are no testimonials to show";
                    response.Data = null;
                    response.Succeeded = true;
                }

                if (testimonials.Count == 0)
                {
                    response.Message = "There are no testimonials with the parameters given";
                    response.Data = null;
                    response.Succeeded = false;
                }
                else
                {
                    var testimonyDto = testimonials
                    .Select(testimonyItem => _entityMapper.TestimonyToTestimonyOutDto(testimonyItem));

                    var paged = PagedList<TestimonyOutDto>.Create(testimonyDto.ToList(), totalCount,
                                                                    pagedParams.PageNumber,
                                                                   pagedParams.PageSize);

                    var url = $"{this._httpContext.HttpContext.Request.Scheme}://{this._httpContext.HttpContext.Request.Host}" +
                        $"{this._httpContext.HttpContext.Request.Path}";

                    var pagedResponse = new PagedListResponse<TestimonyOutDto>(paged, url);
                    
                    response.Data = pagedResponse;
                    response.Succeeded = true;
                    response.Message = "Retrieved paginated list of testimonials successfully";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

        public async Task<Response<string>> DeleteTestimonial(int id)
        {
            try
            {
                await _unitOfWork.TestimonyRepository.Delete(id);
            }
            catch (InvalidOperationException e)
            {
                return new Response<string>("Error", succeeded: false, message: e.Message);
            }
            _unitOfWork.SaveChanges();
            return new Response<string>("Success", message: "Entity Deleted");
        }

        Task<Testimony> ITestimonialsBusiness.GetTestimonial(int id)
        {
            throw new System.NotImplementedException();
        }

        //Task<IEnumerable<Testimony>> ITestimonialsBusiness.GetTestimonials(bool listEntity)
        //{
        //    throw new System.NotImplementedException();
        //}

        public async Task<Response<TestimonyOutDto>> InsertTestimonial(NewTestimonyDto newEntity)
        {
            var result = new Response<TestimonyOutDto>();
            try
            {
                var testimony = _entityMapper.NewTestimonyDtoToTestimony(newEntity);
                await _unitOfWork.TestimonyRepository.AddAsync(testimony);
                await _unitOfWork.SaveChangesAsync();
                var testimonyDto = _entityMapper.TestimonyToTestimonyOutDto(testimony);
                result.Data = testimonyDto;
                result.Succeeded = true;
                result.Message = "The Testimony has been created";
            }
            catch (Exception e)
            {
                throw;
            }
            return result;
        }

        Task ITestimonialsBusiness.UpdateTestimonial(int id, Testimony entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
