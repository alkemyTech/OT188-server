using OngProject.Core.Models;

using OngProject.Core.Models.DTOs;
using OngProject.Core.Models.Pagination;
using OngProject.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface ITestimonialsBusiness
    {

        //Task<IEnumerable<Testimony>> GetTestimonials(bool listEntity);
        Task<Response<PagedListResponse<TestimonyOutDto>>> GetAll(PagedListParams pagedParams);
        Task<Testimony> GetTestimonial(int id);
        Task<Response<TestimonyOutDto>> InsertTestimonial(NewTestimonyDto entity);
        Task<Response<TestimonyOutDto>> UpdateTestimonial(int id, TestimonyInputDto testimonyInput);
        Task<Response<string>> DeleteTestimonial(int id);


    }
}
