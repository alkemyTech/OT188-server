using Microsoft.Extensions.Configuration;
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
        private readonly IEmailServices _emailService;
        private readonly IConfiguration _configuration;

        public ContactsBusiness(IUnitOfWork unitOfWork, IEntityMapper mapper, IEmailServices emailServices, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailService = emailServices;
            _configuration = configuration;
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

                var subject = "Recibimos su mensaje.";

                var body = $"{dto.Name}: Gracias por contactarnos, le responderemos a la brevedad.";

                await _emailService.Send(dto.Email, _configuration.GetSection("emailContacto").Value, subject, body);

                response.Data = dto;

                response.Succeeded = true;

                response.Message = "Operación realizada con éxito";
            }
            catch (Exception ex)
            {
                var listErrors = new string[] { ex.Message };

                response.Errors = listErrors;

                response.Message = "Ha ocurrido un error al intentar realizar la operación";
            }

            return response;
        }
    }    
}
