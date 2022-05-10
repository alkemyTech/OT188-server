using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using OngProject.Core.Models.Pagination;

namespace OngProject.Core.Interfaces
{
    public interface IMembersBusiness
    {
        Task<Response<PagedListResponse<MemberDTO>>> GetMembers(PagedListParams pagedParams);

        Task<Response<NewMemberDTO>> InsertMember(NewMemberDTO entity);

        Task<Response<string>> DeleteMember(int id);
    }
}
