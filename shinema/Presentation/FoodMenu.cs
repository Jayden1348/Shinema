public static class FoodMenu
{
    public static void AddFoodMenu()
    {

        List<string> addFoodOptions = new List<string>{
            "Title (required)",
            "Amount (required)",
            "Price (required)",
            "Add item",
            "Cancel"
        };

        string title = default;
        int amount = default;
        double price = default;

        bool item_added = false;

        while (item_added == false)
        {
            string choice = NavigationMenu.DisplayMenu(addFoodOptions, "Add food item:\n");

            switch (choice)
            {
                case "1":
                    do
                    {
                        title = default;
                        Console.Clear();
                        Console.WriteLine("Fill in a title:");

                        string user_input = Console.ReadLine();

                        if (!string.IsNullOrEmpty(user_input))
                        {
                            title = user_input;
                            addFoodOptions[0] = $"Change title | {title}";
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Cannot be empty\n");
                            Console.WriteLine("Press enter to try again...");
                            Console.ReadLine();
                        }
                    }
                    while (string.IsNullOrEmpty(title));

                    break;
                case "2":
                    do
                    {
                        amount = default;

                        Console.Clear();
                        Console.WriteLine("Fill in an amount: (only numbers)");

                        string amount_input = Console.ReadLine();

                        if (!string.IsNullOrEmpty(amount_input))
                        {
                            if (int.TryParse(amount_input, out int parsed_amount))
                            {
                                amount = parsed_amount;
                                addFoodOptions[1] = $"Change amount | {amount}";
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Invalid input, Enter a number\n");
                                Console.WriteLine("Press enter to try again...");
                                Console.ReadLine();

                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Cannot be empty\n");
                            Console.WriteLine("Press enter to try again...");
                            Console.ReadLine();
                        }
                    }
                    while (amount == default || amount < 0);

                    break;
                case "3":
                    do
                    {
                        price = default;

                        Console.Clear();
                        Console.WriteLine("Fill in a price: (only numbers)");

                        string price_input = Console.ReadLine();

                        if (!string.IsNullOrEmpty(price_input))
                        {
                            if (double.TryParse(price_input, out double parsed_price))
                            {
                                price = parsed_price;
                                addFoodOptions[2] = $"Change price | {price.ToString("F2")}";
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Invalid input, Enter a price\n");
                                Console.WriteLine("Press enter to try again...");
                                Console.ReadLine();

                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Cannot be empty\n");
                            Console.WriteLine("Press enter to try again...");
                            Console.ReadLine();
                        }
                    }
                    while (price == default || price < 0);
                    break;
                case "4":

                    Console.Clear();

                    bool addItem = FoodLogic.AddFood(title, amount, price);

                    if (addItem)
                    {
                        Console.WriteLine("Item added succesfully\n\nPress enter to continue...");
                        item_added = true;
                    }
                    else
                    {
                        Console.WriteLine("Not everything filled in...\n\nPress enter to try again...");
                        Console.ReadLine();
                    }

                    break;
                case "5":
                    item_added = true;
                    Console.Clear();
                    Console.WriteLine("Item adding cancelled...\n\nPress Enter to continue");
                    break;
            }
        }
    }

    public static void DeleteFoodMenu()
    {
        List<FoodModel> food_list = FoodLogic.GetAllFood();

        string food_choice = NavigationMenu.DisplayMenu(food_list, "Select food item you want to delete:");
        FoodModel food = food_list[Convert.ToInt32(food_choice) - 1];
        Console.Clear();
        string confirm_delete = NavigationMenu.DisplayMenu(new() { "Yes", "No" }, $"Are you sure you want to delete {food.Title}?");
        switch (confirm_delete)
        {
            case "1":
                ReservationLogic reservationLogic = new ReservationLogic();
                Console.Clear();
                FoodLogic.DeleteFood(food);
                reservationLogic.RemoveFoodFromReservations(food);
                Console.WriteLine("Item deleted successfully\n\nPress enter to continue...");
                break;
            case "2":
                Console.Clear();
                Console.WriteLine("Deletion cancelled");
                break;
        }
    }

    public static void DeleteDrinkMenu()
    {
        List<DrinkModel> drink_list = DrinkLogic.GetAllDrinks();

        string drink_choice = NavigationMenu.DisplayMenu(drink_list, "Select drink item you want to delete:");
        DrinkModel drink = drink_list[Convert.ToInt32(drink_choice) - 1];
        Console.Clear();
        string confirm_delete = NavigationMenu.DisplayMenu(new() { "Yes", "No" }, $"Are you sure you want to delete {drink.Title}?");
        switch (confirm_delete)
        {
            case "1":
                ReservationLogic reservationLogic = new ReservationLogic();
                Console.Clear();
                DrinkLogic.DeleteDrink(drink);
                reservationLogic.RemoveDrinkFromReservations(drink);
                break;
            case "2":
                Console.Clear();
                Console.WriteLine("Deletion cancelled");
                break;
        }
    }


    public static void AddDrinkMenu()
    {
        List<string> addDrinkOptions = new List<string>{
            "Title (required)",
            "Amount (required)",
            "Price (required)",
            "Size (required)",
            "Add item",
            "Cancel"
        };

        string title = default;
        int amount = default;
        double price = default;
        Sizes size = default;

        bool item_added = false;

        while (item_added == false)
        {
            string choice = NavigationMenu.DisplayMenu(addDrinkOptions, "Add drink item:\n");

            switch (choice)
            {
                case "1":
                    do
                    {
                        title = default;

                        Console.Clear();
                        Console.WriteLine("Fill in a title:");

                        string user_input = Console.ReadLine();

                        if (!string.IsNullOrEmpty(user_input))
                        {
                            title = user_input;
                            addDrinkOptions[0] = $"Change title | {title}";
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Cannot be empty\n");
                            Console.WriteLine("Press enter to try again...");
                            Console.ReadLine();
                        }
                    }
                    while (string.IsNullOrEmpty(title));
                    break;
                case "2":
                    do
                    {
                        amount = default;
                        Console.Clear();
                        Console.WriteLine("Fill in an amount: (only numbers)");

                        string amount_input = Console.ReadLine();

                        if (!string.IsNullOrEmpty(amount_input))
                        {
                            if (int.TryParse(amount_input, out int parsed_amount))
                            {
                                amount = parsed_amount;
                                addDrinkOptions[1] = $"Change amount | {amount}";
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Invalid input, Enter a number\n");
                                Console.WriteLine("Press enter to try again...");
                                Console.ReadLine();
                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Cannot be empty\n");
                            Console.WriteLine("Press enter to try again...");
                            Console.ReadLine();
                        }
                    }
                    while (amount == default || amount < 0);


                    break;
                case "3":
                    do
                    {
                        price = default;

                        Console.Clear();
                        Console.WriteLine("Fill in a price: (only numbers)");

                        string price_input = Console.ReadLine();

                        if (!string.IsNullOrEmpty(price_input))
                        {
                            if (double.TryParse(price_input, out double parsed_price))
                            {
                                price = parsed_price;
                                addDrinkOptions[2] = $"Change price | {price.ToString("F2")}";
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Invalid input, Enter a price\n");
                                Console.WriteLine("Press enter to try again...");
                                Console.ReadLine();

                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Cannot be empty\n");
                            Console.WriteLine("Press enter to try again...");
                            Console.ReadLine();
                        }
                    }
                    while (price == default || price < 0);

                    break;
                case "4":
                    // Handle size input

                    Console.Clear();

                    List<string> sizes = Enum.GetNames(typeof(Sizes)).ToList();

                    string sizeIndex = NavigationMenu.DisplayMenu(sizes, "Pick a size:\n");

                    if (sizeIndex != null)
                    {
                        size = (Sizes)int.Parse(sizeIndex) - 1;
                        addDrinkOptions[3] = $"Change size | {size}";
                    }

                    break;
                case "5":
                    // Add item
                    Console.Clear();

                    if (DrinkLogic.AddDrink(title, amount, price, size))
                    {
                        Console.WriteLine("Item added succesfully\n\nPress enter to continue...");
                        item_added = true;
                    }
                    else
                    {
                        Console.WriteLine("Not everything filled in...\n\nPress enter to try again...");
                        Console.ReadLine();
                    }
                    break;
                case "6":
                    // Cancel
                    item_added = true;
                    Console.Clear();
                    Console.WriteLine("Item adding cancelled...\n\nPress Enter to continue");
                    break;
            }
        }
    }


    public static void EditFoodMenu()
    {

        List<FoodModel> food_list = FoodLogic.GetAllFood();
        if (food_list.Count == 0)
        {
            Console.Clear();
            Console.WriteLine("No food items found\n\nPress enter to continue...");
            return;
        };

        string food_choice = NavigationMenu.DisplayMenu(food_list, "Select food item you want to edit: (press q to cancel)");
        if (food_choice == null)
        {
            Console.Clear();
            Console.WriteLine("Item editing cancelled...\n\nPress Enter to continue");
            Console.ReadKey();
            return;
        }

        FoodModel food = food_list[Convert.ToInt32(food_choice) - 1];


        List<string> editFoodOptions = new List<string>{
        $"Title | {food.Title}",
        $"Amount | {food.Amount}",
        $"Price | {food.Price.ToString("F2")}\n",
        $"Save",
        $"Cancel"
        };


        bool item_edited = false;

        while (item_edited == false)
        {
            string choice = NavigationMenu.DisplayMenu(editFoodOptions, "Edit food item:\n");


            switch (choice)
            {
                case "1":
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("Fill in a title:");

                        string user_input = Console.ReadLine();

                        if (!string.IsNullOrEmpty(user_input))
                        {
                            food.Title = user_input;
                            editFoodOptions[0] = $"Changed title | {food.Title}";
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Cannot be empty\n");
                            Console.WriteLine("Press enter to try again...");
                            Console.ReadLine();
                        }
                    }
                    while (string.IsNullOrEmpty(food.Title));
                    break;
                case "2":
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("Fill in an amount: (only numbers)");

                        string amount_input = Console.ReadLine();

                        if (!string.IsNullOrEmpty(amount_input))
                        {
                            if (int.TryParse(amount_input, out int parsed_amount) && parsed_amount > 0)
                            {
                                food.Amount = parsed_amount;
                                editFoodOptions[1] = $"Change amount | {food.Amount}";
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Invalid input, Enter a positive number\n");
                                Console.WriteLine("Press enter to try again...");
                                Console.ReadLine();
                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Cannot be empty\n");
                            Console.WriteLine("Press enter to try again...");
                            Console.ReadLine();
                        }
                    }
                    while (food.Amount == default);

                    break;
                case "3":
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("Fill in a price: (only numbers)");

                        string price_input = Console.ReadLine();

                        if (!string.IsNullOrEmpty(price_input))
                        {
                            if (double.TryParse(price_input, out double parsed_price) && parsed_price > 0)
                            {
                                food.Price = parsed_price;
                                editFoodOptions[2] = $"Change price | {food.Price.ToString("F2")}";
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Invalid input, Enter a price\n");
                                Console.WriteLine("Press enter to try again...");
                                Console.ReadLine();

                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Cannot be empty\n");
                            Console.WriteLine("Press enter to try again...");
                            Console.ReadLine();
                        }
                    }
                    while (food.Price == default);
                    break;
                case "4":
                    Console.Clear();
                    FoodLogic.UpdateFood(food_list);
                    Console.WriteLine("Item updated successfully\n\nPress enter to continue...");
                    item_edited = true;
                    break;
                case "5":
                    item_edited = true;
                    Console.Clear();
                    Console.WriteLine("Item editing cancelled...\n\nPress Enter to continue");
                    break;
            }
        }
    }

    public static void EditDrinkMenu()
    {
        List<DrinkModel> drink_list = DrinkLogic.GetAllDrinks();
        if (drink_list.Count == 0)
        {
            Console.Clear();
            Console.WriteLine("No drink items found\n\nPress enter to continue...");
            return;
        };

        string drink_choice = NavigationMenu.DisplayMenu(drink_list, "Select drink item you want to edit: (press q to cancel)");
        if (drink_choice == null)
        {
            Console.Clear();
            Console.WriteLine("Item editing cancelled...\n\nPress Enter to continue");
            Console.ReadKey();
            return;
        }

        DrinkModel drink = drink_list[Convert.ToInt32(drink_choice) - 1];


        List<string> editFoodOptions = new List<string>{
        $"Title | {drink.Title}",
        $"Amount | {drink.Amount}",
        $"Price | {drink.Price.ToString("F2")}",
        $"Size | {drink.Size}\n",
        $"Save",
        $"Cancel"
        };


        bool item_edited = false;

        while (item_edited == false)
        {
            string choice = NavigationMenu.DisplayMenu(editFoodOptions, "Edit drink item:\n");


            switch (choice)
            {
                case "1":
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("Fill in a title:");

                        string user_input = Console.ReadLine();

                        if (!string.IsNullOrEmpty(user_input))
                        {
                            drink.Title = user_input;
                            editFoodOptions[0] = $"Changed title | {drink.Title}";
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Cannot be empty\n");
                            Console.WriteLine("Press enter to try again...");
                            Console.ReadLine();
                        }
                    }
                    while (string.IsNullOrEmpty(drink.Title));
                    break;
                case "2":
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("Fill in an amount: (only numbers)");

                        string amount_input = Console.ReadLine();

                        if (!string.IsNullOrEmpty(amount_input))
                        {
                            if (int.TryParse(amount_input, out int parsed_amount) && parsed_amount > 0)
                            {
                                drink.Amount = parsed_amount;
                                editFoodOptions[1] = $"Change amount | {drink.Amount}";
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Invalid input, Enter a positive number\n");
                                Console.WriteLine("Press enter to try again...");
                                Console.ReadLine();
                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Cannot be empty\n");
                            Console.WriteLine("Press enter to try again...");
                            Console.ReadLine();
                        }
                    }
                    while (drink.Amount == default);

                    break;
                case "3":
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("Fill in a price: (only numbers)");

                        string price_input = Console.ReadLine();

                        if (!string.IsNullOrEmpty(price_input))
                        {
                            if (double.TryParse(price_input, out double parsed_price) && parsed_price > 0)
                            {
                                drink.Price = parsed_price;
                                editFoodOptions[2] = $"Change price | {drink.Price.ToString("F2")}";
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Invalid input, Enter a price\n");
                                Console.WriteLine("Press enter to try again...");
                                Console.ReadLine();

                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Cannot be empty\n");
                            Console.WriteLine("Press enter to try again...");
                            Console.ReadLine();
                        }
                    }
                    while (drink.Price == default);
                    break;
                case "4":
                    do
                    {
                        Console.Clear();
                        List<string> sizes = Enum.GetNames(typeof(Sizes)).ToList();

                        string sizeIndex = NavigationMenu.DisplayMenu(sizes, "Pick a size:\n");

                        if (sizeIndex != null)
                        {
                            drink.Size = (Sizes)int.Parse(sizeIndex) - 1;
                            editFoodOptions[3] = $"Change size | {drink.Size}";
                        }
                    }
                    while (drink.Size == default);
                    break;
                case "5":
                    Console.Clear();
                    DrinkLogic.UpdateDrinks(drink_list);
                    Console.WriteLine("Item updated successfully\n\nPress enter to continue...");
                    item_edited = true;
                    break;
                case "6":
                    item_edited = true;
                    Console.Clear();
                    Console.WriteLine("Item editing cancelled...\n\nPress Enter to continue");
                    break;
            }
        }
    }
}