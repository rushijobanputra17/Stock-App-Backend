using Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comments> StockComments { get; set; }
        public DbSet<Company> Companys { get; set; }

        public DbSet<Portfolio> UserPortfolio { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                    new IdentityRole
                    {
                        Name="SuperAdmin",
                        NormalizedName="SUPERADMIN"
                    },
                      new IdentityRole
                    {
                        Name="CompanyAdmin",
                        NormalizedName="COMPANYADMIN"
                    }
                      ,  new IdentityRole
                       {
                        Name="user",
                        NormalizedName="USER"
                    },
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
