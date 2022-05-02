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

        AuthUserDto UserToAuthUserDto(User user, string token);

        CategoriesNameDTO CategoriesNameDTO(Category category);

        Category CategoryToCategoryNewsDTO(NewCategoryDTO categoriesNewsDTO);

        OrganizationDTO OrganizationToOrganizationDTO(Organization organization);

        User RegisterDtoToUser(RegisterDto registerDto);
        PublicSlideDTO PublicSlideDTO(Slide slide);
        DetailSlideDTO DetailSlideDTO(Slide slide);


        Activity ActivityDtoToActivity(NewActivityDto activityDto);

        Slide Slide(AddSlideDTO add);


        Member NewMemberDtoToMember(NewMemberDTO newMemberDTO);

        NewDTO NewToNewDTO(New newEntity);
        New NewDTOToNew(NewDTO newEntity);


    }
}
