using OngProject.Core.Models.DTOs;
using OngProject.Entities;

namespace OngProject.Core.Interfaces
{
    public interface IEntityMapper
    {
        SlideDTO SlidetoSlideDTO(Slide slide);
    }
}
