using System;
using System.Collections.Generic;
using System.Linq;

namespace OngProject.Core.Helper
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public bool HasPrevius => (CurrentPage > 1 && CurrentPage <= TotalPages);
        public bool HasNext => (CurrentPage < TotalPages);

        public PagedList(ICollection<T> items, int totalCount, int pageNumber, int pageSize)
        {
            TotalCount = totalCount;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(this.TotalCount / (double)PageSize);
            AddRange(items);
        }

        public static PagedList<T> Create(ICollection<T> source, int totalCount,int pageNumber, int pageSize)
        {
            return new PagedList<T>(source, totalCount, pageNumber, pageSize);
        }
    }  
}
