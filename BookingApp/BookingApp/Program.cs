namespace BookingApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Helpers.AddData.AddAllTestData();//Calls all methods adding bulk data if the database is not yet populated
            await Helpers.Menu.StartMenu();
        }
    }
}
