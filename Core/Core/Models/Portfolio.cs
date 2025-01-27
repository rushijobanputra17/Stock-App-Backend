using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Portfolio
    {
        [Key]
        public int PortfolioId { get; set; }
        public string UserId { get; set; }

        public int StockId { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public Decimal PurchasedPrice { get; set; }

        public int Quantity { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
