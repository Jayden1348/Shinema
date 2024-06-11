using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;


//This class is not static so later on we can use inheritance and interfaces
public class ReservationLogic
{
    private static List<ReservationModel>? _reservations;

    public ReservationLogic()
    {
        _reservations = GenericAccess<ReservationModel>.LoadAll();
    }
    public ReservationLogic(AccountModel user)
    {
        _reservations = new List<ReservationModel> { };
        foreach (ReservationModel r in GenericAccess<ReservationModel>.LoadAll())
        {
            if (r.Account_ID == user.Id)
            {
                _reservations.Add(r);
            }
        }
        _reservations.Sort();
    }

    public static void DeleteReservation(ReservationModel reservation)
    {
        _reservations.Remove(reservation);
        GenericAccess<ReservationModel>.WriteAll(_reservations);
    }

    public void UpdateReservation(ReservationModel res)
    {
        //Find if there is already an model with the same id
        int index = _reservations.FindIndex(s => s.Id == res.Id);

        if (index != -1)
        {
            //update existing model
            _reservations[index] = res;
        }
        else
        {
            //add new model
            _reservations.Add(res);
        }
        GenericAccess<ReservationModel>.WriteAll(_reservations);

    }

    public ReservationModel GetById(int id)
    {
        return _reservations.Find(i => i.Id == id);
    }

    public int GetNextId()
    {
        if (_reservations.Count != 0)
        {
            int maxId = _reservations.Max(account => account.Id);
            return maxId + 1;
        }
        else { return 1; }
    }

    public void AddNewReservation(int id, int showing_id, int account_id, List<string> seats, double price, string unique_code, Dictionary<int, int> snacks, Dictionary<int, int> drinks)
    {
        ReservationModel newReservation = new ReservationModel(id, showing_id, account_id, seats, price, unique_code, snacks, drinks);
        UpdateReservation(newReservation);
    }

    public List<List<SeatModel>> AddReservationsToHall(List<List<SeatModel>> moviehall, ShowingModel show)
    {
        foreach (ReservationModel reservation in _reservations)
        {
            if (reservation.Showing_ID == show.ID)
            {
                foreach (string position in reservation.Seats)
                {
                    foreach (List<SeatModel> row in moviehall)
                    {
                        foreach (SeatModel seat in row)
                        {
                            if (seat != null && seat.Position == position)
                            {
                                seat.Available = false;
                            }

                        }

                    }
                }
            }
        }
        return moviehall;
    }

    public string GenerateRandomString()
    {
        string allchars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
        Random rand = new();
        bool really_unique = true;
        string code = "";
        while (really_unique)
        {
            code = "";
            for (int i = 0; i < 15; i++)
            {
                char next_char = allchars[rand.Next(allchars.Count())];
                code += next_char;
            }
            // Check if random string already exists
            foreach (ReservationModel reservation in _reservations)
            {
                if (reservation.Unique_code == code)
                {
                    really_unique = false;
                }
            }
            if (really_unique)
            {
                really_unique = false;
            }
            else
            {
                really_unique = true;
            }
        }
        return code;
    }

    public void DisplayReservations()
    {
        if (_reservations == null)
        {
            MyReservations<ReservationModel>.PrintReservation(null);
        }
        else
        {
            int index_of_delete = MyReservations<ReservationModel>.PrintReservation(_reservations) - 1;
            if (index_of_delete != -1)
            {
                DeleteReservation(_reservations[index_of_delete]);
            }
        }
    }
    public static bool IsSoldOut(List<List<SeatModel>> hall)
    {
        foreach (List<SeatModel> row in hall)
        {
            foreach (SeatModel seat in row)
            {
                if (seat != null && seat.Available == true)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public static List<List<SeatModel>> GetEmptyHall(int which_hall) => GenericAccess<SeatModel>.LoadAll(which_hall);

    public static List<List<SeatModel>> CreateMovieHall(int which_hall)
    {
        List<string> hall1 = new()
        {
            "003333333300",
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
            "003333333300"
        };

        List<string> hall2 = new()
        {
            "033333333333333330",
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
        {
            "000033333333333333333333330000",
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

    public bool CheckReservationExist(int movieid)
    {
        if (_reservations is null)
        {
            return false;
        }

        ShowingsLogic showingsLogic = new();
        List<int> showings = showingsLogic.GetShowingID(movieid);

        foreach (ReservationModel reservation in _reservations)
        {
            if (showings.Contains(reservation.Showing_ID))
            {
                return true;
            }
        }
        return false;
    }

    public void DeleteReservation(List<int> showingID)
    {
        List<ReservationModel> newReservations = new();
        if (_reservations is null) { return; }

        foreach (ReservationModel reservation in _reservations)
        {
            if (!showingID.Contains(reservation.Showing_ID))
            {
                newReservations.Add(reservation);
            }
        }
        _reservations = newReservations;
        GenericAccess<ReservationModel>.WriteAll(_reservations);
    }

    public List<ReservationModel> GetAllReservations()
    {
        if (_reservations is null) { return null; }

        return _reservations;
    }

    public List<string> GetReservationCodes(List<int> showingIDs)
    {
        if (_reservations is null)
        {
            return new List<string>();
        }

        return _reservations
            .Where(reservation => showingIDs.Contains(reservation.Showing_ID))
            .Select(reservation => reservation.Unique_code)
            .ToList();
    }
    public List<ReservationModel> GetReservationsByShowingIDs(List<int> showingIDs)
    {
        if (_reservations is null)
        {
            return null;
        }

        return _reservations
            .Where(reservation => showingIDs.Contains(reservation.Showing_ID))
            .OrderBy(reservation => reservation.Showing_ID)
            .ToList();
    }

    public List<ReservationModel> GetReservationsByShowingID(int showingID)
    {
        if (_reservations is null)
        {
            return null;
        }

        return _reservations
            .Where(reservation => reservation.Showing_ID == showingID)
            .OrderBy(reservation => reservation.Showing_ID)
            .ToList();
    }

    public List<ReservationModel> GetAllReservationsSorted()
    {
        if (_reservations is null)
        {
            return null;
        }

        return _reservations.OrderBy(r => r.Showing_ID).ToList();
    }
}