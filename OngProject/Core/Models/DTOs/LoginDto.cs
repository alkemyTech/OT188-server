using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Models.DTOs
{
    public class LoginDto
    {
        /// <summary>
        /// User account email.
        /// </summary>
        /// <example>user@email.com</example>
        [Required(ErrorMessage = "El campo Email es requerido.")]
        [StringLength(320,ErrorMessage ="Alcanzo la maxima cantidad de caracteres.")]
        [EmailAddress(ErrorMessage = "El campo de Email no es una dirección de correo electrónico válida.")]
        public string Email { get; set; }
        /// <summary>
        /// User password
        /// </summary>
        [Required(ErrorMessage = "El campo Contraseña es requerido.")]
        [StringLength(20,ErrorMessage ="Alcanzo la maxima cantidad de caracteres.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}