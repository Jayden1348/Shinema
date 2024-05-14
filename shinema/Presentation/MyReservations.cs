public static class MyReservations
{
    public static void PrintReservation(ReservationModel r)
    {
        if (r == null)
        {
            Console.Clear();
            Console.WriteLine("You currently don't have any reservations. Go make some!");
            Thread.Sleep(2000);
        }
        else
        {
            // Console.WriteLine($"Reservation for {r.}.\nDate: {r.}");
        }
    }
}