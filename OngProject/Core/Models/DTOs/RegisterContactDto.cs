using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs
{
    public class RegisterContactDto
    {
        [Required(ErrorMessage = "El campo Nombre es requerido.")]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required(ErrorMessage = "El campo E-mail es requerido.")]
        [EmailAddress]
        [MaxLength(320)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Debe enviar un mensaje en su contacto.")]
        [StringLength(500)]
        public string Message { get; set; }

        [RegularExpression(@"^([0-9]{0,20})$", ErrorMessage = "Número de teléfono incorrecto.")]
        public int? Phone { get; set; }
    }
}
