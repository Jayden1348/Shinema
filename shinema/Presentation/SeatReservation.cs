public static class SeatReservation
{
    public static void StartReservation(AccountModel user, ShowingModel show)
    {
        ReservationLogic reservationLogic = new();
        bool done_reserving = false;

        while (!done_reserving)
        {
            Console.Clear();
            List<List<SeatModel>> moviehall = HallLogic.ShowHall(show, reservationLogic);

            Console.WriteLine("\nEnter the position of the seat you want to reserve (use A1, B2, C3 format)");
            string positionstring = Console.ReadLine();
            if (positionstring.ToLower() == "q") { return; }

            List<string> seats = (positionstring.ToUpper()).Split(", ").ToList();
            int id = reservationLogic.GetNextId();
            bool TestSeats = reservationLogic.ValidateAndReserveSeats(seats, moviehall);
            done_reserving = reservationLogic.AddNewReservation(id, show.ID, user.Id, seats, TestSeats);
            if (done_reserving)
            {
                Console.Write($"You successfully reserved seats: {seats[0]}");
                foreach (string position in seats.GetRange(1, seats.Count - 1))
                {
                    Console.Write($", {position}");
                }
                Console.WriteLine("!");
                Console.WriteLine("\nWould you like to reserve more seats? (y/n)");
                done_reserving = !GlobalLogic.YN_loop(Console.ReadLine());
            }

        }
    }
    public static void ShowGrid(int columns, List<List<SeatModel>> seatlist)
    {
        string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        int row_letter = 0;

        Console.Write("\n     ");
        for (int j = 1; j <= columns; j++)
        {
            Console.Write($"{j} ");
            if (j < 10) { Console.Write(' '); }
        }

        Console.Write("\n   ---");
        for (int q = 0; q < columns; q++)
        {
            Console.Write("---");
        }
        Console.WriteLine("");

        foreach (List<SeatModel> row in seatlist)
        {
            row_letter++;
            Console.Write($"{letters[row_letter - 1]} | ");

            foreach (SeatModel seat in row)
            {
                Console.ResetColor();
                if (seat == null)
                {
                    Console.Write("   ");
                }
                else if (!seat.Available) { Console.Write(" X "); }
                else
                {
                    switch (seat.Rank)
                    {
                        case 1: Console.ForegroundColor = ConsoleColor.Blue; Console.Write(" ■ "); break;
                        case 2: Console.ForegroundColor = ConsoleColor.Yellow; Console.Write(" ■ "); break;
                        case 3: Console.ForegroundColor = ConsoleColor.Red; Console.Write(" ■ "); break;
                        default: Console.ForegroundColor = ConsoleColor.White; Console.Write(" ■ "); break;
                    }
                }
                Console.ResetColor();

            }
            Console.WriteLine("");
        }
    }

    public static void WrongInput(int errorcode, string invalid_position)
    {
        switch (errorcode)
        {
            case 1: Console.WriteLine($"\nReservation Failed! Position {invalid_position} is invalid: You used too little or too many characters. Try again:"); break;
            case 2: Console.WriteLine($"\nReservation Failed! Position {invalid_position} is invalid: Use A1 B2 format. Try again:"); break;
            case 3: Console.WriteLine($"\nReservation Failed! Seat {invalid_position} has already been booked!"); break;
            case 4: Console.WriteLine($"\nReservation Failed! Seat {invalid_position} doesn't exist!"); break;
            default: Console.WriteLine($"\nReservation Failed! {invalid_position} is invalid!"); break;
        }
        Thread.Sleep(3000);
    }

}