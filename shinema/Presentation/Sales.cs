public static class Sales
{
    public static void MovieSalesInteraction()
    {
        // get movie list
        List<MovieModel> movieList = MoviesLogic.GetAllMovies();

        // create a list with movies as options
        List<String> movieOptionsString = movieList.Select(movie => movie.Title).ToList();
       
       // insert all movies option in the option list
        movieOptionsString.Insert(0, "All Movies");

        //display menu
        int selectedMovieMenuChoice = Convert.ToInt16(NavigationMenu.DisplayMenu(movieOptionsString, "Select Option For Turnover")) -1;
        
        // call functions that return a string for display
        Console.WriteLine(SalesLogic.GetTurnOverMovies(selectedMovieMenuChoice, movieList));

        NavigationMenu.AwaitKey();
    }
}