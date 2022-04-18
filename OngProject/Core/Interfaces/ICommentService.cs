using OngProject.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface ICommentService
    {

        Task<IEnumerable<Comment>> GetTestimonials(bool listEntity);
        Task<Comment> GetTestimonial(int id);
        Task<Comment> InsertTestimonial(Comment entity);
        Task UpdateTestimonial(int id, Comment entity);
        Task DeleteTestimonial(int id);

    }
}
