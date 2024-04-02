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

    public bool AddNewReservation(int id, int showing_id, int account_id, List<string> seats, string unique_code, bool test1)
    {
        if (test1)
        {
            ReservationModel newReservation = new ReservationModel(id, showing_id, account_id, seats, unique_code);
            UpdateReservation(newReservation);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool ValidateAndReserveSeats(List<string> inputseats, List<List<SeatModel>> moviehall)
    {
        foreach (string reserve_position in inputseats)
        {
            if (reserve_position.Count() == 2 || reserve_position.Count() == 3)
            {
                if (char.IsLetter(reserve_position, 0) && char.IsNumber(reserve_position, 1))
                {

                }
                else
                {
                    SeatReservation.WrongInput(2, reserve_position);
                    return false;
                }
            }
            else
            {
                SeatReservation.WrongInput(1, reserve_position);
                return false;
            }

        }
        // At this point all positions given by the user are correct, now checking if they are also available
        foreach (string reserve_position in inputseats)
        {
            bool found = false;
            foreach (List<SeatModel> row in moviehall)
            {
                foreach (SeatModel seat in row)
                {
                    if (seat == null) { }
                    else if (seat.Position == reserve_position)
                    {
                        found = true;
                        if (seat.Available)
                        {
                            seat.Available = false;
                        }
                        else
                        {
                            SeatReservation.WrongInput(3, reserve_position);
                            return false;
                        }

                    }

                }
            }
            if (!found)
            {
                SeatReservation.WrongInput(4, reserve_position);
                return false;
            }
        }
        return true;
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
        string code = "";
        Random rand = new();
        for (int i = 0; i < 15; i++)
        {
            char next_char = allchars[rand.Next(allchars.Count())];
            code += next_char;
        }
        return code;
    }

    public bool ShoppingCart(){
        string user_input;

        do {
            user_input = Console.ReadLine();

        } while(user_input != "1" && user_input != "2");

        switch(user_input){
            case "1":
                return true;
            case "2":
                return false;
            default:
                return false;
        }
    }

    public string ReservationOverview(List<string> seats) {
        string line = "Shopping cart:\n";
        
        foreach(string seat in seats) {
            line += $"Seat: {seat}\n";
        }
        
        line += "\n1. Confirm order\n";
        line += "2. Cancel order\n";

        return line;
    }

}
