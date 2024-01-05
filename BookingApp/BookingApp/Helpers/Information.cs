using BookingApp.Models;
using Microsoft.EntityFrameworkCore;

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

        public static void ViewWeeklySchedule(int weekNumber)
        {
            using (var dbContext = new BookingsContext())
            {
                var week = dbContext.Weeks
                    .Include(w => w.FacilitySchedules)
                    .ThenInclude(fs => fs.Facility)
                    .FirstOrDefault(w => w.Id == weekNumber);

                if (week != null)
                {
                    Console.WriteLine($"Week: {week.Id}");

                    var roomNumbers = week.FacilitySchedules.Select(fs => fs.Facility.RoomNumber).Distinct().OrderBy(r => r).ToList();

                    Console.Write("\t\t".PadLeft(0));
                    foreach (var roomNumber in roomNumbers)
                    {
                        Console.Write($"Room {roomNumber}\t".PadRight(19)); // Padding added
                    }
                    Console.WriteLine();


                    string[] daysOfWeek = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

                    foreach (var day in daysOfWeek)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write($"{day}".PadRight(16));

                        foreach (var roomNumber in roomNumbers)
                        {
                            var facilitySchedule = week.FacilitySchedules.FirstOrDefault(fs =>
                                fs.DayOfWeek.Equals(day, StringComparison.OrdinalIgnoreCase) &&
                                fs.Facility.RoomNumber == roomNumber);

                            if (facilitySchedule != null)
                            {
                                if (facilitySchedule.AvailabilityStatus != "Available")
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    if (facilitySchedule.AvailabilityStatus.Length <= 7)
                                        Console.Write($"{facilitySchedule.AvailabilityStatus}\t".PadRight(26));
                                    else
                                    {
                                        Console.Write($"{facilitySchedule.AvailabilityStatus}\t".PadRight(19));
                                    }
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                                    Console.Write($"{facilitySchedule.AvailabilityStatus}\t".PadRight(20));

                                }
                            }
                        }
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine($"Week {weekNumber} not found in the database.");
                }
            }
        }

        public static void ViewFacilities()
        {
            Console.Clear();
            Console.WriteLine("Here are the bookable facilities:");
            Console.WriteLine();
            using (var dbContext = new BookingsContext())
            {
                foreach (var facility in dbContext.Facilities)
                {
                    Console.WriteLine($"Facility name: {facility.Name}");
                    Console.WriteLine($"   Number: {facility.RoomNumber}");
                    Console.WriteLine($"   Capacity: {facility.Capacity}");
                    Console.WriteLine($"   Projector: {(facility.Projector ? "Yes" : "No")}");
                    Console.WriteLine($"   Price: {facility.Price} SEK per day");
                    Console.WriteLine();
                }
                Console.WriteLine("Press any key to go back");
                Console.ReadKey();

            }
        }
    }
}
