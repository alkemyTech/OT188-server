using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Core.Models.Pagination;
using OngProject.Entities;

namespace OngProject.Core.Interfaces
{
    public interface INewsBusiness
    {
        Task<Response<PagedListResponse<NewDTO>>> GetAll(PagedListParams pagedParams);
        Task<IEnumerable<New>> GetNews(bool listEntity);
        Task<Response<NewOutDto>> GetNew(int id);
        Task<Response<NewWithCommentsDto>> GetNewComments(int id);
        Task<Response<CreateNewOutDto>> InsertNew(CreateNewDto dto);
        Task UpdateNew(int id, New entity);
        Task<Response<string>> DeleteNew(int id);
    }
}