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

                    // Get distinct room numbers
                    var roomNumbers = week.FacilitySchedules.Select(fs => fs.Facility.RoomNumber).Distinct().ToList();

                    Console.Write("\t");
                    foreach (var roomNumber in roomNumbers)
                    {
                        Console.Write($"Room {roomNumber}\t");
                    }
                    Console.WriteLine();

                    string[] daysOfWeek = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

                    foreach (var day in daysOfWeek)
                    {
                        Console.Write($"{day}\t");

                        foreach (var roomNumber in roomNumbers)
                        {
                            var facilitySchedule = week.FacilitySchedules.FirstOrDefault(fs =>
                                fs.DayOfWeek.Equals(day, StringComparison.OrdinalIgnoreCase) &&
                                fs.Facility.RoomNumber == roomNumber);

                            if (facilitySchedule != null)
                            {
                                Console.Write($"{facilitySchedule.AvailabilityStatus}\t");
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
