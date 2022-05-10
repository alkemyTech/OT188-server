using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs
{
    /// <summary>
    /// New Dto
    /// </summary>
    public class CreateNewDto
    {
        /// <summary>
        /// New name of new
        /// </summary>
        /// <example>Novedad 1</example>
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        
        /// <summary>
        /// Novelty content
        /// </summary>
        /// <example>Contenido de la novedad</example>
        [Required]
        public string Content { get; set; }
        
        /// <summary>
        /// New Image
        /// </summary>
        /// <example>Novedad1.jpg</example>
        [Required]
        public IFormFile Image { get; set; }

        /// <summary>
        /// Category id existing
        /// </summary>
        /// <example>1</example>
        [Required]
        public int CategoryId { get; set; }

        //public List<CommentDTO> Comments { get; set; }
    }
}