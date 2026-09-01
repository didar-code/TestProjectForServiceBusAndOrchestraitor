using Microsoft.EntityFrameworkCore;
using PaymentManagementSubSystem.Aggregators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace PaymentManagementSubSystem.Repository.Data
{
    public class PaymentDbContext : DbContext
    {
        public PaymentDbContext(
            DbContextOptions<PaymentDbContext> options)
            : base(options)
        {
        }

        public DbSet<PaymentAggregatorsRoot> Payments { get; set; }

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PaymentAggregatorsRoot>(entity =>
            {
                entity.HasKey(x => x.PaymentId);

                entity.Property(x => x.PaymentId)
                    .ValueGeneratedOnAdd();

                entity.Property(x => x.OrderId)
                    .IsRequired();

                entity.Property(x => x.Amount)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                entity.Property(x => x.PaymentMethod)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(x => x.Status)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(x => x.PaymentDate)
                    .IsRequired();
            });
        }
    }
}
