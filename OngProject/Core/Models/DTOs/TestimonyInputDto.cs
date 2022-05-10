using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs
{
    public class TestimonyInputDto
    {
        /// <summary>
        /// New name of person
        /// </summary>
        /// <example>New name</example>
        [Required(ErrorMessage = "Name field is required")]
        [StringLength(255)]
        public string Name { get; set; }
        ///<summary>
        ///New Descriptive image (optional)
        ///</summary>
        public IFormFile Image { get; set; }
        /// <summary>
        /// New testimony of person
        /// </summary>
        /// <example>New testimony</example>
        [Required(ErrorMessage = "Description field is required")]
        [MaxLength(255)]
        public string Description { get; set; }

    }
}
