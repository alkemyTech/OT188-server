using OngProject.Core.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface IContactsBusiness
    {
        Task<IEnumerable<ContactDto>> GetContacts(bool listEntity);
    }
}
