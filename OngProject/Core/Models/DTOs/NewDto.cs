using System;
using System.Collections.Generic;

namespace OngProject.Core.Models.DTOs
{
    public class NewDto
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
        public List<CommentDto> Comments { get; set; }
    }
}