using OngProject.Core.Interfaces;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class ContactsBusiness : IContactsBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityMapper _mapper;

        public ContactsBusiness(IUnitOfWork unitOfWork, IEntityMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ContactDto>> GetContacts(bool listEntity)
        {
            try
            {
                var contactsList = await _unitOfWork.ContactRepository.GetAll(listEntity);

                if (contactsList == null)
                {
                    return null;
                }

                var contactsDtoList = new List<ContactDto>();

                foreach (var contact in contactsList)
                {
                    contactsDtoList.Add(_mapper.ContactToContactDto(contact));
                }

                return contactsDtoList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
