public class MoviesLogic {
    private static List<MovieModel> _movies;

    static MoviesLogic() {
        _movies = MoviesAccess.LoadAll();
    }

    public static string ListMovies() {
        string line = "";

        foreach(MovieModel movie in _movies) {
            Console.Clear();
            line += $"Title: {movie.Title}\n";
            line += $"Length: {movie.Length} minutes\n";
            line += $"Release date: {movie.Release_Date}\n";
            line += $"Genre: {movie.Genre}\n";
            line += $"Description: {movie.Description}\n";
            line += "\n\n";
        }

        return line;
    }
}