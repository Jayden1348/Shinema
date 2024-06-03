public static class SeatReservation
{
    public static bool StartReservation(AccountModel user, ShowingModel show)
    {
        ReservationLogic reservationLogic = new();
        bool done_reserving = false;
        List<string> allseats = new() { };
        List<List<SeatModel>> hall = ReservationLogic.GetEmptyHall(show.RoomID);
        List<SeatModel> chosenSeats = new();
        hall = reservationLogic.AddReservationsToHall(hall, show);
        double total_price_reservation = 0;
        List<string> list_position = new() { };

        int choice_amount = default;
        Dictionary<int, int> chosen_food_dict = new();
        FoodModel chosenModel = default;
        List<FoodModel> food = FoodAccess.LoadAll();
    
        while (!done_reserving)
        {
            list_position = NavigationMenu.DisplayGrid(hall, allseats, total_price_reservation, list_position);

            if (list_position == null) { return false; }
            else if (list_position.Count == 0)
            {

                string confirm = NavigationMenu.DisplayMenu(new List<string> {"Yes", "No"}, "Do you want to order snacks?");

                switch (confirm) {
                    case "1":
                    
                    string continue_ordering;
                    string food_choice;
                    string amount_input;

                    do {
                        List <string> food_list = new();

                        food.Where(f => f.Amount > 0)
                            .ToList()
                            .ForEach(f => food_list.Add($"{f.Title} | \u20AC{f.Price.ToString("F2")}"));


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

                        // check if the dict already contains the chosen foodmodel
                        if(!chosen_food_dict.ContainsKey(chosenModel.ID)){
                            // add new foodmodel with amount as value to dict if foodmodel id is not in dictionary
                            chosen_food_dict[chosenModel.ID] = choice_amount;
                        } else {
                            // if it already exists then add chosen amount to key value
                            chosen_food_dict[chosenModel.ID] += choice_amount;
                        }

                        continue_ordering = NavigationMenu.DisplayMenu(new() {"Yes", "No"}, "Do you want to order more snacks?");
                    } while (continue_ordering == "1");


                    break;
                    case "2":
                    break;
                }

                string confirm_text = $"Payment overview:\n";

                List<SeatModel> rank1seats = chosenSeats.Where(s => s.Rank == 1).ToList();
                List<SeatModel> rank2seats = chosenSeats.Where(s => s.Rank == 2).ToList();
                List<SeatModel> rank3seats = chosenSeats.Where(s => s.Rank == 3).ToList();

                if(rank1seats.Count() > 0) 
                {
                    confirm_text += $"\nRank 1 seats: {rank1seats.Count()} x \u20AC{rank1seats[0].GetPrice()}\n";
                }
                if(rank2seats.Count() > 0) 
                {
                    confirm_text += $"\nRank 2 seats: {rank2seats.Count()} x \u20AC{rank2seats[0].GetPrice()}\n";
                }
                if(rank3seats.Count() > 0) 
                {
                    confirm_text += $"\nRank 3 seats: {rank3seats.Count()} x \u20AC{rank3seats[0].GetPrice()}\n";
                }
                

                if (choice_amount > 0 ) {
                    foreach(var kvp in chosen_food_dict)
                    {
                        FoodModel item = food.Where(f => f.ID == kvp.Key).ToList().First();

                        confirm_text += $"\n{item.Title}: {kvp.Value} x \u20AC{item.Price.ToString("F2")}\n";
                        
                    }
                }

                confirm_text += $"\n\nTotal: \u20AC{total_price_reservation.ToString("F2")}\n";

                confirm_text += "\nContinue payment?\n";

                // Comfirm seats  of winkelwagen moet hier komen, iets om aankoop te bevestigen. Dit is een basic voorbeeld
                string comfirm = NavigationMenu.DisplayMenu(new List<string> { "Yes", "No" }, confirm_text);
                if (comfirm == "1")
                {
                    int id = reservationLogic.GetNextId();
                    string unique_code = reservationLogic.GenerateRandomString();

                    if (chosenModel != null) {
                        reservationLogic.AddNewReservation(id, show.ID, user.Id, allseats, total_price_reservation, unique_code, chosen_food_dict);

                        // Buy selected food item and amount
                        foreach(var kvp in chosen_food_dict)
                        {
                            FoodModel item = food.Where(f => f.ID == kvp.Key).ToList().First();

                            FoodLogic.BuyFood(item, kvp.Value);
                        }

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
                chosenSeats.Remove(hall[Convert.ToInt32(list_position[1])][Convert.ToInt32(list_position[2])]);
                allseats.Remove(list_position[0]);
            }
            else
            {
                hall[Convert.ToInt32(list_position[1])][Convert.ToInt32(list_position[2])].Available = false;
                total_price_reservation += hall[Convert.ToInt32(list_position[1])][Convert.ToInt32(list_position[2])].GetPrice();
                chosenSeats.Add(hall[Convert.ToInt32(list_position[1])][Convert.ToInt32(list_position[2])]);
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