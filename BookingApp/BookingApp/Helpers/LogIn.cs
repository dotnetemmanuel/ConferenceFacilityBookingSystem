using BookingApp.Models;

namespace BookingApp.Helpers
{
    internal class LogIn
    {
        public static string adminFirstName = "";
        public static string adminLastName = "";
        public static string customerFirstName = "";
        public static string customerLastName = "";

        public static void AdminLogin()
        {
            bool loggedIn = false;

            while (!loggedIn)
            {
                Console.Clear();
                Console.Write("Please enter your username: ");
                string username = Console.ReadLine();
                Console.Write("Please enter your password: ");
                string password = Console.ReadLine();

                using (var dbContext = new BookingsContext())
                {
                    var admin = dbContext.Administrators.FirstOrDefault(a => a.UserName == username && a.Password == password);

                    if (admin != null)
                    {
                        Console.WriteLine($"Login successful! Welcome {admin.FirstName}");
                        loggedIn = true;
                        adminFirstName = admin.FirstName;
                        adminLastName = admin.LastName;
                        Helpers.Menu.AdminMenu();
                    }
                    else
                    {
                        Console.WriteLine("Invalid username or password, Login failed");
                        Console.WriteLine("Press 'Y' to try again, or any other key to exit.");

                        // Check if the user wants to try logging in again
                        if (Console.ReadKey().Key != ConsoleKey.Y)
                        {
                            break; // Exit the loop if the user doesn't want to try again
                        }
                    }
                }
            }
        }


        public static void CustomerLogin()
        {
            bool loggedIn = false;

            while (!loggedIn)
            {
                Console.Clear();
                Console.Write("Please enter your username: ");
                string username = Console.ReadLine();
                Console.Write("Please enter your password: ");
                string password = Console.ReadLine();

                using (var dbContext = new BookingsContext())
                {
                    var customer = dbContext.Customers.FirstOrDefault(a => a.UserName == username && a.Password == password);

                    if (customer != null)
                    {
                        Console.WriteLine($"Login successful! Welcome {customer.FirstName}");
                        loggedIn = true;
                        customerFirstName = customer.FirstName;
                        customerLastName = customer.LastName;
                        Helpers.Menu.CustomerMenu();
                    }
                    else
                    {
                        Console.WriteLine("Invalid username or password, Login failed");
                        Console.WriteLine("Press 'Y' to try again, or any other key to exit.");

                        // Check if the user wants to try logging in again
                        if (Console.ReadKey().Key != ConsoleKey.Y)
                        {
                            break; // Exit the loop if the user doesn't want to try again
                        }
                    }

                }
            }
        }

        public static void LogOut()
        {
            Console.Clear();
            Helpers.Menu.StartMenu();
        }
    }
}
