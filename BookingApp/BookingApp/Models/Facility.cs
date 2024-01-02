namespace BookingApp.Models
{
    internal class Facility
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RoomNumber { get; set; }
        public int Capacity { get; set; }
        public bool Projector { get; set; }
        public bool Whiteboard { get; set; }
        public bool VCSoundSystem { get; set; }
        public bool SpeakerMicrophone { get; set; }
        public decimal Price { get; set; }
    }
}
