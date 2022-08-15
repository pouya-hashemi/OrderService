using Beta.OrderService.Domain.Entities;
using Beta.OrderService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Beta.OrderService.Infrastructure.Persistance.SqlServer
{
    internal class SqlDbContext:DbContext,ISqlDbContext
    {
        public SqlDbContext(DbContextOptions options):base(options)
        {

        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(SqlDbContext)));
            base.OnModelCreating(modelBuilder);
        }

    }
}
