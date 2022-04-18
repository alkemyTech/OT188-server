using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Entities
{
    public class Testimony: EntityBase
    {
        [Required]
        [DataType(DataType.Text)]
        [StringLength(255)]
        public string Name { get; set; }
        
        [MaxLength(255)]
        public string Image { get; set; } 

        [MaxLength(255)]
        public string Description { get; set; }
    }
}