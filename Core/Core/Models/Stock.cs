using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Stock : Generalnfo
    {

        public Stock()
        {
            Comments = new List<Comments>();

        }

        public string StockName { get; set; }
        public long  MarketCap { get; set; }
        

        [Column(TypeName ="decimal(18,4)")]
        public Decimal StockPrice { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public Decimal LastDevidend { get; set; }

        [Key]
        public int StockId { get; set; }

        public string StockType
        {
            get; set;
        }

        public string Symbol
        {
            get; set;
        }

        public DateTime? LastUpdatedTime { get; set; }  


        public  List<Comments> Comments { get; set; }

        public int CompanyId { get; set; }

        public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();

    }
}
