public static class CreateNewUser
{
    static private AccountsLogic accountsLogic = new AccountsLogic();
    public static void Create()
    {
        int id = accountsLogic.GetNextId();

        Console.Clear();
        Console.WriteLine("What is your emailAddress?");
        Console.WriteLine("Requirements:\n- Has to have @\n- Atleast 5 letters\n");
        string email = Console.ReadLine();
        bool testEmail = accountsLogic.CheckEmail(email);
        Thread.Sleep(1500);
        Console.Clear();


        Console.WriteLine("What is your password?");
        Console.WriteLine("Requirements:\n- A cappital letter\n- Atleast 8 letters\n- A number\n");
        string password = Console.ReadLine();
        bool testPassword = AccountsLogic.CheckPassword(password);
        Thread.Sleep(1500);
        Console.Clear();



        Console.WriteLine("What is your full name?");
        Console.WriteLine("Requirements:\n- Only letters\n");
        string name = Console.ReadLine();
        bool testFullName = AccountsLogic.CheckFullName(name);
        Thread.Sleep(1500);


        Console.Clear();
        bool validAccount = accountsLogic.AddNewAccount(id, email, password, name, testEmail, testPassword, testFullName);
        if (validAccount)
        {
            Console.WriteLine("Your Account has been added");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine("Account has not been added because of invalid information!");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }

    public static void CreateAdmin()
    {
        int id = accountsLogic.GetNextId();

        Console.WriteLine("What is the emailAddress?");
        Console.WriteLine("Requirements:\n- Has to have @\n- Atleast 5 letters\n");
        string email = Console.ReadLine();
        bool testEmail = accountsLogic.CheckEmail(email);
        Thread.Sleep(1500);
        Console.Clear();


        Console.WriteLine("What is the password?");
        Console.WriteLine("Requirements:\n- A cappital letter\n- Atleast 8 letters\n- A number\n");
        string password = Console.ReadLine();
        bool testPassword = AccountsLogic.CheckPassword(password);
        Thread.Sleep(1500);
        Console.Clear();



        Console.WriteLine("What is the admin full name?");
        Console.WriteLine("Requirements:\n- Only letters\n");
        string name = Console.ReadLine();
        bool testFullName = AccountsLogic.CheckFullName(name);
        Thread.Sleep(1500);


        Console.Clear();
        bool validAccount = accountsLogic.AddNewAccount(id, email, password, name, testEmail, testPassword, testFullName, true);
        if (validAccount)
        {
            Console.WriteLine("Your Account has been added");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine("Account has not been added because of invalid information!");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}