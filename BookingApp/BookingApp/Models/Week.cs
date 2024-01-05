namespace BookingApp.Models
{
    internal class Week
    {
        public int Id { get; set; }
        public ICollection<FacilitySchedule>? FacilitySchedules { get; set; }
    }
}
