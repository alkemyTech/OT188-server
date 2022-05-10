﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs
{
    public class TestimonyInputDto
    {
        [Required(ErrorMessage = "Name field is required")]
        [StringLength(255)]
        public string Name { get; set; }

        public IFormFile Image { get; set; }

        [Required(ErrorMessage = "Description field is required")]
        [MaxLength(255)]
        public string Description { get; set; }

    }
}