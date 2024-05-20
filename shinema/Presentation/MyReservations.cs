public static class MyReservations
{
    public static void PrintReservation(List<ReservationModel> all_reservations)
    {
        if (all_reservations == null)
        {
            Console.Clear();
            Console.WriteLine("You currently don't have any reservations. Go make some!");
            Thread.Sleep(2000);
        }
        else
        {
            Console.Clear();
            foreach (ReservationModel r in all_reservations)
            {
                ShowingsLogic s = new ShowingsLogic();
                Console.Write($"Reservation for {MoviesLogic.GetById(s.GetById(r.Showing_ID).MovieID).Title} in hall {s.GetById(r.Showing_ID).RoomID}.\nDate: {s.GetById(r.Showing_ID).Datetime}\nSeats: {r.Seats[0]}");
                foreach (string seat in r.Seats[1..])
                {
                    Console.Write($", {seat}");
                }
                Console.WriteLine($"\nTotal price: {r.Price}");
                int num_bar_res = BarReservationLogic.FindByUniqueCode(r.Unique_code);
                if (num_bar_res != 0) { Console.WriteLine($"Reserved bar seats: {num_bar_res}"); }
                Console.WriteLine($"Reservation code: {r.Unique_code}\n");
            }
        }
        Console.WriteLine("Press C to cancel a reservation");
        Console.WriteLine("Press any other key to return to menu");
        if (Console.ReadKey().Key == ConsoleKey.C)
        {
            int delete_reservation = Convert.ToInt32(NavigationMenu.DisplayMenu(all_reservations, "Select a reservation to cancel:")) - 1;

            ReservationLogic.DeleteReservation(all_reservations[delete_reservation]);
            Console.Clear();
            Console.WriteLine("Succesfully cancelled reservation!");
            Thread.Sleep(2000);

        }

    }


}