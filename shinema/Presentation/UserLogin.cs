static class UserLogin
{

    public static void Start()
    {
        AccountsLogic accountsLogic = new AccountsLogic();


        Console.WriteLine("Welcome to the login page");
        Console.WriteLine("Please enter your email address");
        string email = Console.ReadLine();
        Console.WriteLine("\nPlease enter your password");
        string password = NavigationMenu.DisplayBlurredPassword("", $"Welcome to the login page\nPlease enter your email address\n{email}\n\nPlease enter your password");
        AccountModel acc = accountsLogic.CheckLogin(email, password);
        if (acc != null)
        {
            if (acc.isAdmin)
            {
                // hier komt admin menu
                Console.Clear();
                Console.WriteLine("\nWelcome back " + acc.FullName);
                Console.WriteLine("\n\nPress any key to continue...");
                Console.ReadKey();

                Console.Clear();
                Menu.AdminInterface(acc);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("\nWelcome back " + acc.FullName);
                Console.WriteLine("\n\nPress any key to continue...");
                Console.ReadKey();

                Console.Clear();
                Menu.UserInterface(acc);
            }
        }
        else if (accountsLogic.EmailExists(email))
        {
            int count = 3;
            while (count > 0)
            {
                Console.Clear();
                string answer = NavigationMenu.DisplayMenu(new List<string> { "yes", "no" }, "You have entered the wrong password!\nDo you want to try again?\n");
                if (answer == "1")
                {
                    Console.WriteLine("Password:");
                    string newPassword = NavigationMenu.DisplayBlurredPassword("", "You have entered the wrong password!\nDo you want to try again?\n\nPassword:");
                    AccountModel newAcc = accountsLogic.CheckLogin(email, newPassword);
                    if (newAcc != null)
                    {
                        count = 0;
                        if (newAcc.isAdmin)
                        {
                            // hier komt admin menu
                            Console.Clear();
                            Console.WriteLine("\nWelcome back " + newAcc.FullName);
                            Console.WriteLine("\n\nPress any key to continue...");

                            Console.ReadKey();
                            Console.Clear();
                            Menu.AdminInterface(newAcc);
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("\nWelcome back " + newAcc.FullName);
                            Console.WriteLine("\n\nPress any key to continue...");

                            Console.ReadKey();
                            Console.Clear();
                            Menu.UserInterface(newAcc);
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Wrong password entered!");
                        count -= 1;
                        Console.WriteLine($"{count} tries left\n\nPress any key to continue...");
                        Console.ReadKey(true);
                        Console.Clear();
                        if (count == 0)
                        {
                            Console.WriteLine("No more tries left\nReturning to the main menu\n\nPress any key to continue...");
                            Console.ReadKey(true);
                            Console.Clear();
                        }

                    }

                }
                else if (answer == "2")
                {
                    count = 0;
                    Console.Clear();
                    Console.WriteLine("Returning to main menu\n\nPress any key to continue...");
                    Console.ReadKey(true);
                    Console.Clear();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Invalid answer try again!\n\nPress any key to continue...");
                    Console.ReadKey();
                }

            }
        }
        else
        {
            bool boolaccount = true;
            while (boolaccount)
            {
                string newaccount = NavigationMenu.DisplayMenu(new List<string> { "yes", "no" }, "No account found with that email and password\nDo you want to create a account?\n");
                if (newaccount == "1")
                {
                    CreateNewUser.Create();
                    boolaccount = false;
                }
                else if (newaccount == "2")
                {
                    Console.Clear();
                    boolaccount = false;
                }
                else
                {
                    Console.WriteLine("Invalid answer try again!\n\nPress any key to continue...");
                    Console.ReadKey();
                }
            }
        }
    }


}