namespace OngProject.Core.Models.DTOs
{
    public class DetailSlideDTO
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string Text { get; set; }
        public int Order { get; set; }
        public int OrganizationId { get; set; }
    }
}
