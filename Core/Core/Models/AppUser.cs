using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class AppUser : IdentityUser
    {
       
        public string FirstName { get; set; } = null;
        public string LastName { get; set; } = null;

        public string RefreshToken { get; set; } = null;

        public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
    }
}
