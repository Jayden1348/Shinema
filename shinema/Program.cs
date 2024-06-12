class Program
{
    static void Main(string[] args)
    {
        Console.Clear();
        Console.ResetColor();

        // Welcome message with a slower delay
        IntroLogic.PrintWithDelay("Welcome to...\n\n", 50);

        Console.ForegroundColor = ConsoleColor.Yellow;

        // ASCII art with a faster delay
        IntroLogic.PrintWithDelay("     _______. __    __   __  .__   __.  _______ .___  ___.      ___     \n", 2);
        IntroLogic.PrintWithDelay("    /       ||  |  |  | |  | |  \\ |  | |   ____||   \\/   |     /   \\    \n", 2);
        IntroLogic.PrintWithDelay("   |   (----`|  |__|  | |  | |   \\|  | |  |__   |  \\  /  |    /  ^  \\   \n", 2);
        IntroLogic.PrintWithDelay("    \\   \\    |   __   | |  | |  . `  | |   __|  |  |\\/|  |   /  /_\\  \\  \n", 2);
        IntroLogic.PrintWithDelay(".----)   |   |  |  |  | |  | |  |\\   | |  |____ |  |  |  |  /  _____  \\ \n", 2);
        IntroLogic.PrintWithDelay("|_______/    |__|  |__| |__| |__| \\__| |_______||__|  |__| /__/     \\__\\", 2);

        Console.ForegroundColor = ConsoleColor.White;

        Console.WriteLine("\n");
        IntroLogic.PrintWithDelay("\nPress any key to continue...", 50);

        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.ReadKey();
        Menu.Start();

    }
}