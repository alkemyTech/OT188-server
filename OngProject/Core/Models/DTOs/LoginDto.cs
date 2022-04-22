using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Models.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage ="El campo email es requerido.")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage ="El campo Contraseña es requerido.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
