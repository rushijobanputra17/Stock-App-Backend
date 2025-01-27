using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class JsonResponse
    {
       public int Status { get; set; }    

        public string StatusMessage { get; set; }

        public dynamic Data { get; set; }

        public int TotalRecords { get; set; }
    }
}
