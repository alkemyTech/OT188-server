using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface ISlidesBusiness
    {
        Task<Response<IEnumerable<SlideDTO>>> GetSlides(bool listEntity);
        Task<Response<DetailSlideDTO>> GetDetailSlide(int id);
        Task<Response<string>> Delete(int id);
        Task<Response<string>> Add(AddSlideDTO addSlideDTO);
        Task<Response<DetailSlideDTO>> Update(UpdateSlideDTO updateSlideDTO, int id);
    }
}
