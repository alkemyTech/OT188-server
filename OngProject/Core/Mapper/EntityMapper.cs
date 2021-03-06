using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System;
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
                Description = member.Description,
                FacebookUrl = member.FacebookUrl,
                InstagramUrl = member.InstagramUrl,
                LinkedinUrl = member.LinkedinUrl
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

        public Category CategoryNewDTOToCategory(NewCategoryDTO categoriesNewsDTO)
        {
            var category = new Category
            {
                Name = categoriesNewsDTO.Name,
                Description = categoriesNewsDTO.Description,
                Image = categoriesNewsDTO.Image != null ? _amazonS3.UploadFileAsync(categoriesNewsDTO.Image).Result : "sin imagen",
                ModifiedAt = DateTime.Now
            };
            return category;
        }
        public CategoryOutDTO CategoryToCategoryOutDTO(Category categories)
        {
            var category = new CategoryOutDTO()
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
                Address = organization.Address,
                FacebookUrl = organization.FacebookUrl,
                InstagramUrl = organization.InstagramUrl,
                LinkedinUrl = organization.LinkedinUrl
               // Slides = organization.Slides.Select(sl => this.PublicSlideDTO(sl)).ToList()
            };
            return organizationDTO;
        }

        public Organization OrganizationToUpdateOrganizationDTO(UpdateOrganizationDTO updateOrganizationDTO)
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

        public New CreateNewDtoToNew(CreateNewDto newEntity)
        {
            var _newDTO = new New
            {
                Content = newEntity.Content,
                Image = _amazonS3.UploadFileAsync(newEntity.Image).Result,
                CategoryId = newEntity.CategoryId,
                Name = newEntity.Name,
            };
            return _newDTO;
        }

        public CreateNewOutDto NewToCreateNewOutDto(New entity)
        {
            var outNew = new CreateNewOutDto
            {
                Name = entity.Name,
                Content = entity.Content,
                Image = entity.Image
            };

            return outNew;
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
                Image = newMemberDTO.Image != null ? _amazonS3.UploadFileAsync(newMemberDTO.Image).Result : "Sin imagen",
                Description = newMemberDTO.Description,
                ModifiedAt = DateTime.Now
            };
            return member;
        }
        public Member NewMemberDtoToMember(Member member, NewMemberDTO newMemberDTO)
        {
            member.Name = newMemberDTO.Name != null ? newMemberDTO.Name : member.Name;
            member.FacebookUrl = newMemberDTO.FacebookUrl != null ? newMemberDTO.FacebookUrl : member.FacebookUrl;
            member.InstagramUrl = newMemberDTO.InstagramUrl != null ? newMemberDTO.InstagramUrl : member.InstagramUrl;
            member.LinkedinUrl = newMemberDTO.LinkedinUrl != null ? newMemberDTO.LinkedinUrl : member.LinkedinUrl;
            member.Image = newMemberDTO.Image != null ? _amazonS3.UploadFileAsync(newMemberDTO.Image).Result : member.Image;
            member.Description = newMemberDTO.Description != null ? newMemberDTO.Description : member.Description;
            member.ModifiedAt = DateTime.Now;
            return member;
        }
        public Testimony NewTestimonyDtoToTestimony(NewTestimonyDto newTestimonyDto)
        {
            var testimony = new Testimony
            {
                Name = newTestimonyDto.Name,
                Image = newTestimonyDto.Image != null ? _amazonS3.UploadFileAsync(newTestimonyDto.Image).Result : "sin imagen",
                Description = newTestimonyDto.Description,
                ModifiedAt = DateTime.Now
            };
            return testimony;
        }

        public TestimonyOutDto TestimonyToTestimonyOutDto(Testimony testimony)
        {
            var testimonyDto = new TestimonyOutDto
            {
                Name = testimony.Name,
                Image = testimony.Image,
                Description = testimony.Description,


            };
            return testimonyDto;
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


        public Slide UpdateSlide(Slide slide, UpdateSlideDTO changes)
        {
            slide.Text = changes.Text == null ? slide.Text : changes.Text;
            slide.Order = changes.Order == null ? slide.Order : (int)changes.Order;
            slide.OrganizationId = changes.OrganizationId == null ? slide.OrganizationId : (int)changes.OrganizationId;
            slide.ImageUrl = changes.Image == null ? slide.ImageUrl : _amazonS3.UploadFileAsync(changes.Image).Result;
            return slide;
        }

        public Activity UpdateActivity(Activity activity, UpdateActivityDTO changes)
        {
            activity.Name = changes.Name != null ? changes.Name : activity.Name;
            activity.Content = changes.Content != null ? changes.Content : activity.Content;
            activity.Image = changes.Image != null ? _amazonS3.UploadFileAsync(changes.Image).Result : activity.Image;
            activity.ModifiedAt = DateTime.Now;
            return activity;

        }

        public NewDTO NewtoNewDto(New newEntity)
        {
            NewDTO newDtoEntity = new()
            {
                Name = newEntity.Name,
                Content = newEntity.Content,
                Image = newEntity.Image,
                CategoryId = newEntity.CategoryId
            };
            return newDtoEntity;
        }
        public Testimony TestimonyInputDtoToTestimony(Testimony testimony, TestimonyInputDto testimonyInput)
        {
            testimony.Name = testimonyInput.Name != null ? testimonyInput.Name : testimony.Name;
            testimony.Image = testimonyInput.Image != null ? _amazonS3.UploadFileAsync(testimonyInput.Image).Result : "Sin imagen";
            testimony.Description = testimonyInput.Description != null ? testimonyInput.Description : testimony.Description;
            testimony.ModifiedAt = DateTime.Now;

            return testimony;
        }

        public UpdateNewOutDto NewToUpdateNewOUtDto(New entity)
        {
            var outNew = new UpdateNewOutDto
            {
                Name = entity.Name,
                Content = entity.Content,
                Image = entity.Image
            };

            return outNew;
        }

        public User RegisterDtoToUser(RegisterDto update,User user)
        {
            user.FirstName = update.FirstName != null ? update.FirstName : user.FirstName;
            user.LastName = update.LastName != null ? update.LastName : user.LastName;
            user.Email= update.Email != null ? update.Email : user.Email;
            user.Password = update.Password != null ? update.Password : user.Password;
            user.Photo = update.Photo != null ? _amazonS3.UploadFileAsync(update.Photo).Result : user.Photo;
            user.ModifiedAt = DateTime.Now;

            return user;
        }
    }
}
