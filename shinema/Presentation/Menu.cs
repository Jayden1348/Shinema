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
            List<string> startOptions = new List<string> { "Login", "Create new account", "See movies", "Quit the program" };
            string startInput = NavigationMenu.DisplayMenu(startOptions);
            if (startInput == "1")
            {
                Console.Clear();
                UserLogin.Start();
                starting = false;
            }
            else if (startInput == "2")
            {
                CreateNewUser.Create();
            }
            else if (startInput == "3")
            {
                Console.WriteLine(MoviesLogic.ListMovies());
                Console.WriteLine("Press any key to continue");
                Console.ReadKey(true);
            }
            else if (startInput == "4")
            {
                starting = false;
            }
        }
    }

    static public void UserInterface(AccountModel user)
    {
        bool usermenu = true;
        while (usermenu)
        {
            List<string> userMenuOptions = new List<string> { "Show your info", "Change your information", "Reserve seats", "Get cinema info", "Log out" };

            string choice = NavigationMenu.DisplayMenu(userMenuOptions);
            if (choice == "1")
            {
                Console.Clear();
                Console.WriteLine($"Email: {user.EmailAddress}\nFullname: {user.FullName}");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
            else if (choice == "2")
            {
                Console.Clear();
                ChangeInfo(user);
            }
            else if (choice == "3")
            {
                ChooseShowing.ChooseShow(user);
            }

            else if (choice == "4")
            {
                Console.Clear();
                Console.WriteLine(CinemaInfoLogic.GetCinemaInfo());
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
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
    static public void AdminInterface(AccountModel user)
    {
        bool usermenu = true;
        while (usermenu)
        {
            List<string> adminMenuOptions = new List<string>
            {
                "Show your info",
                "Change your information",
                "Add a new admin account",
                "Edit movie information",
                "Delete movie",
                "Add movie",
                "Add a showing",
                "Edit cinema information",
                "Log out"
            };
            string choice = NavigationMenu.DisplayMenu(adminMenuOptions);
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
                Console.WriteLine(MoviesLogic.ListMovies(user.isAdmin));
                Console.WriteLine();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
                MoviesLogic.EditMovie();
                Console.Clear();

            }
            else if (choice == "5")
            {
                MoviesLogic.ListMovies();
                Console.WriteLine("Enter ID of movie you want to delete: ");
                int movieID = Convert.ToInt32(Console.ReadLine());

                bool deltetedMovie = MoviesLogic.DeleteMovie(movieID);
                if (deltetedMovie == false)
                {
                    Console.WriteLine("Movie id is not in movielist");

                }
                else
                {
                    Console.WriteLine("Movie is succesfully deleted");
                }



            }
            else if (choice == "6")
            {
                Console.Clear();
                Console.WriteLine("Enter the details of the new movie:");
                Console.WriteLine("Enter the movie ID:");
                int movieID = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter the movie title:");
                string title = Console.ReadLine();

                Console.WriteLine("Enter the movie length (in minutes):");
                int length = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter the movie description:");
                string description = Console.ReadLine();

                Console.WriteLine("Enter the showing ID:");
                int showingID = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter the movie genre (comma-separated list if multiple genres):");
                string input = Console.ReadLine();
                List<string> genres = input.Split(',').Select(genre => genre.Trim()).ToList();

                Console.WriteLine("Enter the movie release date:");
                string releaseDate = Console.ReadLine();

                bool success = MoviesLogic.AddMovie(movieID, title, length, description, showingID, genres, releaseDate);
                if (success == true)
                {
                    Console.WriteLine("Movie added successfully!");
                }
                else
                {
                    Console.WriteLine("Failed to add movie. Movie with the specified ID already exists!");
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();

            }
            else if (choice == "7")
            {
                //Add new showing
                List<MovieModel> movies = MoviesAccess.LoadAll();
                int movie_id = Convert.ToInt32(NavigationMenu.DisplayMenu<MovieModel>(movies, "Select a movie for the showing"));
                int hall_id = Convert.ToInt32(NavigationMenu.DisplayMenu(new List<string> { "Hall 1", "Hall 2", "Hall 3" }, "Select a hall to show the movie"));
                bool good_datetime = false;
                while (!good_datetime)
                {
                    Console.Clear();
                    Console.WriteLine($"Enter date: (format: {DateTime.Now.ToString("d")})");
                    string datestring = Console.ReadLine();
                    Console.WriteLine($"\nEnter time: (format: {DateTime.Now.ToString("t")})");
                    string timestring = Console.ReadLine();
                    Console.Clear();
                    DateTime datetime = ShowingsLogic.SetToDatetime(datestring, timestring);
                    if (datetime == new DateTime())
                    {
                        Console.WriteLine("Wrong date inputs!");
                        Thread.Sleep(3000);
                    }
                    else
                    {
                        ShowingsLogic s = new ShowingsLogic();
                        int new_id = s.GetNextId();
                        bool test = ShowingsLogic.ValidateDate(datetime);
                        ShowingModel new_show = new ShowingModel(new_id, hall_id, movie_id, datetime);
                        bool result_adding = s.AddNewShowing(new_id, hall_id, movie_id, datetime, test);
                        if (result_adding)
                        {
                            Console.WriteLine("Succesfully added new showing!");
                            Thread.Sleep(3000);
                            good_datetime = true;
                        }
                        else
                        {
                            Console.WriteLine("Given date was in the past!");
                            Thread.Sleep(3000);
                        }
                    }
                }
            }
            else if (choice == "8")
            {
                //Change cinema info
                Console.Clear();
                Thread.Sleep(1000);
                bool cinemaInfoRedo = true;
                Console.WriteLine("Current info:\n");
                Console.WriteLine(CinemaInfoLogic.GetCinemaInfo());
                CinemaInfo.EditLoop();



            }
            else if (choice == "9")
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
            List<string> changeInfoMenuOptions = new List<string> { "Change your email", "Change your full name", "Change your password", "Confirm your changes" };
            string choice = NavigationMenu.DisplayMenu(changeInfoMenuOptions);

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
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();

                }
                else
                {
                    Console.WriteLine("Invalid email!");
                    Thread.Sleep(2500);
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
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Invalid full name!");
                    Thread.Sleep(2500);
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
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
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
                Console.Clear();
                Console.WriteLine("Your info has been updated");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                menu = false;
            }
        }
    }
}