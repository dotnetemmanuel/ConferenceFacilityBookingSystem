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
            //optionsBuilder.UseSqlServer(@"Server=tcp:emmanuelducheneserver.database.windows.net,1433;Initial Catalog=FacilityBookingApp;Persist Security Info=False;User ID=emm_duc;Password=9_wK:ztu8SWZS6k;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Sets the 'Price' property in 'Facility' to store decimal numbers with 10 digits and 2 decimal places
            modelBuilder.Entity<Facility>().Property(p => p.Price)
                .HasColumnType("decimal(10,2)");


            //Had problems with constraints during update-database so had to implement this to make it work 
            //When a 'FacilitySchedule' is deleted, all associated 'Bookings' are also deleted (cascade delete)
            modelBuilder.Entity<FacilitySchedule>()
                .HasMany(fs => fs.Bookings)
                .WithOne(b => b.FacilitySchedule)
                .HasForeignKey(b => b.FacilityScheduleId)
                .OnDelete(DeleteBehavior.Cascade);

            //Each 'Booking' belongs to one 'FacilitySchedule'
            //Deletion of a 'FacilitySchedule' linked to a 'Booking' is restricted (prevent deletion)
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.FacilitySchedule)
                .WithMany(fs => fs.Bookings)
                .HasForeignKey(b => b.FacilityScheduleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
