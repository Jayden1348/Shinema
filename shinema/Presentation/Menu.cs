using System.Runtime.InteropServices;
using System.Globalization;
static class Menu
{

    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    static private AccountsLogic accountsLogic = new AccountsLogic();
    static private ShowingsLogic showingLogic = new ShowingsLogic();
    static private BarReservationLogic barReservation = new BarReservationLogic();

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
            List<string> userMenuOptions = new List<string> { "Show your info", "Change your information", "Reserve seats", "My Reservations", "Get cinema info", "Log out" };

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
                string res = NavigationMenu.DisplayMenu(new List<string> { "Movie Reservations", "Bar Reservations" }, "Choose the type of reservations:");
                if (res == "1")
                {
                    ReservationLogic r = new ReservationLogic(user);
                    r.DisplayReservations();
                }
                else if (res == "2")
                {
                    BarReservationLogic b = new BarReservationLogic(user);
                    b.UpdateBarReservations();
                    b.DisplayReservations();
                }

            }


            else if (choice == "5")
            {
                Console.Clear();
                Console.WriteLine(CinemaInfoLogic.GetCinemaInfo());
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            else if (choice == "6")
            {
                Console.Clear();
                Console.WriteLine("You have been logged out!\nPress any key to continue...");
                Console.ReadKey();
                usermenu = false;
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
                "Profile settings",
                "Movie settings",
                "Cinema settings",
                "Sales Info",
                "Log out"
            };

            List<string> adminProfileOptions = new List<string>{
                "Change your information",
                "Add new admin account"
            };

            List<string> adminMovieOptions = new List<string>{
                "Add movie",
                "Edit movie information",
                "Add showing",
                "Delete movie"
            };

            List<string> adminCinemaOptions = new List<string>{
                "Edit cinema information",
                "Add food",
                "Delete food"
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
                //Profile settings
                choice = NavigationMenu.DisplayMenu(adminProfileOptions);

                if (choice == "1")
                {
                    Console.Clear();
                    ChangeInfo(user);
                }
                else if (choice == "2")
                {
                    Console.Clear();
                    CreateNewUser.CreateAdmin();
                }
            }
            else if (choice == "3")
            {
                //Movie settings
                choice = NavigationMenu.DisplayMenu(adminMovieOptions);

                if (choice == "1")

                {
                    //Add movie
                    Console.Clear();
                    MoviesLogic.ListMovies(true);
                    bool addMovie = true;

                    while (addMovie)
                    {
                        Console.WriteLine("Enter new title:");
                        string title = Console.ReadLine();
                        Thread.Sleep(1000);
                        Console.Clear();


                        bool correctInput = false;
                        int newLength = 0;

                        while (!correctInput)
                        {
                            Console.WriteLine("Enter new length in minutes:");
                            int result;
                            string newLengthStr = Console.ReadLine();

                            if (int.TryParse(newLengthStr, out result))
                            {
                                Console.WriteLine("Length is correctly entered! ");
                                correctInput = true;
                                newLength = result;
                            }
                            else
                            {
                                Console.WriteLine("Length is not entered correctly, try again.");
                                Thread.Sleep(1000);
                            }
                        }
                        Thread.Sleep(1000);
                        Console.Clear();

                        Console.WriteLine("Enter new description: ");
                        string description = Console.ReadLine();
                        Thread.Sleep(1000);
                        Console.Clear();

                        bool isValid = false;
                        List<string> genres = new List<string>();

                        while (!isValid)
                        {
                            Console.WriteLine("Enter new genre or genres (comma separated):");
                            string input = Console.ReadLine();

                            genres = input.Split(',').Select(genre => genre.Trim()).ToList();


                            if (input.Contains(" "))
                            {
                                Console.WriteLine("Put a comma between the genres!");
                                Thread.Sleep(1000);
                                Console.Clear();

                            }
                            else if (genres.Count == 1 && input.Contains(","))
                            {

                                Console.WriteLine("Enter a single genre without a comma!");

                            }
                            else if (genres.Count > 1 && input.Contains(","))
                            {
                                Console.WriteLine("Genres are correctly entered!");
                                isValid = true;

                            }
                            else
                            {
                                Console.WriteLine("Genres are not entered correctly, try again.");
                                Thread.Sleep(1000);
                                Console.Clear();

                            }
                        }
                        Thread.Sleep(1000);
                        Console.Clear();


                        Console.WriteLine("Enter new releasedate (example: 2024):");
                        string newReleaseDate = Console.ReadLine();
                        string releaseDate = newReleaseDate;
                        Thread.Sleep(1000);
                        Console.Clear();



                        MovieModel movie = new MovieModel(0, title, newLength, "", description, 0, genres, newReleaseDate);
                        MoviesLogic.MovieAddLoop(movie);
                        MoviesLogic.AddMovie(movie.ID, title, newLength, " ", description, 0, genres, newReleaseDate);
                        addMovie = false;

                    }
                }
                else if (choice == "2")
                {
                    //edit movie
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

                                bool correctInput = false;
                                while (!correctInput)
                                {
                                    Console.WriteLine("Enter the new length (in minutes):");
                                    var newLength = Console.ReadLine();
                                    if (int.TryParse(newLength, out int length))
                                    {
                                        movie.Length = length;
                                        Console.WriteLine($"New movie length is: {movie.Length}");
                                        MoviesLogic.UpdateMovieList(movie);
                                        Console.WriteLine("Movie information updated successfully!");
                                        correctInput = true;
                                        Thread.Sleep(2000);
                                        Console.Clear();
                                    }
                                    else
                                    {
                                        Console.WriteLine("Enter input in numbers.");
                                        Thread.Sleep(2000);
                                        Console.Clear();

                                    }

                                }



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
                else if (choice == "3")

                {
                    //Add new showing
                    List<MovieModel> movies = MoviesLogic.GetAllMovies();
                    int movie_id = Convert.ToInt32(NavigationMenu.DisplayMenu<MovieModel>(movies, "Select a movie for the showing"));
                    int hall_id = Convert.ToInt32(NavigationMenu.DisplayMenu(new List<string> { "Hall 1", "Hall 2", "Hall 3" }, "Select a hall to show the movie"));
                    bool good_datetime = false;
                    while (!good_datetime)
                    {
                        Console.Clear();
                        CultureInfo.CurrentCulture = new CultureInfo("nl-NL");
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

                else if (choice == "4")
                {
                    ReservationLogic reservationLogic = new ReservationLogic();

                    List<string> Allmovies = MoviesLogic.movieNames();
                    string user_input = NavigationMenu.DisplayMenu(Allmovies, "Choose a movie to delete:\n");


                    if (user_input != null)
                    {
                        int user_choice_index = Convert.ToInt32(user_input) - 1;
                        MovieModel chosen_movie = MoviesLogic.GetByTitle(Allmovies[user_choice_index]);



                        if (reservationLogic.CheckReservationExist(chosen_movie.ID))
                        {
                            string user_input2 = NavigationMenu.DisplayMenu(new List<string> { "Yes", "No" }, $"There are reservations for {chosen_movie.Title}. Are you sure you want to delete it?");
                            if (user_input2 == "1")
                            {
                                List<int> showings = showingLogic.GetShowingID(chosen_movie.ID);
                                List<string> reservationCodes = reservationLogic.GetReservationCodes(showings);

                                barReservation.RemoveBarSeatReservation(reservationCodes);
                                reservationLogic.DeleteReservation(showings);
                                showingLogic.DeleteShowing(showings);
                                MoviesLogic.DeleteMovie(chosen_movie.ID);

                                Console.Clear();
                                Console.WriteLine($"{chosen_movie.Title} has been deleted.\nReservations have been deleted.\nBarreservations have been deleted.\nShowings have been deleted.\nPress any key to continue...");
                                Console.ReadKey();
                            }
                            if (user_input2 == "2")
                            {
                                Console.Clear();
                                Console.WriteLine($"You have not deleted {chosen_movie.Title}.\n");
                                Console.WriteLine("Press any key to continue...");
                                Console.ReadKey();
                            }
                        }
                        else
                        {
                            string user_input3 = NavigationMenu.DisplayMenu(new List<string> { "Yes", "No" }, $"Are you sure you want to delete {chosen_movie.Title}?");
                            if (user_input3 == "1")
                            {
                                List<int> showings = showingLogic.GetShowingID(chosen_movie.ID);

                                showingLogic.DeleteShowing(showings);
                                MoviesLogic.DeleteMovie(chosen_movie.ID);

                                Console.Clear();
                                Console.WriteLine($"{chosen_movie.Title} has been deleted.\nShowings have been deleted.\nPress any key to continue...");
                                Console.ReadKey();
                            }
                            else if (user_input3 == "2")
                            {
                                Console.Clear();
                                Console.WriteLine($"You have not deleted {chosen_movie.Title}.\n");
                                Console.WriteLine("Press any key to continue...");
                                Console.ReadKey();
                            }
                        }
                    }
                }
            }
            else if (choice == "4")
            {
                //Cinema settings
                choice = NavigationMenu.DisplayMenu(adminCinemaOptions);

                if (choice == "1")
                {
                    //Change cinema info
                    Console.Clear();
                    Thread.Sleep(1000);
                    bool cinemaInfoRedo = true;
                    Console.WriteLine("Current info:\n");
                    Console.WriteLine(CinemaInfoLogic.GetCinemaInfo());
                    CinemaInfo.EditLoop();
                }
                else if (choice == "2")
                {
                    //Add food
                    Console.Clear();
                    FoodMenu.AddFoodMenu();
                    Console.ReadLine();
                }
                else if (choice == "3")
                {
                    //Delete food
                    Console.Clear();
                    FoodMenu.DeleteFoodMenu();
                    Console.ReadLine();
                }
            }
            else if (choice == "5")
            {
                Console.Clear();

                Sales.MainSalesInteraction();
            }
            else if (choice == "6")
            {
                Console.Clear();
                Console.WriteLine("You have been logged out!");
                Thread.Sleep(2000);

                Console.WriteLine("You have been logged out!\nPress any key to continue...");
                Console.ReadKey();

                usermenu = false;
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
                Console.WriteLine("Requirements:\n- Only letters\nException: -");
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
                Console.WriteLine("Requirements:\n- A cappital letter\n- Atleast 8 letters\n- A number");
                Console.WriteLine("\nYour new password:");
                string newPassword = NavigationMenu.DisplayBlurredPassword("", "Requirements:\n- A cappital letter\n- Atleast 8 letters\n- A number\n\nYour new password:");
                if (AccountsLogic.CheckPassword(newPassword) && AccountsLogic.GetHashString(newPassword) != user.Password)
                {
                    Console.Clear();
                    Console.WriteLine($"Your password is valid!");
                    user.Password = AccountsLogic.GetHashString(newPassword);
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