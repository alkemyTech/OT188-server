using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface IMembersBusiness
    {
        Task<IEnumerable<MemberDTO>> GetMembers(bool listEntity);

        Task<Response<NewMemberDTO>> InsertMember(NewMemberDTO entity);

        Task<Response<string>> DeleteMember(int id);
    }
}
