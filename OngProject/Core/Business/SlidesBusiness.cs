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

        public SlidesBusiness(IUnitOfWork unitOfWork, IEntityMapper entityMapper, IAmazonS3Helper amazonS3Helper)
        {
            _entityMapper = entityMapper;
            _unitOfWork = unitOfWork;
            _amazonS3Helper = amazonS3Helper;
        }

        public async Task<Response<IEnumerable<SlideDTO>>> GetSlides(bool listEntity)
        {
            try
            {
                var listSlides = await _unitOfWork.SlideRepository.GetAll(listEntity);
                var slideDTOs = new List<SlideDTO>();
                foreach (var slide in listSlides)
                {
                    slideDTOs.Add(_entityMapper.SlidetoSlideDTO(slide));
                }

                return new Response<IEnumerable<SlideDTO>>(slideDTOs);
            }
            catch (System.Exception e)
            {
                var listErrors = new string[] { e.Message };
                return new Response<IEnumerable<SlideDTO>>(null, false, listErrors, "Error");
            }
            
        }
        public async Task<Response<DetailSlideDTO>> GetDetailSlide(int id)
        {
            var slide = await _unitOfWork.SlideRepository.GetById(id);
            if(slide == null || slide.IsDeleted == true)
            {
                return new Response<DetailSlideDTO>(null, false, null, "Not Found");
            }
            return new Response<DetailSlideDTO>(_entityMapper.DetailSlideDTO(slide));
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
            //set order
            if(add.Order == null)
            {
                add.Order = await _unitOfWork.SlideRepository.GetLastOrder() + 1;
            }
            //set organization id
            add.OrganizationId = (await _unitOfWork.OrganizationsRepository.GetAll(true)).FirstOrDefault().Id;
            //upload image and set url access
            add.ImageUrl = await _amazonS3Helper.UploadFileAsync(add.Image);
            //save slide
            await _unitOfWork.SlideRepository.Add(_entityMapper.Slide(add));
            _unitOfWork.SaveChanges();
            //succes
            return new Response<string>("Succes", message:"Slide Agregado");
        }

        public async Task<Response<DetailSlideDTO>> Update(UpdateSlideDTO data, int id)
        {
            var slide = await _unitOfWork.SlideRepository.GetById(id);
            if(slide == null || slide.IsDeleted == true)
            {
                return new Response<DetailSlideDTO>(null, false, null, "Not Found");
            }
            slide = _entityMapper.UpdateSlide(slide, data);
            await _unitOfWork.SlideRepository.Update(slide);
            await _unitOfWork.SaveChangesAsync();
            var result = _entityMapper.DetailSlideDTO(slide);
            return new Response<DetailSlideDTO>(result);
        }
    }
}
