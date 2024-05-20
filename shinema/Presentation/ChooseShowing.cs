using System.Security.Cryptography.X509Certificates;

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
            string user_input = NavigationMenu.DisplayMenu(Allmovies, "Choose a movie:\nPress S to sort\n", true);
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

    public static List<MovieModel> ShowingSort() {

        Allmovies = MoviesLogic.GetAllMovies();

        List<string> genres = new();

        foreach(var movie in Allmovies) {
            foreach(var genre in movie.Genre) {
                if(!genres.Contains(genre)){
                    genres.Add(genre);
                }
            }
        }

        List<string> selected_genres = new();

        bool end_genre_select = false;

        while (end_genre_select == false) {
            string genre_input = NavigationMenu.DisplayMenu(genres, "Select genre:");

            if(genre_input != null) {
                selected_genres.Add(genres[Convert.ToInt32(genre_input) - 1]);
                genres.Remove(genres[Convert.ToInt32(genre_input) - 1]);

                string user_input;

                do {
                    Console.Clear();
                    Console.WriteLine("Selected genres:");
                    foreach(string genre in selected_genres) {
                        Console.WriteLine(genre);
                    }

                    if (selected_genres.Any() == false){
                        Console.WriteLine("\nSelect more(S)\nStop selecting(Q)");
                    } else if (genres.Any() == false){
                        Console.WriteLine("\nDeselect(D)\nStop selecting(Q)");
                    } else {
                        Console.WriteLine("\nSelect more(S)\nDeselect(D)\nStop selecting(Q)");
                    }
                    user_input = Console.ReadLine().ToLower();

                    if((user_input == "d" && selected_genres.Any() == false ) || (user_input == "s" && genres.Any() == false)) {
                        user_input = default;
                    }

                    if(user_input == "q") {
                        end_genre_select = true;
                    } else if(user_input == "s") {
                        end_genre_select = false;
                    } else if (user_input == "d"){
                        bool end_genre_deselect = false;

                        while(end_genre_deselect == false ) {

                            genre_input = NavigationMenu.DisplayMenu(selected_genres, "Deselect genre:");

                            if(genre_input != null) {
                                genres.Add(selected_genres[Convert.ToInt32(genre_input) - 1]);
                                selected_genres.Remove(selected_genres[Convert.ToInt32(genre_input) - 1]);
                            }
                            
                            do {
                                Console.Clear();
                                Console.WriteLine("Selected genres:");
                                foreach(string genre in selected_genres) {
                                    Console.WriteLine(genre);
                                }
                                
                                if (selected_genres.Any() == true) {
                                    Console.WriteLine("\nDeselect more(D)\nStop deselecting(Q)");
                                    user_input = Console.ReadLine().ToLower();

                                    if (user_input == "d") {
                                        end_genre_deselect = false;
                                    } else if (user_input == "q") {
                                        end_genre_deselect = true;
                                    }
                                } else {
                                    end_genre_deselect = true;
                                    user_input = "q";
                                }

                            } while (user_input != "q" && user_input != "d");
                            user_input = "";
                        }
                    }

                } while (user_input != "q" && user_input != "s");
            } else {
                selected_genres = default;
                end_genre_select = true;
            }

        }

        if( selected_genres != default) {
            List<MovieModel> sorted_movies = MoviesLogic.SortMovies(selected_genres);

            Allmovies = sorted_movies;

            return sorted_movies;

        }

        return Allmovies;

    }

}