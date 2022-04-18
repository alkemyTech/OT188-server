using OngProject.Core.Interfaces;
using OngProject.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class CommentService : ICommentService
    {
        public Task DeleteTestimonial(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Comment> GetTestimonial(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Comment>> GetTestimonials(bool listEntity)
        {
            throw new System.NotImplementedException();
        }

        public Task<Comment> InsertTestimonial(Comment entity)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateTestimonial(int id, Comment entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
