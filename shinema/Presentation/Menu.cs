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
            Console.WriteLine("Enter 3 to reserve seats");
            Console.WriteLine("Enter 4 to get cinema info");
            Console.WriteLine("Enter 5 to log out");

            string choice = Console.ReadLine();
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
                // Temporary show
                ShowingModel show = new ShowingModel(1, 3, 1, new DateTime(2015, 12, 25), new DateTime(2015, 12, 25));
                SeatReservation.StartReservation(user, show);
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
            Console.Clear();
            Console.WriteLine("Enter 1 to show your info");
            Console.WriteLine("Enter 2 to change your information");
            Console.WriteLine("Enter 3 to add a new admin account");
            Console.WriteLine("Enter 4 to edit movie information");
            Console.WriteLine("Enter 5 to delete movie");
            Console.WriteLine("Enter 6 to add movie");
            Console.WriteLine("Enter 7 to edit cinema information");
            Console.WriteLine("Enter 8 to log out");

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
                Console.WriteLine(MoviesLogic.ListMovies(user.isAdmin));
                Console.WriteLine();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine("Do you want to edit a movie? (y/n)");
                string wantToEditMovie = Console.ReadLine();
                if (wantToEditMovie == "y")
                {
                    Console.Clear();
                    Console.WriteLine("Which movie do you want to edit?");
                    int movieChoice = int.Parse(Console.ReadLine());
                    MovieModel movie = MoviesLogic.CheckIfMovieExist(movieChoice);
                    bool chosingEdit = true;
                    while (chosingEdit != false)
                    {
                        if (movie != null)
                        {
                            Console.Clear();
                            Console.WriteLine("What do you want to edit?");
                            Console.WriteLine("1. ID");
                            Console.WriteLine("2. Title");
                            Console.WriteLine("3. Length");
                            Console.WriteLine("4. Description");
                            Console.WriteLine("5. ShowingID");
                            Console.WriteLine("6. Genre(s)");
                            Console.WriteLine("7. Release Date");
                            Console.WriteLine("8. Exit Menu");
                            int editChoice = int.Parse(Console.ReadLine());
                            Console.Clear();
                            if (editChoice == 1)
                            {
                                List<(int, bool)> movieIDList = new List<(int, bool)> { (1, true) };
                                int movieChoiceInfo = MoviesLogic.GetEditInfo(movieIDList);
                                Console.WriteLine(movieChoiceInfo);
                                if ((movieChoiceInfo + 1) == 1)
                                {
                                    Console.WriteLine("Enter new ID");
                                    int newMovieID = int.Parse(Console.ReadLine());
                                    movie.ID = newMovieID;
                                    Console.WriteLine($"New movie ID is: {movie.ID}");
                                    MoviesLogic.UpdateMovieList(movie);
                                    Console.WriteLine("Movie information updated successfully!");
                                    Thread.Sleep(2000);
                                    Console.Clear();


                                }
                                else
                                {
                                    Console.WriteLine("Invalid number error");
                                }
                            }

                            else if (editChoice == 2)
                            {
                                List<(int, bool)> newTitleList = new List<(int, bool)> { (2, true) };
                                int movieChoiceInfo = MoviesLogic.GetEditInfo(newTitleList);
                                if ((movieChoiceInfo + 1) == 2)
                                {
                                    Console.WriteLine("Enter the new title:");
                                    string newTitle = Console.ReadLine();
                                    movie.Title = newTitle;
                                    Console.WriteLine($"New movie ID is: {movie.ID}");
                                    MoviesLogic.UpdateMovieList(movie);
                                    Console.WriteLine("Movie information updated successfully!");
                                    Thread.Sleep(2000);
                                    Console.Clear();
                                }
                                else
                                {
                                    Console.WriteLine("Invalid number error");
                                }


                            }
                            else if (editChoice == 3)
                            {

                                List<(int, bool)> newLengthList = new List<(int, bool)> { (3, true) };
                                int movieChoiceInfo = MoviesLogic.GetEditInfo(newLengthList);
                                if ((movieChoiceInfo + 1) == 3)
                                {
                                    Console.WriteLine("Enter the new length (in minutes):");
                                    int newLength = int.Parse(Console.ReadLine());
                                    movie.Length = newLength;
                                    Console.WriteLine($"New movie ID is: {movie.Length}");
                                    MoviesLogic.UpdateMovieList(movie);
                                    Console.WriteLine("Movie information updated successfully!");
                                    Thread.Sleep(2000);
                                    Console.Clear();
                                }
                                else
                                {
                                    Console.WriteLine("Invalid number error");
                                }
                            }
                            else if (editChoice == 4)
                            {

                                List<(int, bool)> newDescriptionList = new List<(int, bool)> { (4, true) };
                                int movieChoiceInfo = MoviesLogic.GetEditInfo(newDescriptionList);
                                if ((movieChoiceInfo + 1) == 4)
                                {
                                    Console.WriteLine("Enter the new description:");
                                    string newDescription = Console.ReadLine();
                                    movie.Description = newDescription;
                                    Console.WriteLine($"New movie ID is: {movie.Description}");
                                    MoviesLogic.UpdateMovieList(movie);
                                    Console.WriteLine("Movie information updated successfully!");
                                    Thread.Sleep(2000);
                                    Console.Clear();
                                }
                                else
                                {
                                    Console.WriteLine("Invalid number error");
                                }

                            }
                            else if (editChoice == 5)
                            {

                                List<(int, bool)> newShowingIDList = new List<(int, bool)> { (5, true) };
                                int movieChoiceInfo = MoviesLogic.GetEditInfo(newShowingIDList);
                                if ((movieChoiceInfo + 1) == 5)
                                {
                                    Console.WriteLine("Enter the new showing ID:");
                                    int newShowingID = int.Parse(Console.ReadLine());
                                    movie.ShowingID = newShowingID;
                                    Console.WriteLine($"New movie ID is: {movie.ShowingID}");
                                    MoviesLogic.UpdateMovieList(movie);
                                    Console.WriteLine("Movie information updated successfully!");
                                    Thread.Sleep(2000);
                                    Console.Clear();
                                }
                                else
                                {
                                    Console.WriteLine("Invalid number error");
                                }

                            }
                            else if (editChoice == 6)
                            {

                                List<(int, bool)> inputGenresList = new List<(int, bool)> { (6, true) };
                                int movieChoiceInfo = MoviesLogic.GetEditInfo(inputGenresList);
                                if ((movieChoiceInfo + 1) == 6)
                                {
                                    Console.WriteLine("Enter the new genre (comma-separated list if multiple genres):");
                                    string inputGenres = Console.ReadLine();
                                    List<string> newGenres = inputGenres.Split(',').Select(genre => genre.Trim()).ToList();
                                    movie.Genre = newGenres;
                                    Console.WriteLine($"New movie ID is: {movie.Genre}");
                                    MoviesLogic.UpdateMovieList(movie);
                                    Console.WriteLine("Movie information updated successfully!");
                                    Thread.Sleep(2000);
                                    Console.Clear();
                                }
                                else
                                {
                                    Console.WriteLine("Invalid number error");
                                }
                            }
                            else if (editChoice == 7)
                            {

                                List<(int, bool)> newReleaseDateList = new List<(int, bool)> { (7, true) };
                                int movieChoiceInfo = MoviesLogic.GetEditInfo(newReleaseDateList);
                                if ((movieChoiceInfo + 1) == 7)
                                {
                                    Console.WriteLine("Enter the new release date:");
                                    string newReleaseDate = Console.ReadLine();
                                    movie.Release_Date = newReleaseDate;
                                    Console.WriteLine($"New movie ID is: {movie.Release_Date}");
                                    MoviesLogic.UpdateMovieList(movie);
                                    Console.WriteLine("Movie information updated successfully!");
                                    Thread.Sleep(2000);
                                    Console.Clear();
                                }
                                else
                                {
                                    Console.WriteLine("Invalid number error");
                                }
                            }
                            else if (editChoice == 8)
                            {

                                Console.WriteLine("Exiting menu....");
                                Thread.Sleep(2000);
                                Console.Clear();
                                chosingEdit = false;
                            }
                            else
                            {
                                Console.WriteLine("Invalid choice");
                            }

                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            Console.Clear();

                        }
                    }

                }
                else if (wantToEditMovie == "n")
                {
                    Console.WriteLine("Exiting....");
                    Thread.Sleep(2000);

                }
                else
                {
                    Console.WriteLine("Invalid Choice");
                }
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
                Console.Clear();
                Thread.Sleep(2000);
                bool cinemaInfoRedo = true;
                Console.WriteLine("Current info:\n");
                Console.WriteLine(CinemaInfoLogic.GetCinemaInfo());
                while (cinemaInfoRedo)
                {
                    Console.WriteLine("Enter New Info:\n");
                    Console.WriteLine("What is the city where the cinema is located: (Example: \"Rotterdam\")");
                    string city = Console.ReadLine();
                    Thread.Sleep(2000);

                    Console.WriteLine("\nWhat is the Address: (Example: \"Wijnhaven 107, 3011 WN\")");
                    string address = Console.ReadLine();
                    Thread.Sleep(2000);

                    Console.WriteLine("\nAt what time (24h format) does the cinema open: (Example: \"09:00\")");
                    string openingTime = Console.ReadLine();
                    Thread.Sleep(2000);

                    Console.WriteLine("\nAt what time (24h format) does the cinema close: (Example: \"22:00\")");
                    string closingTime = Console.ReadLine();
                    Thread.Sleep(2000);

                    Console.WriteLine("\nWhat is the phone number of the cinema: ");
                    string phoneNumber = Console.ReadLine();
                    Thread.Sleep(2000);

                    Console.WriteLine("\nWhat is the e-mail of the cinema: ");
                    string email = Console.ReadLine();
                    Thread.Sleep(2000);
                    Console.Clear();

                    Console.WriteLine("\nThis is what it will look like:\n");
                    Console.WriteLine(CinemaInfoLogic.GetCinemaInfo(city, address, openingTime, closingTime, phoneNumber, email));

                    bool cinemaInfoChoosing = true;
                    while (cinemaInfoChoosing)
                    {

                        Console.WriteLine("Enter 1 To Save Cinema Info");
                        Console.WriteLine("Enter 2 To Re-enter The Cinema Info");
                        Console.WriteLine("Enter 3 To Cancel");
                        string choiceCinemaInfo = Console.ReadLine();

                        if (choiceCinemaInfo == "1")
                        {
                            cinemaInfoChoosing = false;
                            cinemaInfoRedo = false;
                            Thread.Sleep(2000);
                            CinemaInfoLogic.SaveCinemaInfo(city, address, openingTime, closingTime, phoneNumber, email);
                            Thread.Sleep(2000);
                            Console.WriteLine("Info Saved");
                        }

                        else if (choiceCinemaInfo == "2")
                        {
                            cinemaInfoChoosing = false;
                        }

                        else if (choiceCinemaInfo == "3")
                        {
                            cinemaInfoChoosing = false;
                            cinemaInfoRedo = false;

                        }

                        else
                        {
                            Console.WriteLine("Invalid Input");
                        }
                    }
                    Console.Clear();
                }
            }
            else if (choice == "8")
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