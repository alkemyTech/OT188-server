using OngProject.Core.Models.DTOs;
using OngProject.Entities;

namespace OngProject.Core.Interfaces
{
    public interface IEntityMapper
    {
        SlideDTO SlidetoSlideDTO(Slide slide);
        ContactDto ContactToContactDto(Contact contact);
        AuthUserDto UserToAuthUserDto(User user, string token);
    }
}
