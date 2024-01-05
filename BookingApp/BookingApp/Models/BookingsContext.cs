using Microsoft.EntityFrameworkCore;

namespace BookingApp.Models
{
    internal class BookingsContext : DbContext
    {
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<FacilitySchedule> FacilitySchedules { get; set; }
        public DbSet<Week> Weeks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=FacilityBookingApp;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Facility>().Property(p => p.Price)
                .HasColumnType("decimal(10,2)");

            modelBuilder.Entity<FacilitySchedule>()
                .HasMany(fs => fs.Bookings)
                .WithOne(b => b.FacilitySchedule)
                .HasForeignKey(b => b.FacilityScheduleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.FacilitySchedule)
                .WithMany(fs => fs.Bookings)
                .HasForeignKey(b => b.FacilityScheduleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
