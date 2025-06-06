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
                "Catering settings",
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
                "Delete movie",
                "Delete showing"
            };

            List<string> adminCinemaOptions = new List<string>{
                "Edit cinema information",
                "View Reservations",
            };

            List<string> adminCateringOptions = new List<string>{
                "Add food",
                "Delete food",
                "Edit food",
                "Add drinks",
                "Delete drinks",
                "Edit drinks"
            };



            string choice = NavigationMenu.DisplayMenu(adminMenuOptions);
            if (choice == "1")
            {
                Console.Clear();
                Console.WriteLine($"Email: {user.EmailAddress}\nFullname: {user.FullName}\n\nPress any key to continue...");
                Console.ReadKey();
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

                        string newTitle = "";
                        bool correctTitle = false;
                        while (!correctTitle)
                        {
                            Console.WriteLine("Enter new title:");
                            string title = Console.ReadLine();
                            if (string.IsNullOrEmpty(title))
                            {
                                Console.WriteLine("Enter a movie title!");
                                Thread.Sleep(1000);
                                Console.Clear();

                            }
                            else
                            {
                                correctTitle = true;
                                newTitle = title;
                                Thread.Sleep(1000);
                                Console.Clear();
                            }


                        }



                        bool correctLength = false;
                        int newLength = 0;

                        while (!correctLength)
                        {

                            Console.WriteLine("Enter new length in minutes:");
                            int result;
                            string newLengthStr = Console.ReadLine();
                            if (string.IsNullOrEmpty(newLengthStr))
                            {
                                Console.WriteLine("Enter a number!");
                                Thread.Sleep(1000);
                                Console.Clear();
                            }
                            if (int.TryParse(newLengthStr, out result))
                            {
                                if (result >= 0)
                                {
                                    correctLength = true;
                                    newLength = result;
                                }
                                else
                                {
                                    Console.WriteLine("Length cannot be negative!");
                                    Thread.Sleep(1000);
                                    Console.Clear();
                                }
                            }
                            else
                            {
                                Console.WriteLine("Enter a valid amount of minutes!");
                                Thread.Sleep(1000);
                                Console.Clear();
                            }
                        }
                        Thread.Sleep(1000);
                        Console.Clear();

                        bool correctAge = false;
                        string Age = "";

                        while (!correctAge)
                        {
                            Console.WriteLine("Enter age:");
                            string newAge = Console.ReadLine();

                            if (string.IsNullOrEmpty(newAge))
                            {
                                Console.WriteLine("Enter a number!");
                                Thread.Sleep(1000);
                                Console.Clear();
                            }
                            else if (int.TryParse(newAge, out int result))
                            {
                                if (result >= 0)
                                {
                                    Age = newAge;
                                    correctAge = true;
                                    Thread.Sleep(1000);
                                    Console.Clear();

                                }
                                else
                                {
                                    Console.WriteLine("Age cannot be negative!");
                                    Thread.Sleep(1000);
                                    Console.Clear();

                                }

                            }
                            else
                            {
                                Console.WriteLine("Age must be a number!");
                                Thread.Sleep(1000);
                                Console.Clear();
                            }
                        }


                        bool correctDescr = false;
                        string newDescr = "";
                        while (!correctDescr)
                        {
                            Console.WriteLine("Enter new description: ");
                            string description = Console.ReadLine();

                            if (string.IsNullOrEmpty(description))
                            {
                                Console.WriteLine("Enter a movie description!");
                                Thread.Sleep(1000);
                                Console.Clear();

                            }
                            else
                            {
                                newDescr = description;
                                correctDescr = true;
                                Thread.Sleep(1000);
                                Console.Clear();

                            }

                        }


                        bool isValid = false;
                        List<string> genres = new List<string>();

                        while (!isValid)
                        {
                            Console.WriteLine("Enter new genre or genres (comma separated):");
                            string input = Console.ReadLine();


                            if (string.IsNullOrWhiteSpace(input))
                            {
                                Console.WriteLine("Enter at least one movie genre!");
                                Thread.Sleep(1000);
                                Console.Clear();
                            }
                            else
                            {

                                genres = input.Split(',').Select(genre => genre.Trim()).Where(genre => !string.IsNullOrWhiteSpace(genre)).ToList();

                                if (input.Contains(" ") && !input.Contains(","))
                                {
                                    Console.WriteLine("Put a comma between the genres!");
                                    Thread.Sleep(1000);
                                    Console.Clear();
                                }

                                else if (genres.Count == 1 && !input.Contains(","))
                                {
                                    isValid = true;
                                }
                                else if (genres.Count > 1 && input.Contains(","))
                                {
                                    isValid = true;
                                }
                                else
                                {
                                    Console.WriteLine("Genres are not entered correctly, try again.");
                                    Thread.Sleep(1000);
                                    Console.Clear();
                                }
                            }
                        }
                        Thread.Sleep(1000);
                        Console.Clear();

                        bool correctReleaseDate = false;
                        string releaseDate = "";

                        while (!correctReleaseDate)
                        {
                            Console.WriteLine("Enter new release date (example: 2024):");
                            string newReleaseDate = Console.ReadLine();

                            if (string.IsNullOrEmpty(newReleaseDate))
                            {
                                Console.WriteLine("Enter a releasedate!");
                                Thread.Sleep(1000);
                                Console.Clear();
                            }
                            else if (int.TryParse(newReleaseDate, out int result))
                            {
                                if (result >= 0)
                                {
                                    releaseDate = newReleaseDate;
                                    correctReleaseDate = true;
                                    Thread.Sleep(1000);
                                    Console.Clear();

                                }
                                else
                                {
                                    Console.WriteLine("Releasedate cannot be negative!");
                                    Thread.Sleep(1000);
                                    Console.Clear();
                                }
                            }
                            else
                            {
                                Console.WriteLine("Release date must be a number!");
                                Thread.Sleep(1000);
                                Console.Clear();
                            }
                        }

                        MovieModel movie = new MovieModel(MoviesLogic.GetNextMovieID(), newTitle, newLength, Age, newDescr, genres, releaseDate);
                        Console.WriteLine("This is what it will look like:\n");
                        Console.WriteLine($"ID: {movie.ID}");
                        Console.WriteLine($"Title: {movie.Title}");
                        Console.WriteLine($"Length: {movie.Length} minutes");
                        Console.WriteLine($"Age: {movie.Age}+");
                        Console.WriteLine($"Description: {movie.Description}");
                        Console.WriteLine($"Genre: {string.Join(", ", movie.Genre)}");
                        Console.WriteLine($"Release Date: {movie.Release_Date}\n");
                        Console.WriteLine();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        Console.Clear();
                        Console.WriteLine();
                        int movieAddMenu = Convert.ToInt32(NavigationMenu.DisplayMenu(new List<string> { "Save Movie", "Don't Save Movie" }, "Do you want to save the movie? "));
                        if (movieAddMenu == 1)
                        {
                            Console.Clear();
                            MoviesLogic.AddMovie(movie.ID, newTitle, newLength, Age, newDescr, genres, releaseDate);
                            Console.WriteLine("Movie Info Saved");
                            Console.WriteLine("Going back to menu....");
                            addMovie = false;
                            Thread.Sleep(5000);
                            Console.Clear();

                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Movie not saved.");
                            Console.WriteLine("Going back to menu....");
                            addMovie = false;
                            Thread.Sleep(5000);
                            Console.Clear();

                        }




                    }
                }
                else if (choice == "2")
                {
                    //edit movie
                    Console.Clear();
                    Console.WriteLine("Press any key to see existing movies...");
                    Console.ReadKey();
                    Console.Clear();
                    Console.WriteLine(MoviesLogic.ListMovies(user.isAdmin));
                    Console.WriteLine();
                    bool correctMovieID = false;
                    int movieChoice = 0;
                    while (!correctMovieID)
                    {
                        Console.WriteLine("Enter ID from movie you want to edit:");
                        string newMovieChoice = Console.ReadLine();
                        if (string.IsNullOrEmpty(newMovieChoice))
                        {
                            Console.WriteLine("Enter a movieID!");
                            Thread.Sleep(2000);
                            Console.Clear();
                        }
                        else if (int.TryParse(newMovieChoice, out int result))
                        {
                            if (result >= 0 && MoviesLogic.GetById(result) is not null)
                            {
                                movieChoice = result;
                                correctMovieID = true;
                                Thread.Sleep(1000);
                                Console.Clear();

                            }
                            else if (MoviesLogic.GetById(result) is null)
                            {
                                Console.WriteLine("Enter a existing ID!");
                                Thread.Sleep(2000);
                                Console.Clear();

                            }
                            else
                            {
                                Console.WriteLine("MovieID cannot be negative!");
                                Thread.Sleep(2000);
                                Console.Clear();
                            }

                        }
                        else
                        {
                            Console.WriteLine("Enter a movieID, check the list of movies.");
                            Thread.Sleep(2000);
                            Console.Clear();

                        }





                    }

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
                                bool correctInput = false;
                                while (!correctInput)
                                {
                                    Console.WriteLine($"Current movie title is: {movie.Title}");
                                    Console.WriteLine("Enter the new title:");
                                    string newTitle = Console.ReadLine();
                                    if (string.IsNullOrEmpty(newTitle))
                                    {
                                        Console.WriteLine("Enter a title!");
                                        Thread.Sleep(1000);
                                        Console.Clear();
                                    }
                                    else
                                    {
                                        movie.Title = newTitle;
                                        Console.WriteLine($"New movie title is: {movie.Title}");
                                        MoviesLogic.UpdateMovieList(movie);
                                        Console.WriteLine("Movie information updated successfully!");
                                        correctInput = true;
                                        Thread.Sleep(2000);
                                        Console.Clear();
                                    }


                                }




                            }
                            else if (movieEditMenu == 2)
                            {

                                bool correctInput = false;
                                while (!correctInput)
                                {
                                    Console.WriteLine($"Current movie length is: {movie.Length}");
                                    Console.WriteLine("Enter the new length (in minutes):");
                                    var newLength = Console.ReadLine();
                                    if (int.TryParse(newLength, out int length))
                                    {
                                        if (length > 0)
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
                                            Console.WriteLine("Movie length cannot be negative");
                                            Thread.Sleep(2000);
                                            Console.Clear();

                                        }

                                    }
                                    else
                                    {
                                        Console.WriteLine("Enter length in numbers!");
                                        Thread.Sleep(2000);
                                        Console.Clear();


                                    }



                                }



                            }
                            else if (movieEditMenu == 3)
                            {
                                bool correctInput = false;
                                while (!correctInput)
                                {
                                    Console.WriteLine($"Current movie age is: {movie.Age}");
                                    Console.WriteLine("Enter the new age:");
                                    string newAge = Console.ReadLine();
                                    if (int.TryParse(newAge, out int age))
                                    {
                                        if (age > 0)
                                        {
                                            movie.Age = newAge;
                                            Console.WriteLine($"New movie age is: {movie.Age}");
                                            MoviesLogic.UpdateMovieList(movie);
                                            Console.WriteLine("Movie information updated successfully!");
                                            correctInput = true;
                                            Thread.Sleep(2000);
                                            Console.Clear();
                                        }
                                        else
                                        {
                                            Console.WriteLine("Age cannot be negative!");
                                            Thread.Sleep(2000);
                                            Console.Clear();
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Enter input in numbers.");
                                        Thread.Sleep(2000);
                                        Console.Clear();

                                    }

                                }


                            }
                            else if (movieEditMenu == 4)
                            {
                                bool correctInput = false;
                                while (!correctInput)
                                {
                                    Console.WriteLine($"Current movie description is: {movie.Description}");
                                    Console.WriteLine("Enter the new description:");
                                    string newDescription = Console.ReadLine();
                                    if (string.IsNullOrEmpty(newDescription))
                                    {
                                        Console.WriteLine("Enter a description!");
                                        Thread.Sleep(1000);
                                        Console.Clear();
                                    }
                                    else
                                    {
                                        movie.Description = newDescription;
                                        Console.WriteLine($"New movie description is: {movie.Description}");
                                        MoviesLogic.UpdateMovieList(movie);
                                        Console.WriteLine("Movie information updated successfully!");
                                        correctInput = true;
                                        Thread.Sleep(2000);
                                        Console.Clear();
                                    }

                                }




                            }
                            else if (movieEditMenu == 5)
                            {
                                bool correctInput = false;
                                while (!correctInput)
                                {
                                    Console.WriteLine($"Current movie genre is: {movie.Genre.ToList()}");
                                    Console.WriteLine("Enter the new genre (comma-separated list if multiple genres):");
                                    string inputGenres = Console.ReadLine();
                                    if (string.IsNullOrEmpty(inputGenres))
                                    {
                                        Console.WriteLine("Enter genres(s)!");
                                        Thread.Sleep(1000);
                                        Console.Clear();
                                    }
                                    else
                                    {
                                        List<string> newGenres = inputGenres.Split(',').Select(genre => genre.Trim()).ToList();
                                        movie.Genre = newGenres;
                                        Console.WriteLine($"New movie genre(s): {string.Join(", ", movie.Genre)}");
                                        MoviesLogic.UpdateMovieList(movie);
                                        Console.WriteLine("Movie information updated successfully!");
                                        correctInput = true;
                                        Thread.Sleep(2000);
                                        Console.Clear();
                                    }

                                }


                            }
                            else if (movieEditMenu == 6)
                            {
                                bool correctInput = false;
                                while (!correctInput)
                                {
                                    Console.WriteLine($"Current movie releasedate is: {movie.Release_Date}");
                                    Console.WriteLine("Enter new releasedate");
                                    string newReleaseDate = Console.ReadLine();

                                    if (int.TryParse(newReleaseDate, out int year))
                                    {
                                        if (year > 0)
                                        {
                                            movie.Release_Date = newReleaseDate;
                                            Console.WriteLine($"New releasedate is: {movie.Release_Date}");
                                            MoviesLogic.UpdateMovieList(movie);
                                            Console.WriteLine("Movie information updated successfully!");
                                            correctInput = true;
                                            Thread.Sleep(2000);
                                            Console.Clear();
                                        }
                                        else
                                        {
                                            Console.WriteLine("Releasedate cannot be negative!");
                                            Thread.Sleep(2000);
                                            Console.Clear();
                                        }

                                    }
                                    else
                                    {
                                        Console.WriteLine("Releasedate must be number!");
                                        Thread.Sleep(2000);
                                        Console.Clear();
                                    }
                                }





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
                        else
                        {
                            Console.WriteLine("Movie does not exist, enter a valid ID");
                            Thread.Sleep(2000);
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
                            ShowingModel new_show = new ShowingModel(new_id, hall_id, movie_id, datetime);
                            int test = s.ValidateDate(new_show);

                            int result_adding = s.AddNewShowing(new_id, hall_id, movie_id, datetime, test);
                            if (result_adding == 1)
                            {
                                Console.WriteLine("Succesfully added new showing!");
                                Thread.Sleep(3000);
                                good_datetime = true;
                            }
                            else if (result_adding == 2)
                            {
                                Console.WriteLine("Given date was in the past!");
                                Thread.Sleep(3000);
                            }
                            else if (result_adding == 3)
                            {
                                Console.WriteLine("Given date is outside opening hours!");
                                Thread.Sleep(3000);
                            }
                            else if (result_adding == 4)
                            {
                                Console.WriteLine("This hall is already showing a movie at this time!");
                                Thread.Sleep(3000);
                            }
                            else
                            {
                                Console.WriteLine("Given date was wrong!");
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
                else if (choice == "5")
                {
                    //delete showing
                    List<ShowingModel> showingsList = showingLogic.GetAllShowings();
                    if (showingsList.Count == 0)
                    {
                        Console.Clear();
                        Console.WriteLine("There are no showings to delete.\nPress any key to continue...");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.Clear();
                        ReservationLogic reservationLogic = new ReservationLogic();
                        string user_input = NavigationMenu.DisplayMenu(showingsList, "Choose a showing to delete:");

                        if (user_input != null)
                        {
                            ShowingModel chosen_showing = showingsList[Convert.ToInt32(user_input) - 1];
                            if (ReservationLogic.CheckReservationExistByShowingID(chosen_showing.ID))
                            {
                                string user_input2 = NavigationMenu.DisplayMenu(new List<string> { "Yes", "No" }, $"There are reservations for this showing. Are you sure you want to delete it?");
                                if (user_input2 == "1")
                                {
                                    List<string> reservationCodes = reservationLogic.GetReservationCodes(chosen_showing.ID);
                                    barReservation.RemoveBarSeatReservation(reservationCodes);
                                    reservationLogic.DeleteReservation(chosen_showing.ID);
                                    showingLogic.DeleteShowing(chosen_showing.ID);
                                    Console.Clear();
                                    Console.WriteLine("Showing has been deleted.\nReservations have been deleted.\nBarreservations have been deleted.\nPress any key to continue...");
                                    Console.ReadKey();
                                }
                                else if (user_input2 == "2")
                                {
                                    Console.Clear();
                                    Console.WriteLine("You have not deleted the showing.\nPress any key to continue...");
                                    Console.ReadKey();
                                }
                            }
                            else
                            {
                                string user_input3 = NavigationMenu.DisplayMenu(new List<string> { "Yes", "No" }, "Are you sure you want to delete this showing?");
                                if (user_input3 == "1")
                                {
                                    showingLogic.DeleteShowing(chosen_showing.ID);
                                    Console.Clear();
                                    Console.WriteLine("Showing has been deleted.\nPress any key to continue...");
                                    Console.ReadKey();
                                }
                                else if (user_input3 == "2")
                                {
                                    Console.Clear();
                                    Console.WriteLine("You have not deleted the showing.\nPress any key to continue...");
                                    Console.ReadKey();
                                }

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
                    // View Reservations
                    Console.Clear();
                    ReservationLogic reservationLogic = new ReservationLogic();
                    string user_input = "";
                    if (reservationLogic.GetAllReservations().Count == 0)
                    {
                        Console.WriteLine("There are currently no reservations made\nPress any key to continue...");
                        Console.ReadKey();
                    }
                    else
                    {
                        user_input = NavigationMenu.DisplayMenu(new List<string> { "Yes", "No" }, "Do you want to see the reservations of all the movies?");
                    }


                    if (user_input == "1")
                    {
                        List<ReservationModel> reservations = reservationLogic.GetAllReservationsSorted();
                        if (reservations is null)
                        {
                            Console.WriteLine("There are currently no reservations made\nPress any key to continue...");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.Clear();
                            foreach (ReservationModel reservation in reservations)
                            {
                                Console.WriteLine(reservation.AllDetails());
                                Console.WriteLine("-----------------------------------");
                            }
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                        }
                    }
                    else if (user_input == "2")
                    {
                        List<string> Allmovies = MoviesLogic.movieNames();
                        if (Allmovies.Count == 0)
                        {
                            Console.Clear();
                            Console.WriteLine("There are currently no reservations made\nPress any key to continue...");
                            Console.ReadKey();
                        }
                        else if (Allmovies.Count > 0)
                        {
                            string user_input2 = NavigationMenu.DisplayMenu(Allmovies, "Choose a movie to view the Reservations:\n");

                            if (user_input2 != null)
                            {
                                int user_choice_index = Convert.ToInt32(user_input2) - 1;
                                MovieModel chosen_movie = MoviesLogic.GetByTitle(Allmovies[user_choice_index]);

                                List<int> showings = showingLogic.GetShowingID(chosen_movie.ID);
                                if (showings.Count == 0)
                                {
                                    Console.Clear();
                                    Console.WriteLine("There are currently no reservations made\nPress any key to continue...");
                                    Console.ReadKey();
                                }
                                else
                                {
                                    List<ReservationModel> reservations = reservationLogic.GetReservationsByShowingIDs(showings);

                                    string user_input3 = NavigationMenu.DisplayMenu(new List<string> { "Yes", "No" }, $"Do you want to see a specific showing of {chosen_movie.Title}?");
                                    if (user_input3 == "1")
                                    {
                                        List<ShowingModel> showings2 = showingLogic.FilterByMovie(chosen_movie);
                                        string user_input4 = NavigationMenu.DisplayMenu(showings2, "Choose a showing to view the Reservations:\n");
                                        if (user_input4 != null)
                                        {
                                            int user_choice_index2 = Convert.ToInt32(user_input4);

                                            if (reservations.Count == 0)
                                            {
                                                Console.Clear();
                                                Console.WriteLine("There are currently no reservations made\nPress any key to continue...");
                                                Console.ReadKey();
                                            }
                                            else
                                            {
                                                List<ReservationModel> reservations2 = reservationLogic.GetReservationsByShowingID(showings2[user_choice_index2 - 1].ID);
                                                Console.Clear();
                                                foreach (ReservationModel reservation in reservations2)
                                                {
                                                    Console.WriteLine(reservation.AllDetails());
                                                    Console.WriteLine("-----------------------------------");
                                                }
                                                Console.WriteLine("Press any key to continue...");
                                                Console.ReadKey();
                                            }
                                        }
                                    }
                                    else if (user_input3 == "2")
                                    {
                                        Console.Clear();
                                        foreach (ReservationModel reservation in reservations)
                                        {
                                            Console.WriteLine(reservation.AllDetails());
                                            Console.WriteLine("-----------------------------------");
                                        }
                                        Console.WriteLine("Press any key to continue...");
                                        Console.ReadKey();
                                    }
                                }

                            }
                        }
                    }
                }

            }
            else if (choice == "5")
            {
                choice = NavigationMenu.DisplayMenu(adminCateringOptions);

                if (choice == "1")
                {
                    //Add food
                    Console.Clear();
                    FoodMenu.AddFoodMenu();
                    Console.ReadLine();
                }
                else if (choice == "2")
                {
                    //Delete food
                    Console.Clear();
                    FoodMenu.DeleteFoodMenu();
                    Console.ReadLine();
                }
                else if (choice == "3")
                {
                    //Edit food
                    Console.Clear();
                    FoodMenu.EditFoodMenu();
                }
                else if (choice == "4")
                {
                    //Add drinks
                    Console.Clear();
                    FoodMenu.AddDrinkMenu();
                    Console.ReadLine();
                }
                else if (choice == "5")
                {
                    //Delete drinks
                    Console.Clear();
                    FoodMenu.DeleteDrinkMenu();
                    Console.ReadLine();
                }
                else if (choice == "6")
                {
                    //Edit drinks
                    Console.Clear();
                    FoodMenu.EditDrinkMenu();
                }
            }
            else if (choice == "6")
            {
                Console.Clear();

                Sales.MainSalesInteraction();
            }
            else if (choice == "7")
            {
                Console.Clear();
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
                Console.WriteLine($"Your current email:\n{user.EmailAddress}\n");
                Console.WriteLine("Requirements:\n- Has to have @\n- Atleast 5 letters");
                Console.WriteLine("\nNew email:");
                string newEmail = Console.ReadLine();
                if (accountsLogic.CheckEmail(newEmail))
                {
                    Console.Clear();
                    Console.WriteLine($"Your old email:\n{user.EmailAddress}\n");
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
                Console.WriteLine($"Your current full name:\n{user.FullName}\n");
                Console.WriteLine("Requirements:\n- Only letters\nException: -");
                Console.WriteLine("\nNew full name:");
                string newfullName = Console.ReadLine();
                if (AccountsLogic.CheckFullName(newfullName))
                {
                    Console.Clear();
                    Console.WriteLine($"Your old full name:\n{user.FullName}\n");
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
                Console.WriteLine("Requirements:\n- A capital letter\n- Atleast 8 letters\n- A number\nOptional symbols to use: !, @, #, $, %, &\n");
                Console.WriteLine("Your new password:");
                string newPassword = NavigationMenu.DisplayBlurredPassword("", "Requirements:\n- A capital letter\n- Atleast 8 letters\n- A number\nOptional symbols to use: !, @, #, $, %, &\n\nYour new password:");
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