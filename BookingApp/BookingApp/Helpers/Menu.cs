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
                Console.WriteLine();
                Console.WriteLine("╔════════════════════════╗");
                Console.WriteLine("║                        ║");
                Console.WriteLine("║     Welcome to the     ║");
                Console.WriteLine("║         Duchene        ║");
                Console.WriteLine("║     Congress Center    ║");
                Console.WriteLine("║                        ║");
                Console.WriteLine("╚════════════════════════╝");
                Console.WriteLine();
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
                Console.WriteLine($"Admin Dashboard: {Helpers.LogIn.adminFirstName} {Helpers.LogIn.adminLastName}");
                Console.WriteLine();
                Console.WriteLine("Please make a choice in the menu below");
                Console.WriteLine("[1]. Add an administrator");
                Console.WriteLine("[2]. Add a customer");
                Console.WriteLine("[3]. Add a facility");
                Console.WriteLine("[4]. Book a facility");
                Console.WriteLine("[5]. View facilities");
                Console.WriteLine("[6]. View facility schedules");
                Console.WriteLine("[7]. View bookings");
                Console.WriteLine("[8]. View statistics");
                Console.WriteLine("[9]. LogOut");

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
                        Console.WriteLine("Please enter the number of the week you would like to see a schedule for (1-52):");
                        success = int.TryParse(Console.ReadLine(), out int weekNr);
                        Helpers.Information.ViewWeeklySchedule(weekNr);
                        Helpers.AddData.AddBookingAdmin();
                        break;
                    case 5:
                        Helpers.Information.ViewFacilities();
                        break;
                    case 6:
                        Console.WriteLine("Please enter the number of the week you would like to see a schedule for (1-52):");
                        success = int.TryParse(Console.ReadLine(), out int weekNr1);
                        Helpers.Information.ViewWeeklySchedule(weekNr1);
                        Console.WriteLine();
                        Console.WriteLine("Press any key to go back");
                        Console.ReadKey();
                        break;
                    case 7:
                        Helpers.Information.ViewBookings();
                        break;
                    case 8:
                        //Helpers.Information.ViewStatistics();
                        break;
                    case 9:
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
                Console.WriteLine($"Customer Dashboard: {Helpers.LogIn.customerFirstName} {Helpers.LogIn.customerLastName}");
                Console.WriteLine();
                Console.WriteLine("Please make a choice in the menu below");
                Console.WriteLine("[1]. View facilities");
                Console.WriteLine("[2]. Book a facility");
                Console.WriteLine("[3]. View bookings");
                Console.WriteLine("[4]. Cancel booking");
                Console.WriteLine("[5]. Sign out");

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
                        Helpers.AddData.AddBookingCustomer();
                        break;
                    case 3:
                        Helpers.Information.ViewCustomerBookings();
                        break;
                    case 4:
                        Helpers.AddData.CancelBooking();
                        break;
                    case 5:
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
