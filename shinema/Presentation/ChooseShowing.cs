public static class ChooseShowing
{   
    public static List<MovieModel> Allmovies = MoviesLogic.GetAllMovies();
    static private ShowingsLogic showingLogic = new ShowingsLogic();

    public static void ChooseShow(AccountModel user)
    {
        bool choosing = true;
        while (choosing)
        {
            // Select Movie
            Allmovies = MoviesLogic.GetAllMovies();
            string user_input = NavigationMenu.DisplayMenu(Allmovies, "Choose a movie:\nPress S to sort\n");
            if (user_input == null) { return; }
            int user_choice_index = Convert.ToInt32(user_input) - 1;
            MovieModel chosen_movie = Allmovies[user_choice_index];

            // Select Showing
            List<ShowingModel> allshowings = showingLogic.FilterByMovie(chosen_movie);
            if (allshowings.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("There are no shows yet for this movie! Check again later.");
                Thread.Sleep(2000);
            }
            else
            {
                user_input = NavigationMenu.DisplayMenu(allshowings, $"Choose a date and time for {chosen_movie}:\n");
                if (user_input == null) { return; }
                user_choice_index = Convert.ToInt32(user_input) - 1;
                ShowingModel chosen_showing = allshowings[user_choice_index];

                choosing = !SeatReservation.StartReservation(user, chosen_showing);
            }
        }
    }

}