using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OngProject.Core.Interfaces;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;

namespace OngProject.Core.Business
{
    public class OrganizationsBusiness : IOrganizationsBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityMapper _entityMapper;
        
        public OrganizationsBusiness(IUnitOfWork unitOfWork, IEntityMapper entityMapper, 
            IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _entityMapper = entityMapper;
        }


        public async Task<Response<IEnumerable<OrganizationDTO>>> GetOrganizations(bool listEntity)
        {
            var organizationsList = await _unitOfWork.OrganizationsRepository.GetAll(org => org.IsDeleted == false, org => org.Slides.OrderBy(sl => sl.Order));

            if (organizationsList == null)
            {
                return new Response<IEnumerable<OrganizationDTO>>(null, false,null,"Not Found");
            }
            var organizationDTOList = new List<OrganizationDTO>();
            organizationDTOList.Add(_entityMapper.OrganizationToOrganizationDTO(organizationsList.SingleOrDefault()));

            return new Response<IEnumerable<OrganizationDTO>>(organizationDTOList);
        }       

        async Task<Response<OrganizationDTO>> IOrganizationsBusiness.GetOrganization(int id)
        {
            var organization = await _unitOfWork.OrganizationsRepository.GetById(id);
            if (organization == null || organization.IsDeleted == true)
            {
                return new Response<OrganizationDTO>(null, false, message: "Not Found");
            }
            var orgDto = _entityMapper.OrganizationToOrganizationDTO(organization);

            return new Response<OrganizationDTO>(orgDto);
        }

        public async Task <Response<UpdateOrganizationDTO>> UpdateOrganizations(UpdateOrganizationDTO updateEntity)
        {
            var response = new Response<UpdateOrganizationDTO>();
            try
            {
                if((updateEntity.Name is String) == true)
                {
                    var NewUpdateEntity = _entityMapper.OrganizationToUpdateOrganizationDTO(updateEntity);
                    await _unitOfWork.OrganizationsRepository.Update(NewUpdateEntity);
                    await _unitOfWork.SaveChangesAsync();
                    response.Data = updateEntity;
                    response.Succeeded = true;
                    response.Message = "The organization has been updated";
                }
                else
                {
                    response.Succeeded = false;
                    response.Message = "Datos incorrectos";
                }
                
            }
            catch (Exception e)
            {
                throw;                
            }
            return response;
        }
    }
}