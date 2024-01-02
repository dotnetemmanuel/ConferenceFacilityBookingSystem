namespace BookingApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //using (var dbContext = new BookingsContext())
            //{
            //    var week = dbContext.Weeks.FirstOrDefault(w => w.Id == 1);
            //    Console.WriteLine($"Monday:   {week.Monday.PadLeft(10)}");
            //    Console.WriteLine($"Tuesday:  {week.Tuesday.PadLeft(10)}");
            //    Console.WriteLine($"Wednesday:{week.Wednesday.PadLeft(10)}");
            //    Console.WriteLine($"Thursday: {week.Thursday.PadLeft(10)}");
            //    Console.WriteLine($"Friday:   {week.Friday.PadLeft(10)}");
            //    Console.WriteLine($"Saturday: {week.Saturday.PadLeft(10)}");
            //    Console.WriteLine($"Sunday:   {week.Sunday.PadLeft(10)}");
            //    Console.WriteLine("Test");

            //}
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
