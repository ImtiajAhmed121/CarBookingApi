using Microsoft.EntityFrameworkCore;

using CarBookingApi.Entities;
using System.Collections.Generic;
namespace CarBookingApi.Data
{
    public class WafiDbContext : DbContext
    {
        public WafiDbContext(DbContextOptions<WafiDbContext>options) :base(options){ }
        public DbSet<Car> Cars => Set<Car>();
        public DbSet<Booking> Bookings => Set<Booking>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Car)
                .WithMany(); //one car many booking
        }


    }
}
