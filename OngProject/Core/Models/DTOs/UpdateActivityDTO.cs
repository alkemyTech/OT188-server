using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs
{
    public class UpdateActivityDTO
    {
        /// <summary>
        /// activity name.
        /// </summary>
        /// <example>webinar xxx</example>
        [MaxLength(255)]
        public string? Name { get; set; }
        /// <summary>
        /// activity content.
        /// </summary>
       
        [MaxLength(65535)]
        public string? Content { get; set; }

        /// <summary>
        /// image file
        /// </summary>
        
        public IFormFile? Image { get; set; }
    }
}
