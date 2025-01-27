using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helpers
{
    public class QueryParameters
    {
        public  string? Name  { get; set; }

        public string? Symbol { get; set; }

        public string? SortBy { get; set; }

        public bool IsDecending { get; set; }

        public int PageNumber { get; set; } = 1;

        public int Pagesize { get; set; } = 20;
    }
}
