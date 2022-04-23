using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface ICommentsBusiness
    {

        Task<IEnumerable<CommentDto>> GetComments(bool listEntity);
        Task<CommentDto> GetComment(int id);
        Task<CommentDto> InsertComment(CommentDto entity);
        Task UpdateComment(int id, CommentDto entity);
        Task DeleteComment(int id);

    }
}
