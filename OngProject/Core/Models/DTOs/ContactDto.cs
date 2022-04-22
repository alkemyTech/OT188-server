using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs
{
    public class ContactDto
    {
        [Required(ErrorMessage = "El campo Nombre es requerido.")]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required(ErrorMessage = "El campo E-mail es requerido.")]
        [EmailAddress]
        [MaxLength(320)]
        public string Email { get; set; }
    }
}