using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public  class AddUserAccount
    {

        public string FirstName { get; set; } = null;
        public string LastName { get; set; } = null;

        [Required]
        public string? UserName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
