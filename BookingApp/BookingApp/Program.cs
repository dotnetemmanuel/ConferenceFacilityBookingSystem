﻿namespace BookingApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Helpers.AddData.AddAllTestData();//Runs only if database is not populated with test data
            Helpers.AddData.TestAddBookings();
            Console.ReadKey();
            Helpers.Menu.StartMenu();
        }
    }
}
