using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.Pagination
{
    public class PagedListParams
    {
        const int MaxPageSize = 20;

        [Range(1, int.MaxValue, ErrorMessage = "Value must be greater than {1}")]
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;

        [Range(1, int.MaxValue)]
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}
