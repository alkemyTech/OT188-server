using OngProject.Core.Helper;
using System.Collections.Generic;

namespace OngProject.Core.Models.Pagination
{
    public class PagedListResponse<T>
    {
        public string PreviusPage { get; set; }
        public string NextPage { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public List<T> Items { get; set; }
        public PagedListResponse(PagedList<T> pagedListResponse, string url)
        {
            this.Items = pagedListResponse;
            this.TotalPages = pagedListResponse.TotalPages;
            this.PageSize = pagedListResponse.PageSize;
            this.PageNumber = pagedListResponse.CurrentPage;
            if (pagedListResponse.HasNext) this.NextPage = $"{url}?PageNumber={pagedListResponse.CurrentPage + 1}" +
                    $"&PageSize={pagedListResponse.PageSize}";
            if (pagedListResponse.HasPrevius) this.PreviusPage = $"{url}?PageNumber={pagedListResponse.CurrentPage - 1}" +
                    $"&PageSize={pagedListResponse.PageSize}";
        }
    }
}
