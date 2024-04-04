public static class ChooseShowing
{
    static private ShowingsLogic showingLogic = new ShowingsLogic();

    public static void ChooseShow(AccountModel user)
    {
        // Select Movie
        List<string> all_options = new();
        List<MovieModel> allmovies = MoviesAccess.LoadAll();
        foreach (MovieModel movie in allmovies)
        {
            all_options.Add(movie.Title);
        }
        string user_input = NavigationMenu.DisplayMenu(all_options, "Choose a movie:\n");
        int user_choice_index = Convert.ToInt32(user_input) - 1;
        MovieModel chosen_movie = allmovies[user_choice_index];

        // Select Showing
        List<ShowingModel> allshowings = showingLogic.FilterByMovie(chosen_movie);
        all_options = new();
        foreach (ShowingModel s in allshowings)
        {
            all_options.Add($"{s.Datetime.Date.ToShortDateString()} {s.Datetime.ToShortTimeString()} (hall {s.RoomID})");
        }
        user_input = NavigationMenu.DisplayMenu(all_options, "Choose a date and time:\n");
        user_choice_index = Convert.ToInt32(user_input) - 1;
        ShowingModel chosen_showing = allshowings[user_choice_index];

        SeatReservation.StartReservation(user, chosen_showing);

    }

}