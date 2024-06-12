public static class IntroLogic
{
    public static void PrintWithDelay(string line, int delay)
    {
        foreach (char c in line)
        {
            Console.Write(c);
            Thread.Sleep(delay);
        }
    }
}