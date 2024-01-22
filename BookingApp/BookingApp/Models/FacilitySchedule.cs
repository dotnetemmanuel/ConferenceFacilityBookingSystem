namespace BookingApp.Models
{
    internal class FacilitySchedule
    {
        public int Id { get; set; }
        public int FacilityId { get; set; }
        public Facility? Facility { get; set; }
        public int WeekId { get; set; }
        public Week? Week { get; set; }
        public string? DayOfWeek { get; set; }
        public string? AvailabilityStatus { get; set; } = "Available";

        public ICollection<Booking>? Bookings { get; set; }
    }
}
