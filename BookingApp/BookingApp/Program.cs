namespace BookingApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Helpers.AddData.AddAllTestData();
            Helpers.Menu.StartMenu();
        }
    }
}
