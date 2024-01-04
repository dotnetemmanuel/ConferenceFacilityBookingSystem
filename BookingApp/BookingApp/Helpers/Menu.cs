namespace BookingApp.Helpers
{
    internal class Menu
    {
        public static void StartMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Welcome to the Duchene Congress Center booking portal\n");
                Console.WriteLine("Please make a choice in the menu below");
                Console.WriteLine("[1]. Sign in as admin");
                Console.WriteLine("[2]. Sign in as customer");
                Console.WriteLine("[3]. Create a customer account");

                int menuChoice;
                bool success = int.TryParse(Console.ReadLine(), out menuChoice);

                switch (menuChoice)
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        Helpers.AddData.AddCustomer();
                        break;
                    default:
                        Console.WriteLine("Invalid input, try again!");
                        Console.ReadKey();
                        break;
                }
            }
        }

        public static void AdminMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Admin Dashboardl\n");
                Console.WriteLine("Please make a choice in the menu below");
                Console.WriteLine("[1]. Add administrator");
                Console.WriteLine("[2]. Add customer");
                Console.WriteLine("[3]. Add facility");
                Console.WriteLine("[4]. View bookings");
                Console.WriteLine("[5]. View statistics");
                Console.WriteLine("[6]. LogOut");

                int menuChoice;
                bool success = int.TryParse(Console.ReadLine(), out menuChoice);

                switch (menuChoice)
                {
                    case 1:
                        Helpers.AddData.AddAdmin();
                        break;
                    case 2:
                        Helpers.AddData.AddCustomer();
                        break;
                    case 3:
                        Helpers.AddData.AddFacility();
                        break;
                    case 4:
                        //Helpers.Info.ViewBookings();
                        break;
                    case 5:
                        //Helpers.Info.ViewStatistics();
                        break;
                    case 6:
                        Helpers.LogIn.LogOut();
                        break;
                    default:
                        Console.WriteLine("Invalid input, try again!");
                        Console.ReadKey();
                        break;
                }
            }

        }

        public static void CustomerMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Customer Dashboard\n");
                Console.WriteLine("Please make a choice in the menu below");
                Console.WriteLine("[1]. Book a facility");
                Console.WriteLine("[2]. Sign out");

                int menuChoice;
                bool success = int.TryParse(Console.ReadLine(), out menuChoice);

                switch (menuChoice)
                {
                    case 1:
                        Helpers.AddData.AddBooking();
                        break;
                    case 2:
                        Helpers.LogIn.LogOut();
                        break;
                    default:
                        Console.WriteLine("Invalid input, try again!");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
