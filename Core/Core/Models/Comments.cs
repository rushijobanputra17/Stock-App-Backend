using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Comments : Generalnfo
    {
        [Key]
        public int CommentId { get; set; }

        public string Content { get; set; }
        public string Title { get; set; }

        public int StockId { get; set; }


        public int CompanyId { get; set; }

    }

    public class CommentSaveModel
    {
        public int CommentId { get; set; }

        public string Content { get; set; }
        public string Title { get; set; }

        public int StockId { get; set; }

    }
}
