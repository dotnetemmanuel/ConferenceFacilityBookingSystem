using Microsoft.EntityFrameworkCore;

namespace BookingApp.Models
{
    internal class BookingsContext : DbContext
    {
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Week> Weeks { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<FacilitySchedule> FacilitySchedules { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=BookingApp;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Facility>().Property(p => p.Price)
                .HasColumnType("decimal(10,2)");
        }
    }
}
