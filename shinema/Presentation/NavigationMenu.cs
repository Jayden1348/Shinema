public static class NavigationMenu
{
    public static string DisplayMenu(List<string> menu)
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
                if (selectedOptionIndex != menu.Count -1)
                {
                    selectedOptionIndex++;
                }
            }
        }
        return Convert.ToString(selectedOptionIndex + 1);
    }
}