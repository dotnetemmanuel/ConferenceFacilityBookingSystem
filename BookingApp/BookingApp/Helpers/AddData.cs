using BookingApp.Models;

namespace BookingApp.Helpers
{
    internal class AddData
    {
        public static void AddWeeks()
        {
            using (var dbContext = new BookingsContext())
            {
                for (int i = 1; i <= 52; i++)
                {
                    Week newWeek = new Week
                    {
                        Monday = "Available",
                        Tuesday = "Available",
                        Wednesday = "Available",
                        Thursday = "Available",
                        Friday = "Available",
                        Saturday = "Available",
                        Sunday = "Available"
                    };

                    dbContext.Weeks.Add(newWeek);
                }
                dbContext.SaveChanges();
            }
        }
    }
}
