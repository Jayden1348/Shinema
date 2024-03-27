public static class HallLogic
{
    public static List<List<SeatModel>> ShowHall(ShowingModel show, ReservationLogic reservationLogic)
    {
        List<List<SeatModel>> hall = CreateMovieHall(show.RoomID);
        hall = reservationLogic.AddReservationsToHall(hall, show);

        int columns;
        int hallnumber = show.RoomID;

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
        { "001111111100",
          "011111111110",
          "011111111110",
          "111112211111",
          "111122221111",
          "111223322111",
          "111223322111",
          "111223322111",
          "111223322111",
          "111122221111",
          "111112211111",
          "011111111110",
          "001111111100",
          "001111111100" };

        List<string> hall2 = new()
        { "011111111111111110",
          "011111222222111110",
          "011112222222211110",
          "011112222222211110",
          "011122222222221110",
          "011122223322221110",
          "111222233332222111",
          "111222333333222111",
          "112222333333222211",
          "112222333333222211",
          "112222333333222211",
          "011222233332222110",
          "011122223322221110",
          "011112222222211110",
          "001111222222111100",
          "001111222222111100",
          "001111111111111100",
          "000111111111111000",
          "000111111111111000"
           };

        List<string> hall3 = new()
        { "000011111111111111111111110000",
          "000111111222222222222111111000",
          "000111112222222222222211111000",
          "000111112222222222222211111000",
          "000111122222233332222221111000",
          "001111122222333333222221111100",

          "011111222223333333322222111110",
          "111111222223333333322222111111",
          "111112222223333333322222211111",
          "111112222223333333322222211111",
          "111111222223333333322222111111",

          "111111122223333333322221111111",
          "011111112222233332222211111110",
          "001111112222222222222211111100",
          "001111111222222222222111111100",
          "000111111122222222221111111000",
          "000111111111222222111111111000",
          "000001111111111111111111100000",
          "000000011111111111111110000000",
          "000000001111111111111100000000"
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
                    rowlist.Add(new SeatModel(seat_rank, position));
                }
                else
                {
                    SeatModel empty = null;
                    rowlist.Add(empty);
                }
            }
            allseats.Add(rowlist);
        }
        return allseats;
    }

}
