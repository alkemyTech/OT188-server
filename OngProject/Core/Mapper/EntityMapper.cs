using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Mapper
{
    public class EntityMapper : IEntityMapper
    {
        public SlideDTO SlidetoSlideDTO(Slide slide)
        {
            var slideDTO = new SlideDTO()
            {
                ImageUrl = slide.ImageUrl,
                Order = slide.Order
           };  
            return slideDTO;
        }
    }
}
