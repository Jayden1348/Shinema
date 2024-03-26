public static class SeatReservation
{
    public static void Start(ShowingModel show)
    {
        SeatsLogic.ShowHall(show);
        Console.WriteLine("\n ... ... ");
    }
    public static void ShowGrid(int columns, List<List<SeatModel>> seatlist)
    {
        string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        int row_letter = 0;

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
            row_letter++;
            Console.Write($"{letters[row_letter - 1]} | ");

            foreach (SeatModel seat in row)
            {
                Console.ResetColor();
                if (seat == null)
                {
                    Console.Write("   ");
                }
                else if (!seat.Available) { Console.Write(" X "); }
                else
                {
                    switch (seat.Rank)
                    {
                        case 1: Console.ForegroundColor = ConsoleColor.Blue; Console.Write(" ■ "); break;
                        case 2: Console.ForegroundColor = ConsoleColor.Yellow; Console.Write(" ■ "); break;
                        case 3: Console.ForegroundColor = ConsoleColor.Red; Console.Write(" ■ "); break;
                        default: Console.ForegroundColor = ConsoleColor.White; Console.Write(" ■ "); break;
                    }
                }
                Console.ResetColor();

            }
            Console.WriteLine("");
        }
    }
}