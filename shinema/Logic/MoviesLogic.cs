using Microsoft.VisualBasic;

public class MoviesLogic
{
    private static List<MovieModel> _movies;

    static MoviesLogic()
    {
        _movies = MoviesAccess.LoadAll();
    }

    public static string ListMovies(bool admin = false)
    {
        string line = "";

        if (admin)
        {
            foreach (MovieModel movie in _movies)
            {
                Console.Clear();
                line += $"MovieID: {movie.ID}\n";
                line += $"Title: {movie.Title}\n";
                line += $"Length: {movie.Length} minutes\n";
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
                line += $"Release date: {movie.Release_Date}\n";
                line += $"Genre: {string.Join(", ", movie.Genre)}\n";
                line += $"Description: {movie.Description}\n";
                line += "\n\n";
            }
        }

        return line;
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

    public static bool AddMovie(int movieID, string title, int length, string description, int showingID, List<string> genres, string releaseDate)
    {
        // Controleer of de film al bestaat
        if (_movies.Any(movie => movie.ID == movieID))
        {
            return false;
        }
        MovieModel newMovie = new MovieModel(movieID, title, length, description, showingID, genres, releaseDate);

        _movies.Add(newMovie);
        MoviesAccess.WriteAll(_movies);

        return true;
    }


}