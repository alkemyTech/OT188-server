using OngProject.Core.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface ISlidesBusiness
    {
        Task<IEnumerable<SlideDTO>> GetSlides(bool listEntity);


    }
}
