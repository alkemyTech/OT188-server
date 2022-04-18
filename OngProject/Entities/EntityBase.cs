using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Entities
{
    public class EntityBase
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime ModifiedAt { get; set; }
        [Required]
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

         [Required]
        public DateTime DeletedAt { get; set; }
    }
}
