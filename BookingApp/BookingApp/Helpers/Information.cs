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

        public static void PrintWeeklySchedule(int weekNumber)
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
                                    Console.Write($"{facilitySchedule.AvailabilityStatus}\t".PadRight(26));
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                                    Console.Write($"{facilitySchedule.AvailabilityStatus}\t".PadRight(20));

                                }
                            }
                        }
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine($"Week {weekNumber} not found in the database.");
                }
            }
        }
    }
}
