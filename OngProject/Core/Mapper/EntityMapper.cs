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
        public SlideDTO SlidetoSlideDTO(Slide slide)
        {
            var slideDTO = new SlideDTO()
            {
                ImageUrl = slide.ImageUrl,
                Order = slide.Order
            };
            return slideDTO;
        }

        public MemberDTO MemberToMemberDTO(Member member)
        {
            var memberDTO = new MemberDTO()
            {
                Name = member.Name,
                Image = member.Image,
                Description = member.Description
            };
            return memberDTO;
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

        public UserDto UserToUserDto(User user)
        {
            var userDto = new UserDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Photo = user.Photo,
                Rol = user.Roles.Name
            };

            return userDto;
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

        }
        public User RegisterDtoToUser(RegisterDto registerDto)
        {
            var user = new User
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                Password = registerDto.Password
            };
            return user;
        }
        public Category CategoryToCategoryNewsDTO(NewCategoryDTO categoriesNewsDTO)
        {
            var category = new Category
            {
                Name = categoriesNewsDTO.Name,
                Description = categoriesNewsDTO.Description,
                Image = categoriesNewsDTO.Image
            };
            return category;
        }

        public CategoriesNameDTO CategoriesNameDTO(Category category)
        {
            return new CategoriesNameDTO
            {
                Name = category.Name
            };
        }
        public OrganizationDTO OrganizationToOrganizationDTO(Organization organization)
        {
            var organizationDTO = new OrganizationDTO
            {
                Name = organization.Name,
                ImageUrl = organization.Image,
                Phone = organization.Phone,
                Address = organization.Address,
               // Slides = organization.Slides.Select(sl => this.PublicSlideDTO(sl)).ToList()
            };
            return organizationDTO;
        }
        public PublicSlideDTO PublicSlideDTO(Slide slide)
        {
            return new PublicSlideDTO
            {
                Text = slide.Text,
                ImageUrl = slide.ImageUrl,
                Order = slide.Order
            };
        }
        public DetailSlideDTO DetailSlideDTO(Slide slide)
        {
            return new DetailSlideDTO
            {
                Id = slide.Id,
                ImageUrl = slide.ImageUrl,
                Order = slide.Order,
                OrganizationId = slide.OrganizationId,
                Text = slide.Text
            };
        }


        public NewOutDto NewToNewOUtDto(New newEntity)
        {
            return new NewOutDto()
            {
                Name = newEntity.Name,
                Content = newEntity.Content,
                Image = newEntity.Image,
                CategoryId = newEntity.CategoryId,
                Comments = newEntity.Comments.Select(x => this.CommentToCommentOutDto(x)).ToList()
            };
        }

        public CommentOutDto CommentToCommentOutDto(Comment comment)
        {
            return new CommentOutDto()
            {
                Body = comment.Body,
                IdUser = comment.IdUser
            };
        }
        

        public NewDTO NewToNewDTO(New newEntity)
        {
            var _newDTO = new NewDTO
            {
                Content = newEntity.Content,
                Image = newEntity.Image,
                CategoryId = newEntity.CategoryId,
                Name = newEntity.Name,
            };
            return _newDTO;
        }


        public New NewDTOToNew(NewDTO newEntity)
        {
            var _new = new New
            {
                Content = newEntity.Content,
                Image = newEntity.Image,
                CategoryId = newEntity.CategoryId,
                Name = newEntity.Name,
            };
            return _new;
        }

        public Activity ActivityDtoToActivity(NewActivityDto activityDto)
        {
            var activity = new Activity
            {
                Name = activityDto.Name,
                Content = activityDto.Content,
                Image = activityDto.Image,
                ModifiedAt = DateTime.Now
            };
            return activity;
        } 


        public Slide Slide(AddSlideDTO add)
        {
            return new Slide()
            {
                ImageUrl = add.ImageUrl,
                Order = (int)add.Order,
                Text = add.Text,
                OrganizationId = add.OrganizationId
            };
        }


        public Member NewMemberDtoToMember(NewMemberDTO newMemberDTO)
        {
            var member = new Member
            {
                Name = newMemberDTO.Name,
                FacebookUrl = newMemberDTO.FacebookUrl,
                InstagramUrl = newMemberDTO.InstagramUrl,
                LinkedinUrl = newMemberDTO.LinkedinUrl,
                Image = newMemberDTO.Image,
                Description = newMemberDTO.Description,
                ModifiedAt = DateTime.Now
            };
            return member;
        }
    }
}
