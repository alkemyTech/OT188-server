using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface ISlidesBusiness
    {
        Task<IEnumerable<SlideDTO>> GetSlides(bool listEntity);

        Task<DetailSlideDTO> GetDetailSlide(int id);
        Task<Response<string>> Delete(int id);
        Task<Response<string>> Add(AddSlideDTO addSlideDTO); 
    }
}
