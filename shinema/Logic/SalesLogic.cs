using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

public static class SalesLogic
{
    private static int GetPriceForSeatHall(string seat, int hallNumber)
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

    
}