using System.Collections.Generic;

namespace API.Models
{
    public class PaginationSet<T>
    {
        public int PageIndex { set; get; }
        public int PageSize { get; set; }
        public long TotalRows { set; get; }
        public IEnumerable<T> Items { set; get; }
    }
}
