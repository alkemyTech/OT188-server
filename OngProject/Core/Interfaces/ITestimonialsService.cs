using OngProject.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface ITestimonialsService
    {

        Task<IEnumerable<Testimonio>> GetTestimonials(bool listEntity);
        Task<Testimonio> GetTestimonial(int id);
        Task<Testimonio> InsertTestimonial(Testimonio entity);
        Task UpdateTestimonial(int id, Testimonio entity);
        Task DeleteTestimonial(int id);

    }
}
