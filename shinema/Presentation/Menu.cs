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
                Console.WriteLine(MoviesLogic.ListMovies(false));
                Console.WriteLine("Press any key to continue");
                Console.ReadKey(true);
            }
            else if (startInput == "4")
            {
                starting = false;
                Console.Clear();
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
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine(MoviesLogic.ListMovies(user.isAdmin));
                Console.WriteLine();
                Console.WriteLine("Which movie do you want to edit?");
                int movieChoice = int.Parse(Console.ReadLine());
                MovieModel movie = MoviesLogic.CheckIfMovieExist(movieChoice);
                bool chosingEdit = true;
                while (chosingEdit != false)
                {
                    if (movie != null)
                    {
                        int movieEditMenu = Convert.ToInt32(NavigationMenu.DisplayMenu(new List<string> { "Title", "Length", "Age", "Description", "Genre(s)", "Release Date", "Exit Menu" }, "Select a option to edit: "));
                        Console.Clear();
                        if (movieEditMenu == 1)
                        {

                            Console.WriteLine("Enter the new title:");
                            string newTitle = Console.ReadLine();
                            movie.Title = newTitle;
                            Console.WriteLine($"New movie title is: {movie.Title}");
                            MoviesLogic.UpdateMovieList(movie);
                            Console.WriteLine("Movie information updated successfully!");
                            Thread.Sleep(2000);
                            Console.Clear();



                        }
                        else if (movieEditMenu == 2)
                        {


                            Console.WriteLine("Enter the new length (in minutes):");
                            int newLength = int.Parse(Console.ReadLine());
                            movie.Length = newLength;
                            Console.WriteLine($"New movie length is: {movie.Length}");
                            MoviesLogic.UpdateMovieList(movie);
                            Console.WriteLine("Movie information updated successfully!");
                            Thread.Sleep(2000);
                            Console.Clear();


                        }
                        else if (movieEditMenu == 3)
                        {
                            Console.WriteLine("Enter the new age:");
                            string newAge = Console.ReadLine();
                            movie.Age = newAge;
                            Console.WriteLine($"New movie age is: {movie.Age}");
                            MoviesLogic.UpdateMovieList(movie);
                            Console.WriteLine("Movie information updated successfully!");
                            Thread.Sleep(2000);
                            Console.Clear();

                        }
                        else if (movieEditMenu == 4)
                        {


                            Console.WriteLine("Enter the new description:");
                            string newDescription = Console.ReadLine();
                            movie.Description = newDescription;
                            Console.WriteLine($"New movie description is: {movie.Description}");
                            MoviesLogic.UpdateMovieList(movie);
                            Console.WriteLine("Movie information updated successfully!");
                            Thread.Sleep(2000);
                            Console.Clear();


                        }
                        else if (movieEditMenu == 5)
                        {


                            Console.WriteLine("Enter the new genre (comma-separated list if multiple genres):");
                            string inputGenres = Console.ReadLine();
                            List<string> newGenres = inputGenres.Split(',').Select(genre => genre.Trim()).ToList();
                            movie.Genre = newGenres;
                            Console.WriteLine($"New movie genre(s): {string.Join(", ", movie.Genre)}");
                            MoviesLogic.UpdateMovieList(movie);
                            Console.WriteLine("Movie information updated successfully!");
                            Thread.Sleep(2000);
                            Console.Clear();

                        }
                        else if (movieEditMenu == 6)
                        {

                            Console.WriteLine("Enter new releasedate");
                            string newReleaseDate = Console.ReadLine();
                            movie.Release_Date = newReleaseDate;
                            Console.WriteLine($"New releasedate is: {movie.Release_Date}");
                            MoviesLogic.UpdateMovieList(movie);
                            Console.WriteLine("Movie information updated successfully!");
                            Thread.Sleep(2000);
                            Console.Clear();



                        }
                        else if (movieEditMenu == 7)
                        {

                            Console.WriteLine("Exiting menu....");
                            Thread.Sleep(2000);
                            Console.Clear();
                            chosingEdit = false;
                        }

                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        Console.Clear();

                    }
                }

            }

            else if (choice == "5")
            {
                MoviesLogic.ListMovies(true);
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
                MoviesLogic.ListMovies(true);
                MovieModel movie = new MovieModel(0, "", 0, "", "", 0, null, "");
                MoviesLogic.MovieAddLoop(movie);
            }
            else if (choice == "7")
            {
                //Add new showing
                List<MovieModel> movies = MoviesLogic.GetAllMovies();
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
            List<string> changeInfoMenuOptions = new List<string> { "Change your email", "Change your full name", "Change your password", "Confirm your changes" };
            string choice = NavigationMenu.DisplayMenu(changeInfoMenuOptions);

            if (choice == "1")
            {
                Console.Clear();
                Console.WriteLine($"Your current email:\n{user.EmailAddress}");
                Console.WriteLine("Requirements:\n- Has to have @\n- Atleast 5 letters");
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
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
            }
            else if (choice == "2")
            {
                Console.Clear();
                Console.WriteLine($"Your current full name:\n{user.FullName}");
                Console.WriteLine("Requirements:\n- A cappital letter\n- Atleast 8 letters\n- A number");
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
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
            }
            else if (choice == "3")
            {
                Console.Clear();
                Console.WriteLine($"Your current password:\n{AccountsLogic.BlurredPassword(user)}");
                Console.WriteLine("Requirements:\n- Only letters\nException: -");
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
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
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