using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace OngProject.Core.Models.DTOs
{
    public class RegisterDto
    {
        /// <summary>
        /// User first name.
        /// </summary>
        /// <example>Agustin</example>
        [Required(ErrorMessage = "El campo Nombre es requerido.")]
        public string FirstName { get; set; }
        /// <summary>
        /// User last name.
        /// </summary>
        /// <example>Barraza</example>
        [Required(ErrorMessage = "El campo Apellido es requerido.")]
        public string LastName { get; set; }
        /// <summary>
        /// User email.
        /// </summary>
        /// <example>user@email.com</example>
        [Required(ErrorMessage = "El campo Email es requerido.")]
        [StringLength(320, ErrorMessage = "Alcanzo la maxima cantidad de caracteres.")]
        [EmailAddress(ErrorMessage = "El campo de Email no es una dirección de correo electrónico válida.")]
        public string Email { get; set; }
        /// <summary>
        /// User password.
        /// </summary>
        [Required(ErrorMessage = "El campo Contraseña es requerido.")]
        [StringLength(20, ErrorMessage = "Alcanzo la maxima cantidad de caracteres.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        /// <summary>
        /// User photo. (optional)
        /// </summary>
        public IFormFile Photo { get; set; }
    }
}
