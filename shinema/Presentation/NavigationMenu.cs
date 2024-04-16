public static class NavigationMenu
{
    public static string DisplayMenu<T>(List<T> menu, string optional_question)
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


    public static List<string> DisplayGrid(List<List<SeatModel>> seats)
    {
        string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string seat_position = "";
        int columns = seats[1].Count;
        int rows = seats.Count;
        int selectedSeatRow = 0;
        int selectedSeatCol = 0;
        bool set_only_once = true;

        for (int searchrow = 0; searchrow < rows; searchrow++)
        {
            for (int searchcol = 0; searchcol < columns; searchcol++)
            {
                if (seats[searchrow][searchcol] != null && seats[searchrow][searchcol].Available == true && set_only_once == true)
                {
                    selectedSeatRow = searchrow;
                    selectedSeatCol = searchcol;
                    set_only_once = false;

                }
            }
        }
        if (set_only_once == true)
        {
            Console.Clear();
            Console.WriteLine("It seems that all seats for this show have been reserved! Please choose another movie or show.");
            Thread.Sleep(3000);
            return null;
        }

        ConsoleKeyInfo pressedKey = default;
        while (pressedKey.Key != ConsoleKey.Enter)
        {

            Console.Clear();
            SeatReservation.ShowGrid(seats, selectedSeatRow + 1, selectedSeatCol + 1);
            seat_position = $"{letters[selectedSeatRow]}{selectedSeatCol + 1}";
            Console.WriteLine($"\nPrice seat {seat_position}: €{seats[selectedSeatRow][selectedSeatCol].GetPrice()}");

            pressedKey = Console.ReadKey();

            if (pressedKey.Key == ConsoleKey.Q) { return null; }

            if (pressedKey.Key == ConsoleKey.UpArrow)
            {

                if (selectedSeatRow > 0 && seats[selectedSeatRow - 1][selectedSeatCol] != null && seats[selectedSeatRow - 1][selectedSeatCol].Available == true)
                {
                    selectedSeatRow--;
                }
            }

            else if (pressedKey.Key == ConsoleKey.DownArrow)
            {
                if (selectedSeatRow < rows - 1 && seats[selectedSeatRow + 1][selectedSeatCol] != null && seats[selectedSeatRow + 1][selectedSeatCol].Available == true)
                {
                    selectedSeatRow++;

                }
            }
            else if (pressedKey.Key == ConsoleKey.LeftArrow)
            {
                if (selectedSeatCol > 0 && seats[selectedSeatRow][selectedSeatCol - 1] != null && seats[selectedSeatRow][selectedSeatCol - 1].Available == true)
                {
                    selectedSeatCol--;
                }
            }
            else if (pressedKey.Key == ConsoleKey.RightArrow)
            {
                if (selectedSeatCol < columns - 1 && seats[selectedSeatRow][selectedSeatCol + 1] != null && seats[selectedSeatRow][selectedSeatCol + 1].Available == true)
                {
                    selectedSeatCol++;
                }

            }
        }

        return new List<string> { seat_position, selectedSeatRow.ToString(), selectedSeatCol.ToString() };
    }

}