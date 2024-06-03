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
                    while (amount == default);

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
                    while (price == default);
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
        List<FoodModel> food_list = GenericAccess<FoodModel>.LoadAll(); ;

        string food_choice = NavigationMenu.DisplayMenu(food_list, "Select food item you want to delete:");

        Console.Clear();
        string confirm_delete = NavigationMenu.DisplayMenu(new() { "Yes", "No" }, $"Are your sure you want to delete {food_list[Convert.ToInt32(food_choice) - 1].Title}?");
        switch (confirm_delete)
        {
            case "1":
                Console.Clear();
                food_list.Remove(food_list[Convert.ToInt32(food_choice) - 1]);

                GenericAccess<FoodModel>.WriteAll(food_list);
                Console.WriteLine("Deleted");
                break;
            case "2":
                Console.Clear();
                Console.WriteLine("Deletion cancelled");
                break;
        }
    }
}