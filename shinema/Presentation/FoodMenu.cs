public static class FoodMenu {
    public static void AddFoodMenu() {

        List<string> addFoodOptions = new List<string>{
            "Title (required)",
            "Amount (required)",
            "Price (required)",
            "Add item"
        };

        string title = default;
        int amount = default;
        double price = default;

        bool item_added = false;

        while(item_added == false) {
            string choice = NavigationMenu.DisplayMenu(addFoodOptions, "Add food item:\n");

            switch (choice) {
                case "1":
                    do {
                        title = default;
                        Console.Clear();
                        Console.WriteLine("Fill in a title:");

                        string user_input = Console.ReadLine();

                        if (!string.IsNullOrEmpty(user_input)) {
                            title = user_input;
                            addFoodOptions[0] = $"Change title | {title}";
                        } else {
                            Console.Clear();
                            Console.WriteLine("Cannot be empty\n");
                            Console.WriteLine("Press any enter to try again...");
                            Console.ReadLine();
                        }
                    }
                    while (string.IsNullOrEmpty(title));

                    break;
                case "2":
                    do {
                        amount = default;

                        Console.Clear();
                        Console.WriteLine("Fill in an amount: (only numbers)");

                        string amount_input = Console.ReadLine();

                        if (!string.IsNullOrEmpty(amount_input)) {
                            if (int.TryParse(amount_input, out int parsed_amount)) {
                            amount = parsed_amount;
                            addFoodOptions[1] = $"Change amount | {amount}";
                            } else {
                                Console.Clear();
                                Console.WriteLine("Invalid input, Enter a number\n");
                                Console.WriteLine("Press any enter to try again...");
                                Console.ReadLine();

                            }
                        } else {
                            Console.Clear();
                            Console.WriteLine("Cannot be empty\n");
                            Console.WriteLine("Press any enter to try again...");
                            Console.ReadLine();
                        }
                    }
                    while (amount == default);

                    break;
                case "3":
                    do {
                        price = default;

                        Console.Clear();
                        Console.WriteLine("Fill in a price: (only numbers)");

                        string price_input = Console.ReadLine();

                        if (!string.IsNullOrEmpty(price_input)) {
                            if (double.TryParse(price_input, out double parsed_price)) {
                            price = parsed_price;
                            addFoodOptions[2] = $"Change price | {price.ToString("F2")}";
                            } else {
                                Console.Clear();
                                Console.WriteLine("Invalid input, Enter a price\n");
                                Console.WriteLine("Press any enter to try again...");
                                Console.ReadLine();

                            }
                        } else {
                            Console.Clear();
                            Console.WriteLine("Cannot be empty\n");
                            Console.WriteLine("Press any enter to try again...");
                            Console.ReadLine();
                        }
                    }
                    while (price == default);
                    break;
                case "4":

                    Console.Clear();

                    if(!string.IsNullOrEmpty(title) && amount != default && price != default) {
                        FoodLogic.AddFood(title, amount, price);

                        Console.WriteLine("Item added succesfully\n\nPress enter to continue...");
                        item_added = true;
                    } else {
                        Console.Clear();
                        Console.WriteLine("Not everything filled in yet\n");
                        Console.WriteLine("Press any enter to try again...");
                        Console.ReadLine();
                    }

                    break;
            }
        }
    }
}