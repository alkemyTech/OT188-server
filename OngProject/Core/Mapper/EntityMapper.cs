﻿using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OngProject.Core.Mapper
{
    public class EntityMapper : IEntityMapper
    {
        public SlideDTO SlidetoSlideDTO(Slide slide)
        {
            var slideDTO = new SlideDTO()
            {
                ImageUrl = slide.ImageUrl,
                Order = slide.Order
            };
            return slideDTO;
        }

        public ContactDto ContactToContactDto(Contact contact)
        {
            var contactDtoItem = new ContactDto
            {
                Name = contact.Name,
                Email = contact.Email
            };

            return contactDtoItem;

        }

        public AuthUserDto UserToAuthUserDto(User user, string token)
        {
            var _authUserDto = new AuthUserDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Token = token
            };
            return _authUserDto;


        public CategoriesNameDTO CategoriesNameDTO(Category category)
        {
            return new CategoriesNameDTO
            {
                Name = category.Name
            };

        public OrganizationDTO OrganizationToOrganizationDTO(Organization organization)
        {
            var organizationDTO = new OrganizationDTO
            {
                Name = organization.Name,
                ImageUrl = organization.Image,
                Phone = organization.Phone,
                Address = organization.Address
            };
            return organizationDTO;


        }
    }
}
