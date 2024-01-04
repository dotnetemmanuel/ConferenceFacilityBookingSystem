using BookingApp.Models;

namespace BookingApp.Helpers
{
    internal class AddData
    {
        public static void AddAdmin()
        {
            Console.WriteLine("Please enter your first name");
            string firstName = Console.ReadLine();

            Console.WriteLine("Please enter your last name");
            string lastName = Console.ReadLine();

            Console.WriteLine("Please enter your e-mail");
            string email = Console.ReadLine();

            Console.WriteLine("Please enter your username");
            string username = Console.ReadLine();

            Console.WriteLine("Please enter your password");
            string password = Console.ReadLine();

            using (var dbContext = new BookingsContext())
            {
                var newAdmin = new Administrator()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = username,
                    Password = password,

                };
                dbContext.Administrators.Add(newAdmin);
                dbContext.SaveChanges();

                Console.WriteLine("Thank you, your admin account has been created!");
                Console.ReadLine();
            }
        }

        public static void AddCustomer()
        {
            Console.WriteLine("Please enter your first name");
            string firstName = Console.ReadLine();

            Console.WriteLine("Please enter your last name");
            string lastName = Console.ReadLine();

            Console.WriteLine("Please enter your e-mail");
            string email = Console.ReadLine();

            Console.WriteLine("Please enter your username");
            string username = Console.ReadLine();

            Console.WriteLine("Please enter your password");
            string password = Console.ReadLine();

            Console.WriteLine("Please enter your address");
            string address = Console.ReadLine();

            bool isBusinessCustomer = false;
            bool isValidInput = false;

            while (!isValidInput)
            {
                Console.WriteLine("Are you a business customer? (y/n)");
                string businessCustomer = Console.ReadLine().ToLower();

                if (businessCustomer == "y")
                {
                    isBusinessCustomer = true;
                    isValidInput = true;
                }
                else if (businessCustomer == "n")
                {
                    isBusinessCustomer = false;
                    isValidInput = true;
                }
                else
                {
                    Console.WriteLine("Wrong input, please enter 'Y' or 'N'");
                }
            }


            using (var dbContext = new BookingsContext())
            {
                var newCustomer = new Customer
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = username,
                    Password = password,
                    Address = address,
                    IsBusinessCustomer = isBusinessCustomer
                };
                dbContext.Customers.Add(newCustomer);
                dbContext.SaveChanges();

                Console.WriteLine("Thank you, your customer account has been created!");
                Console.ReadLine();
            }
        }

        public static void AddFacility()
        {
            using (var dbContext = new BookingsContext())
            {
                Console.WriteLine("Here are the current facilities:");
                foreach (var facility in dbContext.Facilities)
                {
                    Console.WriteLine($"Name: {facility.Name}");
                    Console.WriteLine($"Room number: {facility.RoomNumber}");
                }
            }

            bool success;
            Console.WriteLine("Please enter the name of the facility");
            string name = Console.ReadLine();

            Console.WriteLine("Please enter the room number (ex.101)");
            int roomNr;
            success = int.TryParse(Console.ReadLine(), out roomNr);

            Console.WriteLine("Please enter the room capacity");
            int capacity;
            success = int.TryParse(Console.ReadLine(), out capacity);

            bool hasProjector = false;
            bool isValidInput = false;

            while (!isValidInput)
            {
                Console.WriteLine("Is the room equipped with a projector? Y/N");
                string projector = Console.ReadLine().ToLower();

                if (projector == "y")
                {
                    hasProjector = true;
                    isValidInput = true;
                }
                else if (projector == "n")
                {
                    hasProjector = false;
                    isValidInput = true;
                }
                else
                {
                    Console.WriteLine("Wrong input, please enter 'Y' or 'N'");
                }
            }

            Console.WriteLine("Please enter a price for the facility (comma separated, max 2 decimal numbers");
            decimal price;
            success = decimal.TryParse(Console.ReadLine(), out price);

            using (var dbContext = new BookingsContext())
            {
                var facility = new Facility()
                {
                    Name = name,
                    RoomNumber = roomNr,
                    Capacity = capacity,
                    Projector = hasProjector,
                    Price = price
                };
                dbContext.Facilities.Add(facility);
                dbContext.SaveChanges();
            }
        }

        public static void AddWeeks()
        {
            using (var dbContext = new BookingsContext())
            {
                for (int i = 1; i <= 52; i++)
                {
                    Week newWeek = new Week
                    {
                        Monday = "Available",
                        Tuesday = "Available",
                        Wednesday = "Available",
                        Thursday = "Available",
                        Friday = "Available",
                        Saturday = "Available",
                        Sunday = "Available"
                    };

                    dbContext.Weeks.Add(newWeek);
                }
                dbContext.SaveChanges();
            }
        }

        public static void AddBooking()
        {
            using (var dbContext = new BookingsContext())
            {
                bool success;
                Console.WriteLine("Please enter the week for your booking");
                success = int.TryParse(Console.ReadLine(), out int week);
                var weekNr = dbContext.Weeks.FirstOrDefault(w => w.Id == week);

                if (weekNr != null)
                {
                    Console.WriteLine($"Please enter the day of week {week} for your booking");
                    string day = Console.ReadLine().Trim().ToLower();

                    // Update the specified day with the user's last name
                    Console.WriteLine("Please enter your last name");
                    string lastName = Console.ReadLine();

                    switch (day)
                    {
                        case "monday":
                            weekNr.Monday = lastName;
                            break;
                        case "tuesday":
                            weekNr.Tuesday = lastName;
                            break;
                        case "wednesday":
                            weekNr.Wednesday = lastName;
                            break;
                        case "thursday":
                            weekNr.Thursday = lastName;
                            break;
                        case "friday":
                            weekNr.Friday = lastName;
                            break;
                        case "saturday":
                            weekNr.Saturday = lastName;
                            break;
                        case "sunday":
                            weekNr.Sunday = lastName;
                            break;
                        default:
                            Console.WriteLine("Invalid day input.");
                            break;
                    }

                    Console.WriteLine("Please enter the number of the room you want to book");
                    success = int.TryParse(Console.ReadLine(), out int roomNr);

                    // Add additional logic for room number if needed

                    // Save changes to the database using Entity Framework
                    dbContext.SaveChanges();
                    Console.WriteLine($"Value for '{day}' updated successfully.");
                }
                else
                {
                    Console.WriteLine($"Week {week} not found in the database.");
                }
            }
        }

        //Bulk add Facility test data
        public static void AddFacilities()
        {
            using (var dbContext = new BookingsContext())
            {
                dbContext.Facilities.AddRange(
                    new Facility
                    {
                        Name = "Auditorium",
                        RoomNumber = 101,
                        Capacity = 200,
                        Projector = true,
                        Price = 6000
                    },
                    new Facility
                    {
                        Name = "The Situation Room",
                        RoomNumber = 2,
                        Capacity = 10,
                        Projector = true,
                        Price = 1000
                    },
                    new Facility
                    {
                        Name = "The Amber Room",
                        RoomNumber = 201,
                        Capacity = 25,
                        Projector = true,
                        Price = 2000
                    },
                    new Facility
                    {
                        Name = "The Oval Office",
                        RoomNumber = 202,
                        Capacity = 5,
                        Projector = false,
                        Price = 750
                    },
                    new Facility
                    {
                        Name = "The Hall of Mirrors",
                        RoomNumber = 301,
                        Capacity = 50,
                        Projector = true,
                        Price = 3500
                    });
                dbContext.SaveChanges();
            }
        }
    }
}
