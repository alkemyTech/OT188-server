using OngProject.Core.Interfaces;
using OngProject.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class TestimonialsBusiness : ITestimonialsBusiness
    {
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

        Task<Testimony> ITestimonialsBusiness.InsertTestimonial(Testimony entity)
        {
            throw new System.NotImplementedException();
        }

        Task ITestimonialsBusiness.UpdateTestimonial(int id, Testimony entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
