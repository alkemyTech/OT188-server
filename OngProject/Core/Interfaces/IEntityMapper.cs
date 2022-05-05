using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface IEntityMapper
    {
        SlideDTO SlidetoSlideDTO(Slide slide);

        MemberDTO MemberToMemberDTO(Member member);

        ContactDto ContactToContactDto(Contact contact);

        UserDto UserToUserDto(User user);

        UserOutDTO UserToUserOutDTO(User user);

        AuthUserDto UserToAuthUserDto(User user, string token);

        CategoriesNameDTO CategoriesNameDTO(Category category);

        Category CategoryToCategoryNewsDTO(NewCategoryDTO categoriesNewsDTO);

        NewCategoryDTO CategoryNewsDTOtoCategory(Category categories);

        OrganizationDTO OrganizationToOrganizationDTO(Organization organization);

        User RegisterDtoToUser(RegisterDto registerDto);
      
        PublicSlideDTO PublicSlideDTO(Slide slide);
      
        DetailSlideDTO DetailSlideDTO(Slide slide);

      
        Contact RegisterContactDtoToContact(RegisterContactDto dto);
      
        RegisterContactDto ContactToRegisterContactDto(Contact contact);


        NewOutDto NewToNewOUtDto(New newEntity);
        
        CommentOutDto CommentToCommentOutDto(Comment comment);
        Comment CommentOutDtoToComment(CommentOutDto comment);

        Activity ActivityDtoToActivity(NewActivityDto activityDto);

        ActivityOutDTO ActivityToActivityOutDto(Activity activity);

        Slide Slide(AddSlideDTO add);

        Member NewMemberDtoToMember(NewMemberDTO newMemberDTO);

        NewDTO NewToNewDTO(New newEntity);
      
        New NewDTOToNew(NewDTO newEntity);

        Testimony NewTestimonyDtoToTestimony(NewTestimonyDto newEntity);

        Comment NewCommentDtoToComment(NewCommentDto newCommentDto, int id);
        New NewWithCommentsDtoToNew(NewWithCommentsDto dto);
        NewWithCommentsDto NewToNewWithCommentsDto(New newEntity);
    }
}
