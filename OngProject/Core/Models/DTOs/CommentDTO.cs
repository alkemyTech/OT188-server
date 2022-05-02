namespace OngProject.Core.Models.DTOs
{
    public class CommentDTO
    {
       
        public string Body { get; set; }

        public int IdUser { get; set; }
      
        public UserDto User { get; set; }

        public int NewId { get; set; }
        public NewDTO New { get; set; }
    }
}
