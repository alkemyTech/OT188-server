using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface ICommentsBusiness
    {

        Task<IEnumerable<Comment>> GetTestimonials(bool listEntity);
        Task<Comment> GetTestimonial(int id);
        Task<Response<NewCommentDto>> InsertComment(NewCommentDto entity);
        Task<Response<string>> UpdateComment(int id, UpdateCommentDto entity);
        Task<Response<string>> DeleteComments(int id);

    }
}
