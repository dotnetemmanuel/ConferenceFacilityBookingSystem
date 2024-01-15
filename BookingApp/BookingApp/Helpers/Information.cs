using BookingApp.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Helpers
{
    internal class Information
    {
        static string connString = "data source = .\\SQLEXPRESS; initial catalog = FacilityBookingApp; persist security info = True; Integrated Security = True; TrustServerCertificate=True;";

        public static void ViewBookings()
        {
            using (var dbContext = new BookingsContext())
            {
                if (dbContext.Bookings.Any())
                {
                    var bookings = dbContext.Bookings.Include(f => f.Facility).Include(fs => fs.FacilitySchedule).Include(c => c.Customer).ToList();

                    foreach (var booking in bookings)
                    {
                        Console.WriteLine($"Booking number: {booking.Id}");
                        Console.WriteLine($"Week: {booking.WeekId}");
                        Console.WriteLine($"Day: {booking.FacilitySchedule.DayOfWeek}");
                        Console.WriteLine($"Customer: {booking.FacilitySchedule.AvailabilityStatus}");
                        Console.WriteLine($"Facility: {booking.Facility.Name}");
                        Console.WriteLine($"Room number: {booking.Facility.RoomNumber}");
                        Console.WriteLine($"Booking Price: {booking.Facility.Price} SEK");
                        Console.WriteLine($"Business customer: {(booking.Customer.IsBusinessCustomer ? "yes" : "no")}");
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("No bookings found");
                }
            }
            Console.ReadKey();
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

        public static void ViewCustomerBookings()
        {
            Console.Clear();
            Console.WriteLine("Bookings for " + Helpers.LogIn.customerFirstName + " " + Helpers.LogIn.customerLastName + "\n");
            using (var dbContext = new BookingsContext())
            {
                if (dbContext.Bookings.Any())
                {
                    var bookings = dbContext.Bookings.Include(f => f.Facility).Include(fs => fs.FacilitySchedule).Where(c => c.Customer.LastName == Helpers.LogIn.customerLastName).ToList();

                    foreach (var booking in bookings)
                    {
                        Console.WriteLine($"Booking number: {booking.Id}");
                        Console.WriteLine($"Week: {booking.WeekId}");
                        Console.WriteLine($"Day: {booking.FacilitySchedule.DayOfWeek}");
                        Console.WriteLine($"Customer: {booking.FacilitySchedule.AvailabilityStatus}");
                        Console.WriteLine($"Facility: {booking.Facility.Name}");
                        Console.WriteLine($"Room number: {booking.Facility.RoomNumber}");
                        Console.WriteLine($"Booking Price: {booking.Facility.Price} SEK");
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("You do not have any bookings");
                }

            }
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        public static void ViewStatistics()
        {
            Console.Clear();
            //Top 3 most popular facilities
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Top 3 most popular facilities:");
            Console.ForegroundColor = ConsoleColor.White;
            List<dynamic> mostPopularFacilities = Helpers.Information.ShowMostPopularFacility();
            foreach (var facility in mostPopularFacilities)
            {
                Console.WriteLine($"{facility.Name}: {facility.BookingCounts} bookings");
            }
            Console.WriteLine();

            //Top 5 most loyal customers
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Top 5 most loyal customers:");
            Console.ForegroundColor = ConsoleColor.White;
            List<dynamic> mostLoyalCustomers = Helpers.Information.ShowMostLoyalCustomers();
            foreach (var customer in mostLoyalCustomers)
            {
                Console.WriteLine($"{customer.Customer}: {customer.BookingCount} bookings");
            }
            Console.WriteLine();

            //Percentage of conference center booked
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Percentage of conference center booked");
            Console.ForegroundColor = ConsoleColor.White;
            List<dynamic> percentageBooked = Helpers.Information.ShowPercentageBooked();
            foreach (var total in percentageBooked)
            {
                Console.WriteLine($"Booking percentage for the year: {total.PercentageBooked}% as of {DateTime.Today.ToString("yyy-MM-dd")}");
            }
            Console.WriteLine();

            //Percentage of facilities booked with a capacity of at least 50
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Booking frequency of facilities with a capacity of at least 50");
            Console.ForegroundColor = ConsoleColor.White;
            List<dynamic> percentageBookedCapacity = Helpers.Information.ShowPercentageBookedCapacity();
            foreach (var facility in percentageBookedCapacity)
            {
                Console.WriteLine($"{facility.Name}: {facility.PercentageBookedCapacity}%");
            }
            Console.WriteLine();
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
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

        //Statistics for admin
        public static List<dynamic> ShowMostPopularFacility()
        {
            string sql = @"
                        SELECT TOP 3
                            f.Name,
	                        COUNT(b.FacilityId) AS 'BookingCounts' 
                        FROM 
                            Bookings b
                        JOIN Facilities f ON b.FacilityId = f.Id
                        GROUP BY 
                            f.Name
                        ORDER BY 
                            'BookingCounts' DESC";

            List<dynamic> mostPopularFacilities = new List<dynamic>();
            using (var connection = new SqlConnection(connString))
            {

                mostPopularFacilities = connection.Query(sql).ToList();
            }
            return mostPopularFacilities;
        }

        public static List<dynamic> ShowMostLoyalCustomers()
        {
            string sql = @"
                        SELECT TOP 5
                            CONCAT(c.FirstName, ' ', c.LastName) AS Customer,
                            COUNT(b.CustomerId) AS 'BookingCount'
                        FROM 
                            Bookings b
                        JOIN Customers c ON b.CustomerId = c.Id
                        GROUP BY 
                            c.FirstName,
                            c.LastName
                        ORDER BY 
                            BookingCount DESC";

            List<dynamic> mostLoyalCustomers = new List<dynamic>();
            using (var connection = new SqlConnection(connString))
            {
                mostLoyalCustomers = connection.Query(sql).ToList();
            }
            return mostLoyalCustomers;
        }

        public static List<dynamic> ShowPercentageBooked()
        {
            string sql = @"
                        SELECT
                            Ceiling((COUNT(CASE WHEN fs.AvailabilityStatus <> 'Available' THEN 1 END) * 100.0) / COUNT(*)) AS PercentageBooked
                        FROM
                            FacilitySchedules fs";

            List<dynamic> percentageBooked = new List<dynamic>();
            using (var connection = new SqlConnection(connString))
            {
                percentageBooked = connection.Query(sql).ToList();
            }
            return percentageBooked;
        }

        public static List<dynamic> ShowPercentageBookedCapacity()
        {
            string sql = @"
                        SELECT 
                            f.Name,
                            COUNT(*) AS BookingCount,
	                        format(ROUND((COUNT(*) * 100.0) / (SELECT COUNT(*) FROM Bookings), 2),'N') AS PercentageBookedCapacity
                        FROM 
                            Bookings b
                        JOIN FacilitySchedules fs ON b.FacilityScheduleId = fs.Id
                        JOIN Facilities f ON b.FacilityId = f.Id
	                    WHERE
                            f.Capacity>=50
	                    GROUP BY
                            f.Name";

            List<dynamic> percentageBookedCapacity = new List<dynamic>();
            using (var connection = new SqlConnection(connString))
            {
                percentageBookedCapacity = connection.Query(sql).ToList();
            }
            return percentageBookedCapacity;
        }
    }
}
