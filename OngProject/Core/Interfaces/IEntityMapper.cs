using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface IEntityMapper
    {
        SlideDTO SlidetoSlideDTO(Slide slide);
        ContactDto ContactToContactDto(Contact contact);

        UserDto UserToUserDto(User user);


        AuthUserDto UserToAuthUserDto(User user, string token);


        CategoriesNameDTO CategoriesNameDTO(Category category);

        OrganizationDTO OrganizationToOrganizationDTO(Organization organization);


    }
}
