using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace OngProject.Core.Business
{
    public class SlidesBusiness:ISlidesBusiness
    {

        private readonly IEntityMapper _entityMapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAmazonS3Helper _amazonS3Helper;

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
        public async Task<DetailSlideDTO> GetDetailSlide(int id)
        {
            var slide = await _unitOfWork.SlideRepository.GetById(id);
            if(slide == null)
            {
                return null;
            }
            return _entityMapper.DetailSlideDTO(slide);
        }
        public async Task<Response<string>> Delete(int id)
        {
            try
            {
                await _unitOfWork.SlideRepository.Delete(id);
            }
            catch (System.InvalidOperationException e)
            {
                return new Response<string>("Error", succeeded: false, message:e.Message);
            }
            _unitOfWork.SaveChanges();
            return new Response<string>("Succes", message:"Entity Deleted");
        }

        public async Task<Response<string>> Add(AddSlideDTO add)
        {
            if(add.Order == null)
            {
                add.Order = await _unitOfWork.SlideRepository.GetLastOrder() + 1;
            }

            var url = await _amazonS3Helper.UploadFileAsync(add.Image);
            await _unitOfWork.SlideRepository.Add(_entityMapper.Slide(add, url));
            _unitOfWork.SaveChanges();

            return new Response<string>("Succes");
        }
    }
}
