using CreditCardVerification.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CardVerifyCore.Models
{
    public partial class CreditCardContext : DbContext
    {
        public virtual DbSet<CreditCard> CreditCard { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=totodev.com;Initial Catalog=2C2P;User ID=sa;Password=P@ssw0rd;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CreditCard>(entity =>
            {
                entity.HasKey(e => e.CardNumber);

                entity.ToTable("CREDIT_CARD");

                entity.Property(e => e.CardNumber)
                    .HasMaxLength(16)
                    .ValueGeneratedNever();
            });
        }
    }
}
