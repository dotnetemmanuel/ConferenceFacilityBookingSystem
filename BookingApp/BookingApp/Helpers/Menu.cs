namespace BookingApp.Helpers
{
    internal class Menu
    {
        public static void StartMenu()
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine("Welcome to the Duchene Congress Center booking portal\n");
                Console.WriteLine("Please make a choice in the menu below");
                Console.WriteLine("[1]. Sign in as admin");
                Console.WriteLine("[2]. Sign in as customer");
                Console.WriteLine("[3]. Create a customer account");
                Console.WriteLine("[4]. Quit");

                int menuChoice;
                bool success = int.TryParse(Console.ReadLine(), out menuChoice);

                switch (menuChoice)
                {
                    case 1:
                        Helpers.LogIn.AdminLogin();
                        break;
                    case 2:
                        Helpers.LogIn.CustomerLogin();
                        break;
                    case 3:
                        Helpers.AddData.AddCustomer();
                        break;
                    case 4:
                        running = false;
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
                Console.WriteLine("Admin Dashboard\n");
                Console.WriteLine("Please make a choice in the menu below");
                Console.WriteLine("[1]. Add administrator");
                Console.WriteLine("[2]. Add customer");
                Console.WriteLine("[3]. Add facility");
                Console.WriteLine("[4]. View facilities");
                Console.WriteLine("[5]. View facility schedules");
                Console.WriteLine("[6]. View bookings");
                Console.WriteLine("[7]. View statistics");
                Console.WriteLine("[8]. LogOut");

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
                        Helpers.Information.ViewFacilities();
                        break;
                    case 5:
                        Console.WriteLine("Please enter the number of the week you would like to see a schedule for (1-52):");
                        success = int.TryParse(Console.ReadLine(), out int weekNr);
                        Helpers.Information.ViewWeeklySchedule(weekNr);
                        Console.WriteLine();
                        Console.WriteLine("Press any key to go back");
                        Console.ReadKey();
                        break;
                    case 6:
                        //Helpers.Info.ViewBookings();
                        break;
                    case 7:
                        //Helpers.Info.ViewStatistics();
                        break;
                    case 8:
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
                Console.WriteLine("[1]. View facilities");
                Console.WriteLine("[2]. Book a facility");
                Console.WriteLine("[3]. Sign out");

                int menuChoice;
                bool success = int.TryParse(Console.ReadLine(), out menuChoice);

                switch (menuChoice)
                {
                    case 1:
                        Helpers.Information.ViewFacilities();
                        break;
                    case 2:
                        Console.WriteLine("Please enter the number of the week you would like to see a schedule for (1-52):");
                        success = int.TryParse(Console.ReadLine(), out int weekNr);
                        Helpers.Information.ViewWeeklySchedule(weekNr);
                        Helpers.AddData.AddBooking();
                        break;
                    case 3:
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
