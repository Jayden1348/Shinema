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

        if (keywords != null)
        {
            _movies = SortMovies(keywords);
        }

        if (_movies.Count > 0)
        {
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
        }
        else
        {

            return "\nNo movies to see...\n";
        }
    }

    public static List<MovieModel> SortMovies(List<string> keywords)
    {

        List<MovieModel> movies = new List<MovieModel>();

        foreach (MovieModel movie in _movies)
        {
            // make first letter of keyword of c uppercase
            if (keywords.Any(c => movie.Genre.Contains(char.ToUpper(c[0]) + c.Substring(1))))
            {
                movies.Add(movie);
            }
        }

        return movies;
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

    public static int GetNextMovieID()
    {
        int counter = 1;
        foreach (MovieModel movie2 in _movies)
        {
            if (movie2.ID != counter)
            {
                return counter;
            }
            counter++;
        }
        return _movies.Count + 1;


    }


    public static void ListAMovie(MovieModel movie)
    {
        movie.ID = GetNextMovieID();


        Console.WriteLine("This is what it will look like:\n");
        Console.WriteLine($"ID: {movie.ID}");
        Console.WriteLine($"Title: {movie.Title}");
        Console.WriteLine($"Length: {movie.Length}");
        Console.WriteLine($"Description: {movie.Description}");
        Console.WriteLine($"Genre: {string.Join(", ", movie.Genre)}");
        Console.WriteLine($"Release Date: {movie.Release_Date}\n");

    }




    public static List<string> movieNames()
    {
        List<string> movieNames = new List<string>();
        foreach (MovieModel movie in _movies)
        {
            movieNames.Add(movie.Title);
        }
        return movieNames;
    }

    public static MovieModel GetByTitle(string title)
    {
        return _movies.Find(i => i.Title == title);
    }

}