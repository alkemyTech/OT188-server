using System;

namespace OngProject.Core.Models
{
    public class QueryFilter
    {
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
