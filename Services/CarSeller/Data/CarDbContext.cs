using CarSeller.Entities;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace CarSeller.Data
{
    public class CarDbContext : DbContext
    {
        public CarDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Car> Cars { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Add in memory outbox
            builder.AddInboxStateEntity();
            builder.AddOutboxMessageEntity();
            builder.AddOutboxStateEntity();

            //Add data
            builder.Entity<Car>().HasData(new Car
            {
                Id = Guid.NewGuid(),
                Name = "Test Car",
                ModelYear = 2015,
                Color = "White",
                Price = 100000,
                Description = "Nice Car",
                Image = "https://fastly.picsum.photos/id/111/4400/2656.jpg?hmac=leq8lj40D6cqFq5M_NLXkMYtV-30TtOOnzklhjPaAAQ",
                Kilometeres = 10100,
                Status = Status.Available,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });
        }
    }
}
