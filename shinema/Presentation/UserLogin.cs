static class UserLogin
{
    static private AccountsLogic accountsLogic = new AccountsLogic();


    public static void Start()
    {
        Console.WriteLine("Welcome to the login page");
        Console.WriteLine("Please enter your email address");
        string email = Console.ReadLine();
        Console.WriteLine("\nPlease enter your password");
        string password = Console.ReadLine();
        AccountModel acc = accountsLogic.CheckLogin(email, password);
        if (acc != null)
        {
            Console.Clear();
            Console.WriteLine("\nWelcome back " + acc.FullName);

            Thread.Sleep(4000);
            Console.Clear();
            Menu.UserInterface(acc);
        }
        else
        {
            bool boolaccount = true;
            while (boolaccount)
            {
                Console.Clear();
                Console.WriteLine("No account found with that email and password");
                Console.WriteLine("Do you want to create a account? (y/n)\n");
                string newaccount = Console.ReadLine().ToLower();
                if (newaccount == "y")
                {
                    CreateNewUser.Create();
                    boolaccount = false;
                }
                else if (newaccount == "n")
                {
                    Console.WriteLine("See you next time!");
                    boolaccount = false;
                }
            }
        }
    }
}