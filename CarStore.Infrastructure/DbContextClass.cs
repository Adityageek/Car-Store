using CarStore.Core.Models;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace CarStore.Infrastructure
{
    public class DbContextClass : DbContext
    {

        public DbContextClass() { }

        public DbContextClass(DbContextOptions<DbContextClass> contextOptions) : base(contextOptions)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // You don't actually ever need to call this
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-87Q15DS;Database=Car_Store_UOWDemo;integrated security = true; TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Add in memory outbox
            builder.AddInboxStateEntity();
            builder.AddOutboxMessageEntity();
            builder.AddOutboxStateEntity();
        }

        public DbSet<Car> Cars { get; set; }
    }
}
