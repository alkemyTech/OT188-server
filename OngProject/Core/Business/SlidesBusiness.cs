using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using OngProject.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class SlidesBusiness:ISlidesBusiness
    {

        private readonly IEntityMapper _entityMapper;
        private readonly IUnitOfWork _unitOfWork;

        public SlidesBusiness(IUnitOfWork unitOfWork, IEntityMapper entityMapper)
        {
            _entityMapper = entityMapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<SlideDTO>> GetSlides(bool listEntity)
        {
            try
            {
                var listSlides = await _unitOfWork.SlideRepository.GetAll(listEntity);
                var slideDTOs = new List<SlideDTO>();
                foreach (var slide in listSlides)
                {
                    slideDTOs.Add(_entityMapper.SlidetoSlideDTO(slide));
                }

                return slideDTOs;
            }
            catch (System.Exception)
            {

                throw;
            }
            
        }
        public async Task Delete(int id)
        {
            await _unitOfWork.SlideRepository.Delete(id);
            _unitOfWork.SaveChanges();
        } 
    }
}
