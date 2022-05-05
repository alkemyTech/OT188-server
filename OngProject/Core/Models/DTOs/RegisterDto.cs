using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace OngProject.Core.Models.DTOs
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "El campo Nombre es requerido.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "El campo Apellido es requerido.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "El campo Email es requerido.")]
        [StringLength(320, ErrorMessage = "Alcanzo la maxima cantidad de caracteres.")]
        [EmailAddress(ErrorMessage = "El campo de Email no es una dirección de correo electrónico válida.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "El campo Contraseña es requerido.")]
        [StringLength(20, ErrorMessage = "Alcanzo la maxima cantidad de caracteres.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public IFormFile Photo { get; set; }
    }
}
