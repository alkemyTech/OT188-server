using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs
{
    public class NewTestimonyDto
    {
        /// <summary>
        /// Name of person
        /// </summary>
        /// <example>Ana</example>
        [Required(ErrorMessage = "Name field is required")]
        [MaxLength(255)]
        public string Name { get; set; }

        ///<summary>
        ///Descriptive image (optional)
        ///</summary>
        public IFormFile Image { get; set; }
        /// <summary>
        /// Testimony of person
        /// </summary>
        /// <example>Testimony of Ana</example>
        [Required(ErrorMessage = "Description field is required")]
        [MaxLength(255)]
        public string Description { get; set; }
    }
}
