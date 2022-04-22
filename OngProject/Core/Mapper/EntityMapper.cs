using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OngProject.Core.Mapper
{
    public class EntityMapper : IEntityMapper
    {























        public OrganizationDTO ConvertToOrganizationDTO(Organization item)
        {
            var organizationDTO = new OrganizationDTO
            {
                Name = item.Name,
                ImageUrl = item.Image,
                Phone = item.Phone,
                Address = item.Address
            };
            return organizationDTO;
        }
    }
}
