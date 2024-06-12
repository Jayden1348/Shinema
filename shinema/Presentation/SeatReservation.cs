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
        int choice_amount_drinks = default;

        Dictionary<int, int> chosen_food_dict = new();
        Dictionary<int, int> chosen_drink_dict = new();

        FoodModel chosenModel = default;
        DrinkModel chosenDrink = default;
        
        // get all drinks where amount is higher than 0
        List<FoodModel> food = FoodLogic.GetAvailableFood();
        List<DrinkModel> drinks = DrinkLogic.GetAvailableDrinks();

        while (!done_reserving)
        {
            list_position = NavigationMenu.DisplayGrid(hall, allseats, total_price_reservation, list_position);

            if (list_position == null) { return false; }
            else if (list_position.Count == 0)
            {

                string confirm = NavigationMenu.DisplayMenu(new List<string> { "Yes", "No" }, "Do you want to order snacks?");

                switch (confirm)
                {
                    case "1":

                        string continue_ordering;
                        string food_choice;
                        string amount_input;

                        do
                        {
                            List<string> food_list = new();

                            foreach (FoodModel f in food)
                            {
                                food_list.Add($"{f.Title} | \u20AC{f.Price.ToString("F2")}");
                            }

                            food_choice = NavigationMenu.DisplayMenu(food_list, "Pick items.");

                            do
                            {
                                Console.Clear();
                                Console.WriteLine($"Enter a quantity of {food[Convert.ToInt32(food_choice) - 1].Title}:");
                                amount_input = Console.ReadLine();
                                if (int.TryParse(amount_input, out choice_amount))
                                {

                                    if (!FoodLogic.CheckStock(food[Convert.ToInt32(food_choice) - 1], choice_amount))
                                    {
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
                            if (!chosen_food_dict.ContainsKey(chosenModel.ID))
                            {
                                // add new foodmodel with amount as value to dict if foodmodel id is not in dictionary
                                chosen_food_dict[chosenModel.ID] = choice_amount;
                            }
                            else
                            {
                                // if it already exists then add chosen amount to key value
                                chosen_food_dict[chosenModel.ID] += choice_amount;
                            }

                            food[Convert.ToInt32(food_choice) - 1].Amount -= choice_amount;

                            continue_ordering = NavigationMenu.DisplayMenu(new() { "Yes", "No" }, "Do you want to order more snacks?");
                        } while (continue_ordering == "1");



                        break;
                    case "2":
                        break;
                }

                string confirm_drinks = NavigationMenu.DisplayMenu(new List<string> { "Yes", "No" }, "Do you want to order drinks?");

                switch(confirm_drinks)
                {
                    case "1":

                        string continue_ordering;
                        string drink_choice;
                        string amount_input;

                        do
                        {
                            List<string> drink_list = new();

                            foreach(DrinkModel d in drinks)
                            {
                                drink_list.Add($"{d.Size} {d.Title} | \u20AC{d.Price.ToString("F2")}");
                            }

                            drink_choice = NavigationMenu.DisplayMenu(drink_list, "Pick items.");

                            do
                            {
                                Console.Clear();
                                Console.WriteLine($"Enter a quantity of {drinks[Convert.ToInt32(drink_choice) - 1].Title}:");
                                amount_input = Console.ReadLine();
                                if (int.TryParse(amount_input, out choice_amount_drinks))
                                {

                                    if (!DrinkLogic.CheckStock(drinks[Convert.ToInt32(drink_choice) - 1], choice_amount_drinks))
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Not enough in stock\nPress enter to continue...");
                                        Console.ReadLine();
                                        choice_amount_drinks = 0;
                                    }
                                    
                                }
                            } while (choice_amount_drinks <= 0);

                            choice_amount_drinks = Convert.ToInt32(amount_input);
                            chosenDrink = drinks[Convert.ToInt32(drink_choice) - 1];
                            total_price_reservation += chosenDrink.Price * choice_amount_drinks;


                            // check if the dict already contains the chosen foodmodel
                            if (!chosen_drink_dict.ContainsKey(chosenDrink.ID))
                            {
                                // add new foodmodel with amount as value to dict if foodmodel id is not in dictionary
                                chosen_drink_dict[chosenDrink.ID] = choice_amount_drinks;
                            }
                            else
                            {
                                // if it already exists then add chosen amount to key value
                                chosen_drink_dict[chosenDrink.ID] += choice_amount_drinks;
                            }

                            drinks[Convert.ToInt32(drink_choice) - 1].Amount -= choice_amount_drinks;

                            continue_ordering = NavigationMenu.DisplayMenu(new() { "Yes", "No" }, "Do you want to order more drinks?");
                        } while (continue_ordering == "1");

                    break;
                    case "2":

                    break;

                }

                string confirm_text = ReservationLogic.GetPaymentOverview(chosenSeats, chosen_food_dict, chosen_drink_dict, total_price_reservation);

                // Comfirm seats  of winkelwagen moet hier komen, iets om aankoop te bevestigen. Dit is een basic voorbeeld
                string comfirm = NavigationMenu.DisplayMenu(new List<string> { "Yes", "No" }, confirm_text);
                if (comfirm == "1")
                {
                    int id = reservationLogic.GetNextId();
                    string unique_code = reservationLogic.GenerateRandomString();

                    if(chosen_food_dict.Count > 0) {
                         // Buy selected food item and amount
                        foreach (var kvp in chosen_food_dict)
                        {
                            FoodLogic.BuyFood(ReservationLogic.GetConsumableFromList(food, kvp.Key));
                        }
                    } else {
                        chosen_food_dict = null;
                    }

                    if (chosen_drink_dict.Count > 0)
                    {
                        // Buy selected drink item and amount
                        foreach (var kvp in chosen_drink_dict)
                        {
                            DrinkLogic.BuyDrink(ReservationLogic.GetConsumableFromList(drinks, kvp.Key));
                        }
                    } else {
                        chosen_drink_dict = null;
                    }

                    reservationLogic.AddNewReservation(id, show.ID, user.Id, allseats, total_price_reservation, unique_code, chosen_food_dict, chosen_drink_dict);

                    // Bar Reservation
                    BarReservation.ReserveBarSeatsInteraction(show.Datetime.AddMinutes(MoviesLogic.GetById(show.MovieID).Length), unique_code, user.Id, allseats.Count);

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