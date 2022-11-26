using BBGymManagement.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BBGymManagement.Models.Context
{
    public class EFDbContext:DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Rol> Rols { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}