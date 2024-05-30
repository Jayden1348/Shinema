public static class SeatReservation
{
    public static bool StartReservation(AccountModel user, ShowingModel show)
    {
        ReservationLogic reservationLogic = new();
        bool done_reserving = false;
        List<string> allseats = new() { };
        List<List<SeatModel>> hall = ReservationLogic.GetEmptyHall(show.RoomID);

        hall = reservationLogic.AddReservationsToHall(hall, show);
        double total_price_reservation = 0;
        List<string> list_position = new() { };

        int choice_amount = default;
        FoodModel chosenModel = default;
    
        while (!done_reserving)
        {
            list_position = NavigationMenu.DisplayGrid(hall, allseats, total_price_reservation, list_position);

            if (list_position == null) { return false; }
            else if (list_position.Count == 0)
            {

                string confirm = NavigationMenu.DisplayMenu(new List<string> {"Yes", "No"}, "Do you want to order snacks?");

                switch (confirm) {
                    case "1":
                    
                    bool confirm_food_order = false;
                    string food_choice;
                    string amount_input;

                    do {
                        List<FoodModel> food = FoodAccess.LoadAll();
                        List <string> food_list = new();

                        food.Where(f => f.Amount > 0)
                            .ToList()
                            .ForEach(f => food_list.Add($"{f.Title} | €{f.Price.ToString("F2")}"));


                        food_choice = NavigationMenu.DisplayMenu(food_list, "Pick items.");

                        do {
                            Console.Clear();
                            Console.WriteLine($"Enter a quantity of {food[Convert.ToInt32(food_choice) - 1].Title}:");
                            amount_input = Console.ReadLine();

                            if (int.TryParse(amount_input, out choice_amount)) {

                                if (choice_amount > food[Convert.ToInt32(food_choice) - 1].Amount) {
                                    Console.Clear();
                                    Console.WriteLine("Not enough in stock\nPress enter to continue...");
                                    Console.ReadLine();
                                    choice_amount = 0;
                                }
                            }
                        } while (choice_amount <= 0);

                        choice_amount = Convert.ToInt32(amount_input);
                        chosenModel = food[Convert.ToInt32(food_choice) - 1];
                        total_price_reservation += chosenModel.Price * choice_amount;

                        confirm_food_order = true;
                    } while (!confirm_food_order);


                    break;
                    case "2":
                    break;
                }

                // Comfirm seats  of winkelwagen moet hier komen, iets om aankoop te bevestigen. Dit is een basic voorbeeld
                string comfirm = NavigationMenu.DisplayMenu(new List<string> { "Yes", "No" }, $"Pay \u20AC{total_price_reservation} for {allseats.Count()} seats?");
                if (comfirm == "1")
                {
                    int id = reservationLogic.GetNextId();
                    string unique_code = reservationLogic.GenerateRandomString();

                    Dictionary<int, int> foodDict = new();

                    if (chosenModel != null) {
                        foodDict[chosenModel.ID] = choice_amount;
                        reservationLogic.AddNewReservation(id, show.ID, user.Id, allseats, total_price_reservation, unique_code, foodDict);

                        // Buy selected food item and amount
                        FoodLogic.BuyFood(chosenModel, choice_amount);
                    } else {
                        reservationLogic.AddNewReservation(id, show.ID, user.Id, allseats, total_price_reservation, unique_code, null);

                        
                    }

                    // Bar Reservation
                    BarReservation.ReserveBarSeatsInteraction(show.Datetime, unique_code, user.Id , allseats.Count);

                    Console.Clear();
                    Console.WriteLine("Succesfull reservation!");
                    Thread.Sleep(1000);
                    Console.WriteLine($"Your reservation code: {unique_code}");
                    Thread.Sleep(2000);
                }
                else
                {
                    Console.WriteLine("Cancelling reservation...");
                    Thread.Sleep(2000);
                }
                // Alles hierboven kan verbeterd worden met een winkelwagen of bevesiging ofzo
                return true;
            }
            else if (list_position.Count == 4)
            {
                hall[Convert.ToInt32(list_position[1])][Convert.ToInt32(list_position[2])].Available = true;
                total_price_reservation -= hall[Convert.ToInt32(list_position[1])][Convert.ToInt32(list_position[2])].GetPrice();
                allseats.Remove(list_position[0]);
            }
            else
            {
                hall[Convert.ToInt32(list_position[1])][Convert.ToInt32(list_position[2])].Available = false;
                total_price_reservation += hall[Convert.ToInt32(list_position[1])][Convert.ToInt32(list_position[2])].GetPrice();
                allseats.Add(list_position[0]);
                allseats.Sort();
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