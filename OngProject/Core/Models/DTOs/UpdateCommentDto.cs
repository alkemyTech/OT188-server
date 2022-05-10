using System;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs
{
    public class UpdateCommentDto
    {

        [MaxLength(65535)]
        public string Body { get; set; }
    }
}
