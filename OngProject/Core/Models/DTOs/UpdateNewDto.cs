using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Models.DTOs
{
    public class UpdateNewDto
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public IFormFile Image { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}
