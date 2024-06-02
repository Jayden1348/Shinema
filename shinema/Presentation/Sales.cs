using System.Globalization;
public static class Sales
{
    public static void MainSalesInteraction()
    {
        List<string> mainMenuOptions = new List<string> { "Movie Sales", "Seat Sales", "Food Sales", "Quit"};
        bool MainSalesActive = true;
        while (MainSalesActive)
        {
            string mainMenuChoice = NavigationMenu.DisplayMenu(mainMenuOptions);

            if (mainMenuChoice == "4")
            {
                return;
            }
            //dates.Item1 is the startdate and dates.Item2 is the enddate
            (DateTime, DateTime) dates = EnterDateInteraction();

            if (mainMenuChoice == "1")
            {
                MovieSalesInteraction(dates.Item1, dates.Item2);
                NavigationMenu.AwaitKey();
            }
            else if (mainMenuChoice == "2")
            {
                SeatSalesInteraction(dates.Item1, dates.Item2);
                NavigationMenu.AwaitKey();
            }
            else if (mainMenuChoice == "3")
            {

            }
            
        }
    }
     public static (DateTime, DateTime) EnterDateInteraction()
     {
        List<string> yesNoOptions = new List<string> { "Yes", "No" };
        string yesNoFilter = NavigationMenu.DisplayMenu(yesNoOptions, "Would you like to filter based on date");
        Console.Clear();
        DateTime startDate = default;
        DateTime endDate = default;
        
        if (yesNoFilter == "1")
        {
            bool validDates = false; // stop the outer loop when both dates are valid
            while (!validDates)
            {
                
                // stops the inner loop when start date could be parsed to datetime
                bool validStartDate = false;
                while (!validStartDate)
                {
                    Console.WriteLine();
                    Console.WriteLine("Enter start date (format: DD-MM-YYYY)");
                    Console.WriteLine("If you don't want a start date enter '0'");
                    string enteredStartDate = Console.ReadLine();
                    if (enteredStartDate == "0")
                    {
                        validStartDate = true;
                        startDate = default;
                    }
                    else
                    {
                        CultureInfo provider = new CultureInfo("nl-NL");
                        validStartDate = DateTime.TryParseExact(enteredStartDate, "d-M-yyyy", provider, DateTimeStyles.None, out startDate);

                        if (!validStartDate)
                        {
                            Console.Clear();
                            Console.WriteLine("Incorrect Input");
                        }
                    }
                }


                // stops the inner loop when end date could be parsed to datetime
                bool validEndDate = false;
                while (!validEndDate)
                {
                    Console.WriteLine();
                    Console.WriteLine("Enter end date (format: DD-MM-YYYY)");
                    Console.WriteLine("If you don't want a end date enter '0'");
                    string enteredEndDate = Console.ReadLine();
                    if (enteredEndDate == "0")
                    {
                        validEndDate = true;
                        endDate = default;
                    }
                    else
                    {
                        CultureInfo provider = new CultureInfo("nl-NL");
                        validEndDate = DateTime.TryParseExact(enteredEndDate, "d-M-yyyy", provider, DateTimeStyles.None, out endDate);
                        endDate = endDate.AddHours(23).AddMinutes(59).AddSeconds(59);
                        if (!validEndDate)
                        {
                            Console.WriteLine("Incorrect Input");
                        }
                    }
                }

                
                if (startDate > endDate)
                {
                    Console.Clear();
                    Console.WriteLine("start date is later than end date");
                    validStartDate = false;
                    validEndDate = false;
                }
                else
                {
                    Console.WriteLine($"Dates {startDate}, {endDate}");
                    validDates = true;
                    return (startDate, endDate);
                }
            }
        }
        return (default, default);
     }
    public static void MovieSalesInteraction(DateTime startDate, DateTime endDate)
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
        Console.WriteLine(SalesLogic.GetTurnOverMovies(selectedMovieMenuChoice, movieList, startDate, endDate));

    }
    public static void SeatSalesInteraction(DateTime startDate, DateTime endDate)
    {
        string salesString = SalesLogic.GetAmountOfSeatsBooked(startDate, endDate);
        Console.WriteLine(salesString);
    }
}