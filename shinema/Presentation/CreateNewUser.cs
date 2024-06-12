using System.Reflection.Metadata;

public static class CreateNewUser
{
    static private AccountsLogic accountsLogic = new AccountsLogic();
    public static void Create()
    {
        int id = accountsLogic.GetNextId();
        string newEmail = "";
        string newPassword = "";
        string newName = "";

        bool correct_email = true;
        while (correct_email)
        {
            Console.Clear();
            Console.WriteLine("What is your emailAddress?");
            Console.WriteLine("Requirements:\n- Has to have @\n- Atleast 5 letters\n");
            string email = Console.ReadLine();
            bool testEmail = accountsLogic.CheckEmail(email);
            if (testEmail)
            {
                correct_email = false;
                newEmail = email;
            }
            else
            {
                Console.WriteLine("Incorrect email\nTry again!");
            }
            Thread.Sleep(1500);
            Console.Clear();
        }



        bool correct_password = true;
        while (correct_password)
        {
            Console.WriteLine("What is your password?");
            Console.WriteLine("Requirements:\n- A cappital letter\n- Atleast 8 letters\n- A number\n");
            string password = NavigationMenu.DisplayBlurredPassword("", "What is your password?\nRequirements:\n- A cappital letter\n- Atleast 8 letters\n- A number\n");
            bool testPassword = AccountsLogic.CheckPassword(password);
            if (testPassword)
            {
                correct_password = false;
                newPassword = password;
            }
            else
            {
                Console.WriteLine("Incorrect password\nTry again!");
            }

            Thread.Sleep(1500);
            Console.Clear();
        }

        bool correct_name = true;
        while (correct_name)
        {
            Console.WriteLine("What is your full name?");
            Console.WriteLine("Requirements:\n- Only letters\nException: -\n");
            string name = Console.ReadLine();
            bool testFullName = AccountsLogic.CheckFullName(name);
            if (testFullName)
            {
                newName = name;
                correct_name = false;
            }
            else
            {
                Console.WriteLine("Incorrect Name\nTry again!");
            }
            Thread.Sleep(1500);
            Console.Clear();
        }


        bool validAccount = accountsLogic.AddNewAccount(id, newEmail, newPassword, newName, true, true, true);
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
        string newEmail = "";
        string newPassword = "";
        string newName = "";

        bool correct_email = true;
        while (correct_email)
        {
            Console.Clear();
            Console.WriteLine("What is your emailAddress?");
            Console.WriteLine("Requirements:\n- Has to have @\n- Atleast 5 letters\n");
            string email = Console.ReadLine();
            bool testEmail = accountsLogic.CheckEmail(email);
            if (testEmail)
            {
                correct_email = false;
                newEmail = email;
            }
            else
            {
                Console.WriteLine("Incorrect email\nTry again!");
            }
            Thread.Sleep(1500);
            Console.Clear();
        }



        bool correct_password = true;
        while (correct_password)
        {
            Console.WriteLine("What is your password?");
            Console.WriteLine("Requirements:\n- A cappital letter\n- Atleast 8 letters\n- A number\nOptional symbols to use: !, @, #, $, %, &");
            string password = Console.ReadLine();
            bool testPassword = AccountsLogic.CheckPassword(password);
            if (testPassword)
            {
                correct_password = false;
                newPassword = password;
            }
            else
            {
                Console.WriteLine("Incorrect password\nTry again!");
            }

            Thread.Sleep(1500);
            Console.Clear();
        }

        bool correct_name = true;
        while (correct_name)
        {
            Console.WriteLine("What is your full name?");
            Console.WriteLine("Requirements:\n- Only letters\nException: -\n");
            string name = Console.ReadLine();
            bool testFullName = AccountsLogic.CheckFullName(name);
            if (testFullName)
            {
                newName = name;
                correct_name = false;
            }
            else
            {
                Console.WriteLine("Incorrect Name\nTry again!");
            }
            Thread.Sleep(1500);
            Console.Clear();
        }

        bool validAccount = accountsLogic.AddNewAccount(id, newEmail, newPassword, newName, true, true, true, true);
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