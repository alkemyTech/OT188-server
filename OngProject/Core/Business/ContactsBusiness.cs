using OngProject.Core.Interfaces;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<Response<RegisterContactDto>> InsertAsync(RegisterContactDto dto)
        {
            var response = new Response<RegisterContactDto>();
            
            try
            {
                var contact = _mapper.RegisterContactDtoToContact(dto);

                contact.ModifiedAt = DateTime.Now;

                await _unitOfWork.ContactRepository.AddAsync(contact);                

                await _unitOfWork.SaveChangesAsync();

                response.Data = dto;

                response.Succeeded = true;

                response.Message = "Operación realizada con éxito";
            }
            catch (Exception ex)
            {
                var listErrors = new string[] { ex.Message, ex.StackTrace };

                response.Errors = listErrors;

                response.Message = "Ha ocurrido un error al intentar realizar la operación";
            }

            return response;
        }
    }    
}
