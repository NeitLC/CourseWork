using System;

namespace CourseWork.Domain.Dto
{
    public class Page
    {
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }

        public Page(int count, int pageNumber, int pageSize)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }

        public bool HasNextPage => PageNumber < TotalPages;
        public bool HasPreviousPage => PageNumber > 1;
    }
}