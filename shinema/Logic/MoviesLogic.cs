public class MoviesLogic
{
    private static List<MovieModel> _movies;

    static MoviesLogic()
    {
        _movies = MoviesAccess.LoadAll();
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

    public static void EditMovie()
    {
        Console.WriteLine("Enter the ID of the movie you want to edit:");
        int movieID = int.Parse(Console.ReadLine());

        // Zoek de film met de opgegeven ID
        MovieModel movieToEdit = _movies.FirstOrDefault(movie => movie.ID == movieID);

        if (movieToEdit != null)
        {
            Console.WriteLine("Enter the new title:");
            movieToEdit.Title = Console.ReadLine();

            Console.WriteLine("Enter the new length (in minutes):");
            movieToEdit.Length = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter the new description:");
            movieToEdit.Description = Console.ReadLine();

            Console.WriteLine("Enter the new showing ID:");
            movieToEdit.ShowingID = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter the new genre (comma-separated list if multiple genres):");
            string input = Console.ReadLine();
            List<string> newGenres = input.Split(',').Select(genre => genre.Trim()).ToList();
            movieToEdit.Genre = newGenres;


            Console.WriteLine("Enter the new release date:");
            movieToEdit.Release_Date = Console.ReadLine();

            // Schrijf de bijgewerkte filmgegevens terug naar het bestand
            MoviesAccess.WriteAll(_movies);
            Console.WriteLine("Movie information updated successfully!");
        }
        else
        {
            Console.WriteLine("Movie with the specified ID not found!");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
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


}