using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShEAT.Models.Entity
{
    public class CaloriesContext : DbContext
    {
        public DbSet<User> Books { get; set; }
        public DbSet<History> Purchases { get; set; }
    }
}