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

    public static List<MovieModel> GetAllMovies() => MoviesAccess.LoadAll();
}