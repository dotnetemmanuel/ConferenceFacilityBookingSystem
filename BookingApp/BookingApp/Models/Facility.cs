namespace BookingApp.Models
{
    internal class Facility
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? RoomNumber { get; set; }
        public int Capacity { get; set; }
        public bool Projector { get; set; }
        public decimal Price { get; set; }

        public ICollection<FacilitySchedule>? FacilitySchedules { get; set; }
    }
}
