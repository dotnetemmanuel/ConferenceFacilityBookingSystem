namespace BookingApp.Models
{
    internal class Booking
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public int FacilityId { get; set; }
        public Facility? Facility { get; set; }

        public int WeekId { get; set; }
        public Week? Week { get; set; }

        public int FacilityScheduleId { get; set; }
        public FacilitySchedule? FacilitySchedule { get; set; }
    }
}
