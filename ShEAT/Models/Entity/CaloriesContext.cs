using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShEAT.Models.Entity
{
    public class CaloriesContext : DbContext
    {

        public CaloriesContext() : base("DefaultConnection")
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<History> Histories { get; set; }
    }
}