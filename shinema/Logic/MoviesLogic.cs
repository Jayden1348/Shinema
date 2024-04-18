public class MoviesLogic
{
    private static List<MovieModel> _movies;

    static MoviesLogic()
    {
        _movies = MoviesAccess.LoadAll();
    }

    public static MovieModel GetById(int id)
    {
        return _movies.Find(i => i.ID == id);
    }



    public static string ListMovies(bool admin, List<string> keywords = null)
    {
        string line = "";
        
        if (keywords != null) {
            _movies = SortMovies(keywords);
        }
        
        if (_movies.Count > 0) {
            if (admin)
            {
                foreach (MovieModel movie in _movies)
                {
                    Console.Clear();
                    line += $"MovieID: {movie.ID}\n";
                    line += $"Title: {movie.Title}\n";
                    line += $"Length: {movie.Length} minutes\n";
                    line += $"Age: {movie.Age}\n";
                    line += $"Release date: {movie.Release_Date}\n";
                    line += $"Genre: {string.Join(", ", movie.Genre)}\n";
                    line += $"Description: {movie.Description}\n";
                    line += "\n\n";
                }
            }
            else
            {
                foreach (MovieModel movie in _movies)
                {
                    Console.Clear();
                    line += $"Title: {movie.Title}\n";
                    line += $"Length: {movie.Length} minutes\n";
                    line += $"Age: {movie.Age}\n";
                    line += $"Release date: {movie.Release_Date}\n";
                    line += $"Genre: {string.Join(", ", movie.Genre)}\n";
                    line += $"Description: {movie.Description}\n";
                    line += "\n\n";
                }
            }

            return line;
        } else {

            return "\nNo movies to see...\n";
        }
    }

    public static List<MovieModel> SortMovies(List<string> keywords){

        List<MovieModel> movies = new List<MovieModel>();

        foreach(MovieModel movie in _movies) {
            if (keywords.Any(c => movie.Genre.Contains(c))) {
                movies.Add(movie);
            }
        }

        return movies;
    }

    public static int GetEditInfo(List<(int, bool)> lijst)
    {
        List<List<(int, bool)>> nestedList = new List<List<(int, bool)>> { lijst };
        foreach (List<(int, bool)> infoList in nestedList)
        {
            foreach ((int intValue, bool boolValue) in infoList)
            {
                if (boolValue == true)
                {
                    return ReturnEditInfo(intValue);

                }
                else
                {
                    return ReturnEditInfo(0);
                }

            }

        }
        return 0;

    }

    public static int ReturnEditInfo(int chosennumber)
    {
        if (chosennumber >= 0 && chosennumber < _movies.Count)
        {
            for (int i = 0; i < _movies.Count; i++)
            {
                if (_movies[i].ID == chosennumber)
                {
                    return i;
                }
            }
        }
        return -1;
    }

    public static MovieModel CheckIfMovieExist(int movieID)
    {
        foreach (MovieModel movie in _movies)
        {
            if (movie.ID == movieID)
            {
                return movie;
            }
        }
        return null;

    }

    public static void UpdateMovieList(MovieModel movie)
    {
        //Find if there is already an model with the same id
        int index = _movies.FindIndex(s => s.ID == movie.ID);

        if (index != -1)
        {
            //update existing model
            _movies[index] = movie;
        }
        else
        {
            //add new model
            _movies.Add(movie);
        }
        MoviesAccess.WriteAll(_movies);

    }



    public static bool EditMovie(int movieID, string newTitle, int newLength, string newDescription, int newShowingID, List<string> newGenres, string newReleaseDate)
    {
        // Zoek de film met de opgegeven ID
        MovieModel movieToEdit = _movies.FirstOrDefault(movie => movie.ID == movieID);

        if (movieToEdit != null)
        {
            // Update de filmgegevens
            movieToEdit.Title = newTitle;
            movieToEdit.Length = newLength;
            movieToEdit.Description = newDescription;
            movieToEdit.ShowingID = newShowingID;
            movieToEdit.Genre = newGenres;
            movieToEdit.Release_Date = newReleaseDate;

            // Schrijf de bijgewerkte filmgegevens terug naar het bestand
            MoviesAccess.WriteAll(_movies);

            return true; // Film succesvol bijgewerkt
        }
        else
        {
            return false; // Film met de opgegeven ID niet gevonden
        }
    }
    public static bool DeleteMovie(int movieID)
    {
        foreach (MovieModel movie in _movies)
        {
            if (movie.ID == movieID)
            {
                _movies.Remove(movie);
                MoviesAccess.WriteAll(_movies);
                return true;
            }

        }
        return false;

    }

    public static bool AddMovie(int movieID, string title, int length, string age, string description, int showingID, List<string> genres, string releaseDate)
    {
        // Controleer of de film al bestaat
        if (_movies.Any(movie => movie.ID == movieID))
        {
            return false;
        }
        MovieModel newMovie = new MovieModel(movieID, title, length, age, description, showingID, genres, releaseDate);

        _movies.Add(newMovie);
        MoviesAccess.WriteAll(_movies);

        return true;
    }
  
    public static List<MovieModel> GetAllMovies() => MoviesAccess.LoadAll();


    public static MovieModel returnMoviebyName(string title)
    {
        foreach (MovieModel movie in _movies)
        {
            if (movie.Title == title)
            {
                return movie;
            }
        }
        return null;
    }

    public static void MovieEditLoop(MovieModel movie)
    {
        // Bepaal de hoogste movieID en showingID
        int highestMovieID = _movies.Max(m => m.ID);
        int highestShowingID = _movies.Max(m => m.ShowingID);

        bool MovieInfoRedo = true;
        bool Exit = false;
        while (MovieInfoRedo)
        {
            List<string> options = new List<string> { "Add Title", "Add Length", "Add Description", "Add Genre", "Add Releasedate", "Exit" };
            // Keuzemenu
            string choiceEditMovieInfo = NavigationMenu.DisplayMenu(options);
            Console.Clear();

            if (choiceEditMovieInfo == "1")
            {
                Console.WriteLine("Enter new title");
                movie.Title = Console.ReadLine();
                Thread.Sleep(1000);
            }
            else if (choiceEditMovieInfo == "2")
            {
                Console.WriteLine("Enter new length");
                movie.Length = int.Parse(Console.ReadLine());
                Thread.Sleep(1000);
            }
            else if (choiceEditMovieInfo == "3")
            {
                Console.WriteLine("Enter new description");
                movie.Description = Console.ReadLine();
                Thread.Sleep(1000);
            }
            else if (choiceEditMovieInfo == "4")
            {
                Console.WriteLine("Enter new genre or genres (comma separated)");
                string input = Console.ReadLine();
                List<string> genres = input.Split(',').Select(genre => genre.Trim()).ToList();
                movie.Genre = genres;
                Thread.Sleep(1000);
            }
            else if (choiceEditMovieInfo == "5")
            {
                Console.WriteLine("Enter new releasedate");
                string newReleaseDate = Console.ReadLine();
                movie.Release_Date = newReleaseDate;
            }
            else if (choiceEditMovieInfo == "6")
            {
                Console.WriteLine("Exiting....");
                MovieInfoRedo = false;
                Exit = true;
                Thread.Sleep(1000);
            }

            Console.Clear();
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();

            bool MovieInfoChoosing = true;
            while (MovieInfoChoosing && !Exit)
            {
                List<string> optionEditMovieInfo = new List<string> { "Save Movie Info", "Continue Changing", "Cancel" };
                string choiceMovieInfo = NavigationMenu.DisplayMenu(optionEditMovieInfo);

                if (choiceMovieInfo == "1")
                {
                    // Automatisch toewijzen van movieID en showingID
                    movie.ID = ++highestMovieID;
                    movie.ShowingID = ++highestShowingID;

                    // Validatie en opslaan van filmgegevens
                    if (ValidateMovie(movie))
                    {
                        Console.WriteLine("This is what it will look like:\n");
                        Console.WriteLine($"ID: {movie.ID}");
                        Console.WriteLine($"Title: {movie.Title}");
                        Console.WriteLine($"Length: {movie.Length}");
                        Console.WriteLine($"Description: {movie.Description}");
                        Console.WriteLine($"Showing ID: {movie.ShowingID}");
                        Console.WriteLine($"Genre: {string.Join(", ", movie.Genre)}");
                        Console.WriteLine($"Release Date: {movie.Release_Date}\n");


                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();

                        _movies.Add(movie);
                        MoviesAccess.WriteAll(_movies);
                        Console.WriteLine();
                        Console.WriteLine("Movie Info Saved");
                        Thread.Sleep(5000);
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Movie Info validation failed. Movie not saved.");
                        Thread.Sleep(5000);

                    }

                    MovieInfoChoosing = false;
                }
                else if (choiceMovieInfo == "2")
                {
                    MovieInfoChoosing = false;
                }
                else if (choiceMovieInfo == "3")
                {
                    MovieInfoChoosing = false;
                }
            }

            Console.Clear();
        }
    }


    private static bool ValidateMovie(MovieModel movie)
    {
        if (string.IsNullOrEmpty(movie.Title))
        {
            Console.WriteLine("Enter a movie title!");
            return false;
        }


        if (movie.Length <= 0)
        {
            Console.WriteLine("Enter a valid movie length!");
            return false;
        }


        if (string.IsNullOrEmpty(movie.Description))
        {
            Console.WriteLine("Enter a movie description!");
            return false;
        }

        if (movie.ShowingID <= 0)
        {
            Console.WriteLine("Enter a valid showing ID!");
            return false;
        }

        if (movie.Genre == null)
        {
            Console.WriteLine("Enter at least one movie genre!");
            return false;
        }


        if (string.IsNullOrEmpty(movie.Release_Date))
        {
            Console.WriteLine("Enter a valid release date!");
            return false;
        }


        return true;
    }