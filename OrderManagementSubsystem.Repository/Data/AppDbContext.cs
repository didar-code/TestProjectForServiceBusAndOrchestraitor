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

        
                entity.HasMany(x => x.Items)
                    .WithOne()
                    .HasForeignKey(i => i.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);

             
                entity.Metadata
                    .FindNavigation(nameof(OrderAggregatorsRoot.Items))!
                    .SetPropertyAccessMode(PropertyAccessMode.Field);
            });

        
            modelBuilder.Entity<OrderItemAggregator>(entity =>
            {
                entity.HasKey(x => x.OrderItemId);

                entity.Property(x => x.OrderItemId)
                    .ValueGeneratedOnAdd();

                entity.Property(x => x.ProductName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(x => x.Quantity)
                    .IsRequired();

                entity.Property(x => x.UnitPrice)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

               
                entity.Ignore(x => x.LineTotal);

                entity.Metadata
                    .FindProperty(nameof(OrderItemAggregator.OrderId))!
                    .SetPropertyAccessMode(PropertyAccessMode.Field);
            });
        }
    }
}
