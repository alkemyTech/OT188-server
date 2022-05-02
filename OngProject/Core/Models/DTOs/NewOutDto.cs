using System;
using System.Collections.Generic;

namespace OngProject.Core.Models.DTOs
{
    public class NewOutDto
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
        public List<CommentOutDto> Comments { get; set; }
    }
}