using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface IContactsBusiness
    {
        Task<IEnumerable<ContactDto>> GetContacts(bool listEntity);

        public Task<Response<RegisterContactDto>> InsertAsync(RegisterContactDto dto);
    }
}
