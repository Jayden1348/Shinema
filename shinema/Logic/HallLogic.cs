public static class HallLogic
{

    private static List<SeatModel> _seats;

    static HallLogic()
    {
        _seats = SeatsAccess.LoadAll();
    }
    public static List<List<SeatModel>> ShowHall(ShowingModel show, ReservationLogic reservationLogic)
    {
        List<List<SeatModel>> hall = CreateMovieHall(show.HallID);
        hall = reservationLogic.AddReservationsToHall(hall, show);

        int columns;
        int hallnumber = show.HallID;

        switch (hallnumber) // Default is hall 1
        {
            case 1:
                columns = 12;
                break;
            case 2:
                columns = 18;
                break;
            case 3:
                columns = 30;
                break;
            default:
                columns = 12;
                break;
        }
        SeatReservation.ShowGrid(columns, hall);
        return hall;
    }
    private static List<List<SeatModel>> CreateMovieHall(int which_hall)
    {
        List<string> hall1 = new()


        { "003333333300",
          "033333333330",
          "033333333330",
          "333332233333",
          "333322223333",
          "333221122333",
          "333221122333",
          "333221122333",
          "333221122333",
          "333322223333",
          "333332233333",
          "033333333330",
          "003333333300",
          "003333333300" };
        



        List<string> hall2 = new()
        { "033333333333333330",
          "033333222222333330",
          "033332222222233330",
          "033332222222233330",
          "033322222222223330",
          "033322221122223330",
          "333222211112222333",
          "333222111111222333",
          "332222111111222233",
          "332222111111222233",
          "332222111111222233",
          "033222211112222330",
          "033322221122223330",
          "033332222222233330",
          "003333222222333300",
          "003333222222333300",
          "003333333333333300",
          "000333333333333000",
          "000333333333333000"
           };

        List<string> hall3 = new()
        { "000033333333333333333333330000",
          "000333333222222222222333333000",
          "000333332222222222222233333000",
          "000333332222222222222233333000",
          "000333322222211112222223333000",
          "003333322222111111222223333300",

          "033333222221111111122222333330",
          "333333222221111111122222333333",
          "333332222221111111122222233333",
          "333332222221111111122222233333",
          "333333222221111111122222333333",

          "333333322221111111122223333333",
          "033333332222211112222233333330",
          "003333332222222222222233333300",
          "003333333222222222222333333300",
          "000333333322222222223333333000",
          "000333333333222222333333333000",
          "000003333333333333333333300000",
          "000000033333333333333330000000",
          "000000003333333333333300000000"
        };

        List<List<string>> all_halls = new() { hall1, hall2, hall3 };
        List<string> hall = all_halls[which_hall - 1];

        string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        int row = 0;
        int column;

        List<List<SeatModel>> allseats = new();
        foreach (string seatrow in hall)
        {
            column = 0;
            row++;
            List<SeatModel> rowlist = new();
            foreach (char seat in seatrow)
            {
                column++;
                int seat_rank = Convert.ToInt32(seat.ToString());
                if (seat_rank != 0)
                {
                    string position = $"{letters[row - 1]}{column}";
                    int seatID;

                    var lastSeat = _seats.LastOrDefault(seat => seat != null);
                    if (lastSeat != null) {
                        seatID = lastSeat.ID + 1;
                    } else {
                        seatID = 1;
                    }

                    SeatModel newSeat = new SeatModel(seatID, seat_rank, which_hall, position);
                    _seats.Add(newSeat);
                    rowlist.Add(newSeat);
                }
                else
                {
                    SeatModel empty = null;
                    rowlist.Add(empty);
                }
            }
            allseats.Add(rowlist);


        }
        
    
        if (_seats.Count() == 0) {
            SeatsAccess.WriteAll(_seats);
        } else {
            List<SeatModel> seats = new List<SeatModel>();
        
            foreach(SeatModel ListSeat in _seats) {
                if (!seats.Exists(seat => seat.HallID == ListSeat.HallID && seat.Position == ListSeat.Position)) {
                    seats.Add(ListSeat);
                }
            }
            SeatsAccess.WriteAll(seats);

        }
       

        return allseats;
    }

}
