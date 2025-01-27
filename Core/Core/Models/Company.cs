using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public  class Company : Generalnfo
    {
        [Key]
        public int CompanyId { get; set; }

        public string CompanyName { get; set; }

        public int IndustryId {  get; set; }
        
    }
}
