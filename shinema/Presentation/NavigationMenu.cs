public static class NavigationMenu
{
    public static string DisplayMenu(List<string> menu, string optional_question)
    {
        //List<string> menu is a list of all options
        //example: {option 1, option2, option 3}

        //DisplayMenu will show a menu which you can navigate using arrows
        //When pressing enter this method will return the option number (index + 1)
        //So when pressing enter on option 2 this method will return 2 (the second item in the menu)


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
            if (pressedKey.Key == ConsoleKey.UpArrow)
            {
                if (selectedOptionIndex != 0)
                {
                    selectedOptionIndex--;
                }
            }
            else if (pressedKey.Key == ConsoleKey.DownArrow)
            {
                if (selectedOptionIndex != menu.Count - 1)
                {
                    selectedOptionIndex++;
                }
            }
        }
        return Convert.ToString(selectedOptionIndex + 1);
    }

    public static string DisplayMenu(List<string> menu)
    {
        return DisplayMenu(menu, null);
    }

    public static string DisplayGrid(List<List<SeatModel>> seats)
    {
        int columns = seats[1].Count;
        int rows = seats.Count;

        // for (int selectedSeatRow = 0; selectedSeatRow < rows; selectedSeatRow++)
        // {
        //     for (int selectedSeatCol = 0; selectedSeatCol < columns; selectedSeatCol++)
        //     {
        //         if ()
        //     }
        // }
        int selectedSeatRow = 6;
        int selectedSeatCol = 6;


        ConsoleKeyInfo pressedKey = default;
        while (pressedKey.Key != ConsoleKey.Enter)
        {

            Console.Clear();
            SeatReservation.ShowGrid(columns, seats, selectedSeatRow + 1, selectedSeatCol + 1);
            Console.WriteLine("\nChoose the seat you want to reserve: \nBlue seats:        €10,00\nYellow seats:      €12,50\nRed seats (VIP):   €15,00\n");

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
        return $"{selectedSeatRow}-{selectedSeatCol}";
    }

}