namespace BookingApp.Models
{
    internal class FacilitySchedule
    {
        public int Id { get; set; }
        public int FacilityId { get; set; }
        public int WeekId { get; set; }
        public int BookingId { get; set; }

        public Facility Facility { get; set; }
        public Week Week { get; set; }
        public Booking Booking { get; set; }
    }
}
