using OngProject.Core.Models;

using OngProject.Core.Models.DTOs;

using OngProject.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface ITestimonialsBusiness
    {

        Task<IEnumerable<Testimony>> GetTestimonials(bool listEntity);
        Task<Testimony> GetTestimonial(int id);
        Task<Response<TestimonyOutDto>> InsertTestimonial(NewTestimonyDto entity);
        Task UpdateTestimonial(int id, Testimony entity);
        Task<Response<string>> DeleteTestimonial(int id);


    }
}
