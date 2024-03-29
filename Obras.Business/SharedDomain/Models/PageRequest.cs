﻿namespace Obras.Business.SharedDomain.Models
{
    public class PageRequest<T, E>
    {
        public PaginationDetails Pagination { get; set; }
        public T Filter { get; set; }
        public SortingDetails<E> OrderBy { get; set; }
    }
}
