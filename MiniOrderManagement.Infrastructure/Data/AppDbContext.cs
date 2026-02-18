using Microsoft.EntityFrameworkCore;
using MiniOrderManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniOrderManagement.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options): base(options) 
        {
            
        }

        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<CustomerProfile> Profiles => Set<CustomerProfile>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Orders)
                .WithOne()
                .HasForeignKey(o => o.CustomerId);

            modelBuilder.Entity<Customer>()
                .HasOne(c => c.Profile)
                .WithOne()
                .HasForeignKey<CustomerProfile>(p => p.CustomerId);
        }
    }
}
