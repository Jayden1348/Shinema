using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;

public static class SalesLogic
{
    public static int GetSeatRank(string seat, int hallNumber)
    {
        List<List<SeatModel>> hall = GenericAccess<SeatModel>.LoadAll(hallNumber);
        char rowChar = seat[0];

        //Converts letter to ascii value
        int asciiValueSeat = rowChar;

        //row Int is the position in the alphabet -1
        //"A" becomes 0 and "B" becomes 1...
        int rowInt = asciiValueSeat - 65;

        List<SeatModel> hallSeats = hall[rowInt];
        int seatRank = hallSeats.Find(seatModel => seatModel is not null && seatModel.Position == seat).Rank;

        return seatRank;
    }

    public static string GetAmountOfSeatsBooked(DateTime startDate, DateTime endDate)
    {
        //creating first line
        string returnString = "";
        if (startDate != default && endDate != default)
        {
            returnString += $"Sales between {startDate.ToString("dd-MM-yyyy")} and {endDate.ToString("dd-MM-yyyy")}\n";
        }
        else if (startDate == default && endDate != default)
        {
            returnString += $"Sales before {endDate.ToString("dd-MM-yyyy")}\n";
        }
        else if (endDate == default && startDate != default)
        {
            returnString += $"Sales after {startDate.ToString("dd-MM-yyyy")}\n";
        }

        // dictionary to keep track off seats booked per rank
        Dictionary<int, int> seatRankBooked = new Dictionary<int, int> { { 1, 0 }, { 2, 0 }, { 3, 0 } };

        // filtered reservation list based on dates
        List<ReservationModel> filteredReservations = GetReservationsListBasedOnDate(startDate, endDate);
        foreach (ReservationModel reservation in filteredReservations)
        {
            ShowingModel showing = GenericAccess<ShowingModel>.LoadAll().Find(showing => showing.ID == reservation.Showing_ID);
            foreach (string seat in reservation.Seats)
            {
                int seatRank = GetSeatRank(seat, showing.RoomID);
                seatRankBooked[seatRank]++;
            }
        }
        returnString += $"Rank 1 Seats Booked: {seatRankBooked[1]}; total turnover for Rank 1: \u20AC{seatRankBooked[1] * 15.00}\n" +
                              $"Rank 2 Seats Booked: {seatRankBooked[2]}; total turnover for Rank 2: \u20AC{seatRankBooked[2] * 12.50}\n" +
                              $"Rank 3 Seats Booked: {seatRankBooked[3]}; total turnover for Rank 3: \u20AC{seatRankBooked[3] * 10.00}\n";
        return returnString;
    }

    public static string GetTurnOverForSingleMovie(int movieItemIndex, List<MovieModel> allMovies, DateTime startDate, DateTime endDate)
    {
        Console.Clear();
        // int movieItemIndex = Convert.ToInt16(NavigationMenu.DisplayMenu(allMovies)) -1;

        //get movie id from selected movie
        int movieID = allMovies[movieItemIndex].ID;
        List<ShowingModel> allShowings = new ShowingsLogic().GetAllShowings();


        //movie id -> showing id -> reservation price
        //getting int list of showing ids where the movie id is the selected movie
        List<int> showingIDsWithMovie = allShowings.Where(showing => showing.MovieID == movieID).Select(showing => showing.ID).ToList();

        //getting reservations price from reservations using showing id
        List<ReservationModel> reservations = GetReservationsListBasedOnDate(startDate, endDate);

        //    ↓ create a list with reservations that has theshowingIDsWithMovie 
        double movieTurnOver = reservations.Where(reservation => showingIDsWithMovie.Contains(reservation.Showing_ID))
                                           //    ↓ select only the price from reservations
                                           .Select(reservations => reservations.Price)
                                           //    ↓ get the total for turnover for selected movie
                                           .Sum();

        string returnString = $"Total turn over for {allMovies[movieItemIndex]}: \u20AC{movieTurnOver}";

        return returnString;
    }

    public static string GetTurnOverMovies(int selectedTurnOverMovie, List<MovieModel> movieList, DateTime startDate, DateTime endDate)
    {
        //creating first line off return string with dates
        string returnString = "";
        if (startDate != default && endDate != default)
        {
            returnString += $"Sales between {startDate.ToString("dd-MM-yyyy")} and {endDate.ToString("dd-MM-yyyy")}\n";
        }
        else if (startDate == default && endDate != default)
        {
            returnString += $"Sales before {endDate.ToString("dd-MM-yyyy")}\n";
        }
        else if (endDate == default && startDate != default)
        {
            returnString += $"Sales after {startDate.ToString("dd-MM-yyyy")}\n";
        }

        if (selectedTurnOverMovie == 0)
        {

            for (int movieindex = 0; movieindex < movieList.Count; movieindex++)
            {
                returnString += GetTurnOverForSingleMovie(movieindex, movieList, startDate, endDate);
                returnString += "\n";
            }
            return returnString;
        }
        selectedTurnOverMovie--;
        returnString += GetTurnOverForSingleMovie(selectedTurnOverMovie, movieList, startDate, endDate);
        return returnString;
    }

    public static List<ReservationModel> GetReservationsListBasedOnDate(DateTime startDate, DateTime endDate)
    {
        List<ReservationModel> allReservations = new ReservationLogic().GetAllReservations();
        List<ShowingModel> showingList = new ShowingsLogic().GetAllShowings();
        List<int> filteredIntList;
        if (startDate == default && endDate == default)
        {

            // get full showing list
            return allReservations;
        }
        else if (startDate == default)
        {

            // get showing list that is before the end date
            filteredIntList = showingList.Where(showing => showing.Datetime < endDate)
                                         .Select(showing => showing.ID)
                                         .ToList();
        }
        else if (endDate == default)
        {

            // get list of showing ids that is before the after the start date
            filteredIntList = showingList.Where(showing => showing.Datetime > startDate)
                                         .Select(showing => showing.ID)
                                         .ToList();

        }
        else
        {

            //get showing list between the dates
            filteredIntList = showingList.Where(showing => showing.Datetime > startDate && showing.Datetime < endDate).Select(showing => showing.ID).ToList();
            // if (filteredIntList.Count == 0)
            // {
            //     return default;
            // }
        }
        List<ReservationModel> filteredReservations = allReservations.Where(reservation => filteredIntList.Contains(reservation.Showing_ID)).ToList();
        return filteredReservations;
    }
    public static string GetTotalTurnOver(DateTime startDate, DateTime endDate)
    {
        List<ReservationModel> reservations = GetReservationsListBasedOnDate(startDate, endDate);
        double totalTurnOver = 0.0;
        foreach (ReservationModel reservation in reservations)
        {
            totalTurnOver += reservation.Price;
        }


        string returnString = $"The Cinema has made \u20AC{totalTurnOver} in total";
        if (startDate != default && endDate != default)
        {
            returnString += $" between {startDate.ToString("dd-MM-yyyy")} and {endDate.ToString("dd-MM-yyyy")}";
        }
        else if (startDate == default && endDate != default)
        {
            returnString += $" before {endDate.ToString("dd-MM-yyyy")}\n";
        }
        else if (endDate == default && startDate != default)
        {
            returnString += $" since {startDate.ToString("dd-MM-yyyy")}";
        }
        return returnString;
    }

}