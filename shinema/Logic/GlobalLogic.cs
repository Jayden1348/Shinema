public class GlobalLogic
{
    public static bool YN_loop(string user_input)
    {
        while (true)
        {
            if (user_input == "y") { return true; }
            else if (user_input == "n") { return false; }
            else { Console.WriteLine("Enter y or n!"); }

        }
    }
}