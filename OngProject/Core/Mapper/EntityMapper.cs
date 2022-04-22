using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Mapper
{
    public class EntityMapper : IEntityMapper
    {
        public ContactDto ContactToContactDto(Contact contact)
        {
            var contactDtoItem = new ContactDto
            {
                Name = contact.Name,
                Email = contact.Email
            };

            return contactDtoItem;
        }
    }
}
