using OngProject.Core.Interfaces;
using OngProject.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using OngProject.Repositories.Interfaces;
using OngProject.Core.Models;

using OngProject.Core.Models.DTOs;

using System;

namespace OngProject.Core.Business
{
    public class TestimonialsBusiness : ITestimonialsBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityMapper _entityMapper;

        public TestimonialsBusiness(IUnitOfWork unitOfWork, IEntityMapper entityMapper)
        {
            _unitOfWork = unitOfWork;
            _entityMapper = entityMapper;
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

        Task<IEnumerable<Testimony>> ITestimonialsBusiness.GetTestimonials(bool listEntity)
        {
            throw new System.NotImplementedException();
        }

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
