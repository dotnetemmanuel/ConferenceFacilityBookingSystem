﻿using BookingApp.Models;

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
                success = int.TryParse(Console.ReadLine(), out int weekNumber);

                var week = dbContext.Weeks.FirstOrDefault(w => w.Id == weekNumber);

                if (week != null)
                {
                    Console.WriteLine($"Please enter your last name");
                    string lastName = Console.ReadLine().Trim().ToLower(); // Normalize input

                    Console.WriteLine("Please enter the room number");
                    success = int.TryParse(Console.ReadLine(), out int roomNr);

                    var facility = dbContext.Facilities.FirstOrDefault(f => f.RoomNumber == roomNr);

                    if (facility != null)
                    {
                        var customer = dbContext.Customers.FirstOrDefault(c =>
                            c.LastName.ToLower() == lastName);

                        if (customer != null)
                        {
                            // Update the availability status for the specified day in the week
                            Console.WriteLine($"Please enter the day of the week for your booking");
                            string dayOfWeek = Console.ReadLine().Trim(); // Normalize input

                            var facilitySchedule = dbContext.FacilitySchedules.FirstOrDefault(fs =>
                                fs.WeekId == weekNumber &&
                                fs.FacilityId == facility.Id &&
                                fs.DayOfWeek.ToLower() == dayOfWeek.ToLower());

                            if (facilitySchedule != null)
                            {
                                // Check if the facility is available
                                if (facilitySchedule.AvailabilityStatus == "Available")
                                {
                                    facilitySchedule.AvailabilityStatus = lastName;
                                    var booking = new Booking
                                    {
                                        CustomerId = customer.Id,
                                        FacilityId = facility.Id,
                                        // No DateTime needed for the booking
                                    };

                                    dbContext.Bookings.Add(booking);
                                    dbContext.SaveChanges();
                                    Console.WriteLine($"Booking updated successfully for {dayOfWeek}.");
                                }
                                else
                                {
                                    Console.WriteLine($"The facility is already booked for {dayOfWeek}.");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Facility schedule not found for room number {roomNr} in week {weekNumber} on {dayOfWeek}.");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Customer with last name {lastName} not found.");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Facility with room number {roomNr} not found.");
                    }
                }
                else
                {
                    Console.WriteLine($"Week {weekNumber} not found in the database.");
                }
            }
        }

        //Bulk add test data
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
                        RoomNumber = 102,
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
        public static void AddCustomers()
        {
            using (var dbContext = new BookingsContext())
            {
                dbContext.Customers.AddRange(
                    new Customer
                    {
                        FirstName = "Emmanuel",
                        LastName = "Duchene",
                        Email = "emmanuel.duchene@gmail.com",
                        UserName = "emmduc",
                        Password = "abc123",
                        Address = "Stighultsgatan 14B 63347 Eskilstuna",
                        IsBusinessCustomer = false
                    },
                    new Customer
                    {
                        FirstName = "Sofia",
                        LastName = "Gustafsson",
                        Email = "sofia.gustafsson@example.com",
                        UserName = "sofgus_657",
                        Password = "xyz789",
                        Address = "Storgatan 42 11459 Stockholm",
                        IsBusinessCustomer = true
                    },
                    new Customer
                    {
                        FirstName = "Viktor",
                        LastName = "Nilsson",
                        Email = "viktor@nilsson.com",
                        UserName = "viknil",
                        Password = "asd456",
                        Address = "Kungsportsavenyn 22 41136 Göteborg",
                        IsBusinessCustomer = true
                    },
                    new Customer
                    {
                        FirstName = "Elin",
                        LastName = "Karlsson",
                        Email = "elin.karlsson@example.se",
                        UserName = "elikar345",
                        Password = "def678",
                        Address = "Föreningsgatan 10 21152 Malmö",
                        IsBusinessCustomer = false
                    });
                dbContext.SaveChanges();
            }
        }
        public static void AddAdmins()
        {
            using (var dbContext = new BookingsContext())
            {
                dbContext.Administrators.AddRange(
                    new Administrator
                    {
                        FirstName = "Charles",
                        LastName = "Xavier",
                        Email = "charles.xavier@xmen.com",
                        UserName = "charx",
                        Password = "wolverine1"
                    },
                    new Administrator
                    {
                        FirstName = "Logan",
                        LastName = "Howlett",
                        Email = "wolverine@xmen.com",
                        UserName = "wolverine",
                        Password = "jeanisbest"
                    }
                    );
                dbContext.SaveChanges();
            }
        }
        public static void AddFacilitySchedules()
        {
            using (var dbContext = new BookingsContext())
            {
                var facilities = dbContext.Facilities.ToList();

                for (int weekNumber = 1; weekNumber <= 52; weekNumber++)
                {
                    foreach (var facility in facilities)
                    {
                        string[] daysOfWeek = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

                        foreach (var dayOfWeek in daysOfWeek)
                        {
                            var facilitySchedule = new FacilitySchedule
                            {
                                WeekId = weekNumber,
                                FacilityId = facility.Id,
                                DayOfWeek = dayOfWeek,
                                AvailabilityStatus = "Available"
                            };

                            dbContext.FacilitySchedules.Add(facilitySchedule);
                        }
                    }
                }

                dbContext.SaveChanges();
            }
        }
    }
}
