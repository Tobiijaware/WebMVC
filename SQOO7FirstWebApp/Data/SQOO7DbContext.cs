using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SQOO7FirstWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQOO7FirstWebApp.Data
{
    public class SQOO7DbContext : IdentityDbContext<Employee>
    {
        public SQOO7DbContext(DbContextOptions<SQOO7DbContext> options) : base(options) {}
        public DbSet<Address> Address { get; set; } // adding more tables to the database
        public DbSet<ClaimsList> ClaimsLists { get; set; } // adding more tables to the database

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
