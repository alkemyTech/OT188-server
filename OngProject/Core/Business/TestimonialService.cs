using OngProject.Core.Interfaces;
using OngProject.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class TestimonialService : ITestimonialsService
    {
        Task ITestimonialsService.DeleteTestimonial(int id)
        {
            throw new System.NotImplementedException();
        }

        Task<Testimonio> ITestimonialsService.GetTestimonial(int id)
        {
            throw new System.NotImplementedException();
        }

        Task<IEnumerable<Testimonio>> ITestimonialsService.GetTestimonials(bool listEntity)
        {
            throw new System.NotImplementedException();
        }

        Task<Testimonio> ITestimonialsService.InsertTestimonial(Testimonio entity)
        {
            throw new System.NotImplementedException();
        }

        Task ITestimonialsService.UpdateTestimonial(int id, Testimonio entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
