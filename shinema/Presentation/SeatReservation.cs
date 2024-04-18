public static class SeatReservation
{
    public static bool StartReservation(AccountModel user, ShowingModel show)
    {
        ReservationLogic reservationLogic = new();
        bool done_reserving = false;
        List<string> allseats = new() { };
        List<List<SeatModel>> hall = ReservationLogic.GetEmptyHall(show.RoomID);

        hall = reservationLogic.AddReservationsToHall(hall, show);
        string yesno;
        double total_price_reservation = 0;

        while (!done_reserving)
        {
            List<string> list_position = NavigationMenu.DisplayGrid(hall, allseats);

            if (list_position == null) { return false; }
            hall[Convert.ToInt32(list_position[1])][Convert.ToInt32(list_position[2])].Available = false;
            total_price_reservation += hall[Convert.ToInt32(list_position[1])][Convert.ToInt32(list_position[2])].GetPrice();
            string chosen_position = list_position[0];

            allseats.Add(chosen_position);
            allseats.Sort();
            Console.WriteLine($"You successfully reserved seat {chosen_position}");
            Thread.Sleep(1000);

            Console.Write($"\nYour seat(s): {allseats[0]}");
            foreach (string position in allseats.GetRange(1, allseats.Count - 1))
            {
                Console.Write($", {position}");
            }
            Console.WriteLine($"\nTotal price: €{total_price_reservation}");


            // Console.Clear();
            // Console.WriteLine(reservationLogic.ReservationOverview(allseats, hall));
                
            // if (reservationLogic.ShoppingCart() == false) {
            //     done_reserving = true;
            //     continue;
            // }

            Thread.Sleep(2000);
            if (ReservationLogic.IsSoldOut(hall))
            {
                Console.Clear();
                Console.WriteLine("The hall is now sold out!");
                Thread.Sleep(2000);
                yesno = "2";
            }
            else
            {
                yesno = NavigationMenu.DisplayMenu(new List<string>() { "Yes", "No" }, "Would you like to reserve more seats?");

            }
            if (yesno == "2")
            {
                int id = reservationLogic.GetNextId();
                string unique_code = reservationLogic.GenerateRandomString();
                reservationLogic.AddNewReservation(id, show.ID, user.Id, allseats, unique_code);
                return true;
            }
            else
            {
                yesno = NavigationMenu.DisplayMenu(new List<string>() { "Yes", "No" }, "Would you like to reserve more seats?");

            }
            if (yesno == "2")
            {
                int id = reservationLogic.GetNextId();
                string unique_code = reservationLogic.GenerateRandomString();
                reservationLogic.AddNewReservation(id, show.ID, user.Id, allseats, unique_code);
                return true;
            }



        }
        return true;
    }
    public static void ShowGrid(List<List<SeatModel>> seatlist, int pointer_row, int pointer_col, List<string> reserved_seats)
    {
        string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        int row_num = 0;
        int col_num = 0;
        string symbol;
        int columns = seatlist[0].Count();

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
            row_num++;
            Console.Write($"{letters[row_num - 1]} | ");
            col_num = 0;
            foreach (SeatModel seat in row)
            {
                col_num++;
                Console.ResetColor();
                if (seat == null)
                {
                    Console.Write("   ");
                }
                else
                {
                    if (!seat.Available) { symbol = "X"; }
                    else { symbol = "■"; }

                    if (row_num == pointer_row && col_num == pointer_col)
                    {
                        Console.ForegroundColor = ConsoleColor.Green; Console.Write($">");
                        switch (seat.Rank)
                        {
                            case 1: Console.ForegroundColor = ConsoleColor.Red; break;
                            case 2: Console.ForegroundColor = ConsoleColor.Yellow; break;
                            case 3: Console.ForegroundColor = ConsoleColor.Blue; break;
                            default: Console.ForegroundColor = ConsoleColor.White; break;
                        }
                        if (symbol == "X")
                        {
                            if (reserved_seats.Contains(seat.Position)) { Console.ForegroundColor = ConsoleColor.Green; }
                            else { Console.ForegroundColor = ConsoleColor.White; }
                        }
                        Console.Write($"{symbol}");
                        Console.ForegroundColor = ConsoleColor.Green; Console.Write($"<");
                    }
                    else
                    {
                        switch (seat.Rank)
                        {
                            case 1: Console.ForegroundColor = ConsoleColor.Red; break;
                            case 2: Console.ForegroundColor = ConsoleColor.Yellow; break;
                            case 3: Console.ForegroundColor = ConsoleColor.Blue; break;
                            default: Console.ForegroundColor = ConsoleColor.White; break;
                        }
                        if (symbol == "X")
                        {
                            if (reserved_seats.Contains(seat.Position)) { Console.ForegroundColor = ConsoleColor.Green; }
                            else { Console.ForegroundColor = ConsoleColor.White; }
                        }
                        Console.Write($" {symbol} ");
                    }
                    Console.ResetColor();

                }
            }
            Console.WriteLine("");

        }
    }

}