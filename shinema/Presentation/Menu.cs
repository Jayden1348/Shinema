using System.Runtime.InteropServices;

static class Menu
{

    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    static private AccountsLogic accountsLogic = new AccountsLogic();
    static public void Start()
    {
        bool starting = true;

        while (starting)
        {
            Console.Clear();
            Console.WriteLine("Enter 1 to login");
            Console.WriteLine("Enter 2 to create new account");
            Console.WriteLine("Enter 3 to see movies");
            Console.WriteLine("Enter 4 to quit the program");

            string input = Console.ReadLine();
            if (input == "1")
            {
                Console.Clear();
                UserLogin.Start();
                starting = false;
            }
            else if (input == "2")
            {
                CreateNewUser.Create();
            }
            else if (input == "3")
            {
                Console.WriteLine(MoviesLogic.ListMovies());
                Console.WriteLine("Press any key to continue");
                Console.ReadKey(true);
            }
            else if (input == "4")
            {
                starting = false;
            }
            else
            {
                Console.WriteLine("Invalid input");
            }
        }
    }

    static public void UserInterface(AccountModel user)
    {
        bool usermenu = true;
        while (usermenu)
        {
            Console.Clear();
            Console.WriteLine("Enter 1 to show your info");
            Console.WriteLine("Enter 2 to change your information");
            Console.WriteLine("Enter 3 to log out");

            string choice = Console.ReadLine();
            if (choice == "1")
            {
                Console.Clear();
                Console.WriteLine($"Email: {user.EmailAddress}\nFullname: {user.FullName}");
                Thread.Sleep(4000);
            }
            else if (choice == "3")
            {
                Console.Clear();
                Console.WriteLine("You have been logged out!");
                Thread.Sleep(2000);
                usermenu = false;
                Start();
            }
            else if (choice == "2")
            {
                Console.Clear();
                ChangeInfo(user);
            }
        }
    }
    static public void AdminInterface(AccountModel user)
    {
        bool usermenu = true;
        while (usermenu)
        {
            Console.Clear();
            Console.WriteLine("Enter 1 to show your info");
            Console.WriteLine("Enter 2 to change your information");
            Console.WriteLine("Enter 3 to add a new admin account");
            Console.WriteLine("Enter 4 to edit movie information");
            Console.WriteLine("Enter 5 to log out");

            string choice = Console.ReadLine();
            if (choice == "1")
            {
                Console.Clear();
                Console.WriteLine($"Email: {user.EmailAddress}\nFullname: {user.FullName}");
                Thread.Sleep(4000);
            }
            else if (choice == "2")
            {
                Console.Clear();
                ChangeInfo(user);
            }
            else if (choice == "3")
            {
                Console.Clear();
                CreateNewUser.CreateAdmin();
            }
            else if (choice == "4")
            {
                Console.Clear();
                Console.WriteLine(MoviesLogic.ListMovies());
                Console.WriteLine();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
                MoviesLogic.EditMovie();
                Console.Clear();

            }
            else if (choice == "5")
            {
                Console.Clear();
                Console.WriteLine("You have been logged out!");
                Thread.Sleep(2000);
                usermenu = false;
                Start();
            }
        }
    }


    static public void ChangeInfo(AccountModel user)
    {
        bool menu = true;
        while (menu)
        {
            Console.Clear();
            Console.WriteLine("Enter 1 to change your email");
            Console.WriteLine("Enter 2 to change your full name");
            Console.WriteLine("Enter 3 to change your password");
            Console.WriteLine("Enter 4 to confirm your changes");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.Clear();
                Console.WriteLine($"Your current email:\n{user.EmailAddress}");
                Console.WriteLine("\nNew email:");
                string newEmail = Console.ReadLine();
                if (accountsLogic.CheckEmail(newEmail))
                {
                    Console.Clear();
                    Console.WriteLine($"Your old email:\n{user.EmailAddress}");
                    Console.WriteLine($"Your new email:\n{newEmail}");
                    user.EmailAddress = newEmail;
                    Thread.Sleep(3000);

                }
                else
                {
                    Console.WriteLine("Invalid email!");
                    Thread.Sleep(3000);
                }
            }
            else if (choice == "2")
            {
                Console.Clear();
                Console.WriteLine($"Your current full name:\n{user.FullName}");
                Console.WriteLine("\nNew full name:");
                string newfullName = Console.ReadLine();
                if (AccountsLogic.CheckFullName(newfullName))
                {
                    Console.Clear();
                    Console.WriteLine($"Your old full name:\n{user.FullName}");
                    Console.WriteLine($"Your new full name:\n{newfullName}");
                    user.FullName = newfullName;
                    Thread.Sleep(4000);
                }
                else
                {
                    Console.WriteLine("Invalid full name!");
                    Thread.Sleep(3000);
                }
            }
            else if (choice == "3")
            {
                Console.Clear();
                Console.WriteLine($"Your current password:\n{AccountsLogic.BlurredPassword(user)}");
                Console.WriteLine("\nYour new password:");
                string newPassword = Console.ReadLine();
                if (AccountsLogic.CheckPassword(newPassword) && newPassword != user.Password)
                {
                    Console.Clear();
                    Console.WriteLine($"Your new password:\n{newPassword}");
                    user.Password = newPassword;
                    Thread.Sleep(3000);
                }
                else
                {
                    Console.WriteLine("Invalid password!");
                    Thread.Sleep(3000);
                }
            }
            else if (choice == "4")
            {
                accountsLogic.UpdateList(user);
                Console.WriteLine("Your info has been updated");
                Thread.Sleep(2000);
                menu = false;
            }
        }
    }
}