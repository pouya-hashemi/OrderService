using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beta.OrderService.Application.Common
{
    public class PaginatedList<T>
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int TotalRows { get; set; }
        public IEnumerable<T> List { get; set; }
    }
}
