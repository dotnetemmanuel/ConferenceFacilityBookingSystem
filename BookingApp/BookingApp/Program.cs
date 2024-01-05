namespace BookingApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region
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
            #endregion

            while (true)
            {
                Helpers.AddData.AddFacilitySchedules();
                Console.WriteLine("Added FS");
                Console.ReadKey();
                //Helpers.Menu.StartMenu();
            }
        }
    }
}
