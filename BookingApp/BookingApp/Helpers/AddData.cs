using BookingApp.Models;

namespace BookingApp.Helpers
{
    internal class AddData
    {
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

            Console.WriteLine("Please enter your pasword");
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
    }
}
