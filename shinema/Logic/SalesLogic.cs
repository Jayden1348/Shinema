using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;

public static class SalesLogic
{
    private static int GetPriceForSeatHall(string seat, int hallNumber )
    {
        List<List<SeatModel>> hall = HallAccess.LoadAll(hallNumber);
        char rowChar = seat[0];

        //Converts letter to ascii value
        int asciiValueSeat = (int)rowChar;

        //row Int is the position in the alphabet -1
        //"A" becomes 0 and "B" becomes 1...
        int rowInt = asciiValueSeat - 65;

        List<SeatModel> hallSeats = hall[rowInt];
        int seatRank = hallSeats.Find(seatModel => seatModel is not null && seatModel.Position == seat).Rank;
        
        return seatRank;
    }

    public static string GetAmountOfSeatsBooked()
    {
        Dictionary<int, int> seatRankBooked = new Dictionary<int, int> { { 1, 0 }, { 2, 0 }, { 3, 0 } };
        foreach(ReservationModel reservation in ReservationAccess.LoadAll())
        {
            ShowingModel showing = ShowingsAccess.LoadAll().Find(showing => showing.ID == reservation.Showing_ID);
            foreach( string seat in reservation.Seats)
            {
                int seatRank = GetPriceForSeatHall(seat, showing.RoomID);
                seatRankBooked[seatRank]++;
            }
        }
        string returnString = $"Rank 1 Seats Booked: {seatRankBooked[1]} total turnover for seat 1 {seatRankBooked[1]* 15.00}\n" +
                              $"Rank 2 Seats Booked: {seatRankBooked[2]} total turnover for seat 2 {seatRankBooked[2]* 12.50 }\n" +
                              $"Rank 3 Seats Booked: {seatRankBooked[3]} total turnover for seat 3 {seatRankBooked[3]* 10.00}\n";
        return returnString;
    }

    public static string GetTurnoverPerMovie()
    {
        Console.Clear();
        List<MovieModel> allMovies = MoviesLogic.GetAllMovies();
        int movieItemInt = Convert.ToInt16(NavigationMenu.DisplayMenu(allMovies)) -1;

        //get movie id from selected movie
        int movieID = allMovies[movieItemInt].ID;
        List<ShowingModel> allShowings = new ShowingsLogic().GetAllShowings();
        

        //movie id -> showing id -> reservation price
        //getting showing model list where the movie id is the selected movie
        List<int> showingIDsWithMovie = allShowings.Where(showing => showing.MovieID == movieID).Select(showing => showing.ID).ToList();

        //getting reservations price from reservations using showing id
        List<ReservationModel> reservations = new ReservationLogic().GetAllReservations();

                                        //    ↓ create a list with reservations that has theshowingIDsWithMovie 
        double movieTurnOver = reservations.Where(reservation => showingIDsWithMovie.Contains(reservation.Showing_ID)) 
                                        //    ↓ select only the price from reservations
                                           .Select(reservations => reservations.Price)
                                        //    ↓ get the total for turnover for selected movie
                                           .Sum();
        
        string returnString = $"Total turn over for {allMovies[movieItemInt]}: {movieTurnOver} Euro";
        
        return returnString;
    }
}