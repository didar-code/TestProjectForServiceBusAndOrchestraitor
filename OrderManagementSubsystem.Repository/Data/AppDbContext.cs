using Microsoft.EntityFrameworkCore;
using OrderManagementSubsystem.Aggregators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSubsystem.Repository.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<OrderAggregatorsRoot> Orders { get; set; }

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderAggregatorsRoot>(entity =>
            {
                entity.HasKey(x => x.OrderId);

                entity.Property(x => x.CustomerName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(x => x.TotalAmount)
                    .HasColumnType("decimal(18,2)");

                entity.Property(x => x.Status)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(x => x.OrderDate)
                    .IsRequired();
            });
        }
    }
}
