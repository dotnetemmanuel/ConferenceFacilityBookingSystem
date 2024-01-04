using BookingApp.Models;

namespace BookingApp.Helpers
{
    internal class Information
    {
        public static void ViewBookings()
        {

        }

        public static void ViewStatistics()
        {

        }

        public static void ViewFacilitySchedule(int week)
        {
            using (var dbContext = new BookingsContext())
            {
                var facilities = dbContext.Facilities.ToList();

                Console.Write("\t".PadRight(9));
                foreach (var facility in facilities)
                {
                    Console.Write($"{facility.Name.PadRight(20)}\t");
                }
                Console.WriteLine();

                var weekAvailability = dbContext.Weeks.FirstOrDefault();

                if (weekAvailability != null)
                {
                    var properties = typeof(Week).GetProperties();

                    foreach (var property in properties)
                    {
                        if (property.Name != "Id") // Skip the property with the name "Id"
                        {
                            Console.Write($"{property.Name.PadRight(12)}\t");

                            foreach (var facility in facilities)
                            {
                                var availability = property.GetValue(weekAvailability);
                                Console.Write($"{availability.ToString().PadRight(20)}\t");
                            }
                            Console.WriteLine();
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No availability data found for the week.");
                }
            }
        }
    }
}
