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
        _reservations = ReservationAccess.LoadAll();
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
        ReservationAccess.WriteAll(_reservations);

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

    public void AddNewReservation(int id, int showing_id, int account_id, List<string> seats, string unique_code)
    {
        ReservationModel newReservation = new ReservationModel(id, showing_id, account_id, seats, unique_code);
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

    public bool IsSoldOut(List<List<SeatModel>> hall)
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

    public static List<List<SeatModel>> GetEmptyHall(int which_hall) => HallAccess.LoadAll(which_hall);
}





