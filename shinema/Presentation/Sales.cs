using System.Globalization;
public static class Sales
{
    public static void MainSalesInteraction()
    {
        List<string> mainMenuOptions = new List<string> { "Movie Sales", "Seat Sales", "Catering Sales", "Total Sales", "Quit"};
        bool MainSalesActive = true;
        while (MainSalesActive)
        {
            string mainMenuChoice = NavigationMenu.DisplayMenu(mainMenuOptions);
            string snackChoice = "0";
            if (mainMenuChoice == "5" || mainMenuChoice == null)
            {
                return;
            }
            if (mainMenuChoice == "3")
            {
                List<string> foodMenuOptions = new List<string> { "Snack Sales", "Drink Sales", "Quit" };
                snackChoice = NavigationMenu.DisplayMenu(foodMenuOptions);
                if (snackChoice == "3")
                {
                    return;
                }
            }
            //dates.Item1 is the startdate and dates.Item2 is the enddate
            (DateTime, DateTime) dates = EnterDateInteraction();
            Console.Clear();

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
                Console.Clear();
                if (snackChoice == "1")
                {
                    SnackSalesInteraction<FoodModel>(dates.Item1, dates.Item2);
                    NavigationMenu.AwaitKey();
                }
                else if (snackChoice == "2")
                {
                    SnackSalesInteraction<DrinkModel>(dates.Item1, dates.Item2);
                    NavigationMenu.AwaitKey();
                }
            }
            else if (mainMenuChoice == "4")
            {
                TotalSalesInteraction(dates.Item1, dates.Item2);
                NavigationMenu.AwaitKey();
            }
            
        }
    }
     public static (DateTime, DateTime) EnterDateInteraction()
     {
        // returns a tuple of startdate and enddate
        // this function gets the start and enddate from the user

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
                        // try to parse date to DD-MM-YYYY format
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

                
                if (startDate > endDate && endDate != default)
                {
                    Console.Clear();
                    Console.WriteLine("start date is later than end date");
                    validStartDate = false;
                    validEndDate = false;
                }
                else
                {
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
        
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        // call functions that return a string for display
        Console.WriteLine(SalesLogic.GetTurnOverMovies(selectedMovieMenuChoice, movieList, startDate, endDate));

    }
    public static void SeatSalesInteraction(DateTime startDate, DateTime endDate)
    {
        string salesString = SalesLogic.GetAmountOfSeatsBooked(startDate, endDate);
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine(salesString);
    }
    public static void TotalSalesInteraction(DateTime startDate, DateTime endDate)
    {
        string totalSalesString = SalesLogic.GetTotalTurnOver(startDate, endDate);
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine(totalSalesString);
    }

    public static void SnackSalesInteraction<T>(DateTime startDate, DateTime endDate) where T: Consumable
    {
        
        string totalSnackString = SalesLogic.GetConsumableSales<T>(startDate, endDate);
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine(totalSnackString);
    }

}