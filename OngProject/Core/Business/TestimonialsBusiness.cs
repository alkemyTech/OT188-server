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
        
        public TestimonialsBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        Task ITestimonialsBusiness.DeleteTestimonial(int id)
        {
            throw new System.NotImplementedException();
        }

        Task<Testimony> ITestimonialsBusiness.GetTestimonial(int id)
        {
            throw new System.NotImplementedException();
        }

        Task<IEnumerable<Testimony>> ITestimonialsBusiness.GetTestimonials(bool listEntity)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Response<NewTestimonyDto>> InsertTestimonial(NewTestimonyDto entity)
        {
            try
            {
                

                return new Response<NewTestimonyDto>();
            }
            catch (Exception)
            {

                throw;
            }
        }

        Task ITestimonialsBusiness.UpdateTestimonial(int id, Testimony entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
