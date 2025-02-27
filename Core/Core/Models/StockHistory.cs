using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public  class StockHistory
    {
        [Key]

        public int StockHistoryId { get; set; }

        public int StockId { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal StockPrice { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
