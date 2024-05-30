public static class NavigationMenu
{
    public static string DisplayMenu<T>(List<T> menu, string optional_question, bool movie_select = false)
    {
        //List<string> menu is a list of all options
        //example: {option 1, option2, option 3}

        //DisplayMenu will show a menu which you can navigate using arrows
        //When pressing enter this method will return the option number (index + 1)
        //So when pressing enter on option 2 this method will return 2 (the second item in the menu)

        Console.CursorVisible = false;
        int selectedOptionIndex = 0;
        ConsoleKeyInfo pressedKey = default;
        while (pressedKey.Key != ConsoleKey.Enter)
        {
            Console.Clear();
            if (optional_question != null)
            {
                Console.WriteLine(optional_question);
            }
            for (int i = 0; i < menu.Count; i++)
            {
                if (i == selectedOptionIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"> {menu.ElementAt(i)}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine(menu.ElementAt(i));
                }
            }

            pressedKey = Console.ReadKey();
            if (pressedKey.Key == ConsoleKey.Q) { return null; }
            if (pressedKey.Key == ConsoleKey.S && movie_select == true)
            {
                Console.Clear();

                menu = ChooseShowing.ShowingSort().Cast<T>().ToList();
            }
            if (pressedKey.Key == ConsoleKey.UpArrow)
            {
                if (selectedOptionIndex != 0)
                {
                    selectedOptionIndex--;
                }
                else
                {
                    selectedOptionIndex = menu.Count - 1;
                }
            }
            else if (pressedKey.Key == ConsoleKey.DownArrow)
            {
                if (selectedOptionIndex != menu.Count - 1)
                {
                    selectedOptionIndex++;
                }
                else
                {
                    selectedOptionIndex = 0;
                }
            }
        }
        Console.CursorVisible = true;
        return Convert.ToString(selectedOptionIndex + 1);
    }

    public static string DisplayMenu<T>(List<T> menu)
    {
        return DisplayMenu(menu, null);
    }

    public static string DisplayMenu(List<string> menu, string optional_question)
    {
        return DisplayMenu<string>(menu, optional_question);
    }

    public static string DisplayMenu(List<string> menu)
    {
        return DisplayMenu<string>(menu, null);
    }


    public static List<string> DisplayGrid(List<List<SeatModel>> seats, List<string> reserved_seats, double total_price_reservation, List<string> previous_position)
    {
        string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string seat_position = "";
        int columns = seats[1].Count;
        int rows = seats.Count;
        int selectedSeatRow = 0;
        int selectedSeatCol = 0;
        bool check_sold_out = true;

        for (int searchrow = 0; searchrow < rows; searchrow++)
        {
            for (int searchcol = 0; searchcol < columns; searchcol++)
            {
                if (seats[searchrow][searchcol] != null && seats[searchrow][searchcol].Available == true)
                {
                    selectedSeatRow = searchrow;
                    selectedSeatCol = searchcol;
                    searchrow += 10000;
                    searchcol += 10000;
                    check_sold_out = false;

                }
            }
        }
        if (reserved_seats.Count == 0 && check_sold_out == true)
        {
            Console.Clear();
            Console.WriteLine("It seems that all seats for this show have been reserved! Please choose another movie or show.");
            Thread.Sleep(3000);
            return null;
        }
        if (previous_position.Count != 0)
        {
            selectedSeatRow = Convert.ToInt32(previous_position[1]);
            selectedSeatCol = Convert.ToInt32(previous_position[2]);
        }


        ConsoleKeyInfo pressedKey = default;
        while (!(pressedKey.Key == ConsoleKey.Enter && (seats[selectedSeatRow][selectedSeatCol].Available || reserved_seats.Contains(seats[selectedSeatRow][selectedSeatCol].Position))))
        {

            Console.Clear();
            SeatReservation.ShowGrid(seats, selectedSeatRow + 1, selectedSeatCol + 1, reserved_seats);
            seat_position = $"{letters[selectedSeatRow]}{selectedSeatCol + 1}";
            Console.Write($"\nPrice seat {seat_position}: ");
            Console.WriteLine($"{(seats[selectedSeatRow][selectedSeatCol].Available ? "\u20AC" + seats[selectedSeatRow][selectedSeatCol].GetPrice() : "Already reserved!")}");
            bool locked_free_select = reserved_seats.Count == 0;

            Console.Write($"\nYour seat(s): ");
            if (reserved_seats.Count != 0)
            {
                Console.Write(reserved_seats[0]);
                foreach (string position in reserved_seats.GetRange(1, reserved_seats.Count - 1))
                {
                    Console.Write($", {position}");
                }
            }
            Console.WriteLine($"\nTotal price: \u20AC{total_price_reservation}");
            Console.WriteLine($"\nPress Enter to select a seat");
            Console.WriteLine($"Press Q to leave, or D to proceed to payment");

            pressedKey = Console.ReadKey();

            if (pressedKey.Key == ConsoleKey.Q) { return null; }
            if (pressedKey.Key == ConsoleKey.D && reserved_seats.Count != 0) { return new List<string> { }; }

            if (pressedKey.Key == ConsoleKey.UpArrow && locked_free_select && selectedSeatRow > 0 && seats[selectedSeatRow - 1][selectedSeatCol] != null)
            {
                selectedSeatRow--;
            }

            else if (pressedKey.Key == ConsoleKey.DownArrow && locked_free_select && selectedSeatRow < rows - 1 && seats[selectedSeatRow + 1][selectedSeatCol] != null)
            {
                selectedSeatRow++;
            }

            else if (pressedKey.Key == ConsoleKey.LeftArrow && selectedSeatCol > 0 && seats[selectedSeatRow][selectedSeatCol - 1] != null)
            {
                if (!locked_free_select)
                {
                    if (reserved_seats.Contains(seats[selectedSeatRow][selectedSeatCol].Position) || reserved_seats.Contains(seats[selectedSeatRow][selectedSeatCol - 1].Position))
                    {
                        selectedSeatCol--;
                    }
                }
                else
                {
                    selectedSeatCol--;
                }
            }


            else if (pressedKey.Key == ConsoleKey.RightArrow && selectedSeatCol < columns - 1 && seats[selectedSeatRow][selectedSeatCol + 1] != null)
            {
                if (!locked_free_select)
                {
                    if (reserved_seats.Contains(seats[selectedSeatRow][selectedSeatCol].Position) || reserved_seats.Contains(seats[selectedSeatRow][selectedSeatCol + 1].Position))
                    {
                        selectedSeatCol++;
                    }
                }
                else
                {
                    selectedSeatCol++;
                }

            }
        }
        if (reserved_seats.Contains(seats[selectedSeatRow][selectedSeatCol].Position))
        {
            return new List<string> { seat_position, selectedSeatRow.ToString(), selectedSeatCol.ToString(), "remove" };
        }
        return new List<string> { seat_position, selectedSeatRow.ToString(), selectedSeatCol.ToString() };

    }

    public static void AwaitKey()
    {
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
}
