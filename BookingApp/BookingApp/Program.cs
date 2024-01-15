namespace BookingApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Helpers.AddData.AddAllTestData();//Calls all methods adding bulk data if the database is not yet populated
            Helpers.Information.ViewStatistics();
            //Helpers.Menu.StartMenu();
        }
    }
}
