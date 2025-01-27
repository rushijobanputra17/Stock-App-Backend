using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class StockCreateModel
    {

        public int StockId { get; set; }    

        public string StockName { get; set; }
        public long MarketCap { get; set; }


       
        public Decimal StockPrice { get; set; }

  
        public Decimal LastDevidend { get; set; }

   

        public string StockType
        {
            get; set;
        }

        public string Symbol
        {
            get; set;
        }

        public DateTime? LastUpdatedTime { get; set; }


      

        public int CompanyId { get; set; }
    }
}
