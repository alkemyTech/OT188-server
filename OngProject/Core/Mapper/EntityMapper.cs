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

        private readonly IAmazonS3Helper _amazonS3;

        public EntityMapper(IAmazonS3Helper amazonS3)
        {
            _amazonS3 = amazonS3;
        }
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

        public UserOutDTO UserToUserOutDTO(User user)
        {
            return new UserOutDTO()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Photo = user.Photo,
            };
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
                Password = registerDto.Password,
                Photo = registerDto.Photo != null ? _amazonS3.UploadFileAsync(registerDto.Photo).Result : null,
                ModifiedAt = DateTime.Now
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
        public NewCategoryDTO CategoryNewsDTOtoCategory(Category categories)
        {
            var category = new NewCategoryDTO
            {
                Name = categories.Name,
                Description = categories.Description,
                Image = categories.Image
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
                Address = organization.Address
               // Slides = organization.Slides.Select(sl => this.PublicSlideDTO(sl)).ToList()
            };
            return organizationDTO;
        }

        public  Organization OrganizationToUpdateOrganizationDTO(UpdateOrganizationDTO updateOrganizationDTO)
        {
            var organization = new Organization
            {
                Name = updateOrganizationDTO.Name,
                Image = updateOrganizationDTO.Image,
                Address = updateOrganizationDTO.Address,
                Phone = updateOrganizationDTO.Phone,
                Email = updateOrganizationDTO.Email,
                WelcomeText = updateOrganizationDTO.WelcomeText,
                AboutUsText = updateOrganizationDTO.AboutUsText,
                FacebookUrl = updateOrganizationDTO.FacebookUrl,
                InstagramUrl = updateOrganizationDTO.InstagramUrl,
                LinkedinUrl = updateOrganizationDTO.LinkedinUrl               
            };
            return organization;
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

        public Contact RegisterContactDtoToContact(RegisterContactDto dto)
        {
            return new Contact
            {
                Name = dto.Name,
                Email = dto.Email,
                Message = dto.Message
            };
        }

        public RegisterContactDto ContactToRegisterContactDto(Contact contact)
        {
            return new RegisterContactDto
            {
                Name = contact.Name,
                Email = contact.Email,
                Message = contact.Message
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

        public Comment CommentOutDtoToComment(CommentOutDto comment)
        {
            return new Comment()
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
                Image = activityDto.Image != null ? _amazonS3.UploadFileAsync(activityDto.Image).Result : "sin imagen",
                ModifiedAt = DateTime.Now
            };
            return activity;
        }

        public ActivityOutDTO ActivityToActivityOutDto(Activity activity)
        {
            var activityDto = new ActivityOutDTO
            {
                Name = activity.Name,
                Content = activity.Content,
                Image = activity.Image
            };
            return activityDto;
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
        public Testimony NewTestimonyDtoToTestimony(NewTestimonyDto newTestimonyDto)
        {
            var testimony = new Testimony
            {
                Name = newTestimonyDto.Name,
                Image = newTestimonyDto.Image,
                Description = newTestimonyDto.Description,
                ModifiedAt = DateTime.Now
            };
            return testimony;
        }

        public Comment NewCommentDtoToComment(NewCommentDto newCommentDto, int id)
        {

            var comment = new Comment
            {
                Body = newCommentDto.Body,
                IdUser = id,
                
                NewId = newCommentDto.NewId,
                
                
                ModifiedAt = DateTime.Now

            };
            return comment;
        }

        public New NewWithCommentsDtoToNew(NewWithCommentsDto dto)
        {
            var _new = new New
            {
                Comments = dto.Comments.Select(x => this.CommentOutDtoToComment(x)).ToList()
            };

            return _new;
        }

        public NewWithCommentsDto NewToNewWithCommentsDto(New newEntity)
        {
            var _new = new NewWithCommentsDto
            {
                Comments = newEntity.Comments.Select(x => this.CommentToCommentOutDto(x)).ToList()
            };

            return _new;
        }
    }
}
