public static class MyReservations<T> where T : IReservation
{
    public static int PrintReservation(List<T> all_reservations)
    {
        if (all_reservations == null || all_reservations.Count == 0)
        {
            Console.Clear();
            Console.WriteLine("You currently don't have any reservations. Go make some!");
            Thread.Sleep(3000);
            return 0;
        }
        else
        {
            Console.Clear();
            foreach (T r in all_reservations)
            {
                Console.WriteLine(r.AllDetails());
            }
            Console.WriteLine("Press C to cancel a reservation");
            Console.WriteLine("Press any other key to return to menu");
            ConsoleKeyInfo k = Console.ReadKey();
            if (k.Key == ConsoleKey.C)
            {
                int delete_reservation = Convert.ToInt32(NavigationMenu.DisplayMenu(all_reservations, "Select a reservation to cancel:"));
                if (delete_reservation is 0)
                {
                    Console.Clear();
                    Console.WriteLine("Cancellation aborted...");
                    Thread.Sleep(2000);
                    return 0;
                }
                Console.Clear();
                Console.WriteLine("Succesfully cancelled reservation!");
                Thread.Sleep(2000);
                return delete_reservation;
            }
            return 0;
        }
    }
}
