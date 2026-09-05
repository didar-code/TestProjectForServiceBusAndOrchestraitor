using AuthManagementSubSystem.Aggregators;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace AuthManagementSubSystem.Repository.Data
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(
            DbContextOptions<AuthDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserAggregatorsRoot> Users => Set<UserAggregatorsRoot>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserAggregatorsRoot>(
                entity =>
                {
                    entity.HasKey(x => x.UserId);

                    entity.Property(x => x.UserName)
                        .IsRequired()
                        .HasMaxLength(100);

                    entity.Property(x => x.Email)
                        .IsRequired()
                        .HasMaxLength(200);

                    entity.HasIndex(x => x.Email)
                        .IsUnique();

                    entity.Property(x => x.PasswordHash)
                        .IsRequired();

                    entity.Property(x => x.Role)
                        .IsRequired()
                        .HasMaxLength(50);

                    entity.Property(x => x.IsActive)
                        .IsRequired();

                    entity.Property(x => x.CreateDate)
                        .IsRequired();
                });
        }
    }
}
