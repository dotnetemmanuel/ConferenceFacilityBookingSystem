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
                }
            }
        }
    }
}
