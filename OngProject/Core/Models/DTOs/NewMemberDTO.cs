using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs
{
    public class NewMemberDTO
    {
        /// <summary>
        /// Name of member
        /// </summary>
        /// <example>Ana</example>
        [Required(ErrorMessage = "\"Name\" field is required")]
        [MaxLength(255)]
        public string Name { get; set; }
        /// <summary>
        /// Facebook link of the member
        /// </summary>
        /// <example>http://facebook.com/ana</example>
        [MaxLength(255)]
        public string FacebookUrl { get; set; }
        /// <summary>
        /// Facebook link of the member
        /// </summary>
        /// <example>http://instagram.com/ana</example>
        [MaxLength(255)]
        public string InstagramUrl { get; set; }
        /// <summary>
        /// linkedin url of the member
        /// </summary>
        /// <example>http://linkedin.com/ana</example>
        [MaxLength(255)]
        public string LinkedinUrl { get; set; }
        ///<summary>
        ///New Descriptive image (optional)
        ///</summary>
        public IFormFile Image { get; set; }
        /// <summary>
        /// Description of member
        /// </summary>
        /// <example>I'm a friendly person...</example>
        [MaxLength(255)]
        public string Description { get; set; }
    }
}
