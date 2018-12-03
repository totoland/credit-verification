using CreditCardVerification.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CardVerifyCore.Models
{
    public partial class CreditCardContext : DbContext
    {
        private readonly AppSettings _appSettings;

        public virtual DbSet<CreditCard> CreditCard { get; set; }

        public CreditCardContext(IOptions<AppSettings> _appSettings)
        {
            this._appSettings = _appSettings.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(this._appSettings.ConnectionURL);
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
