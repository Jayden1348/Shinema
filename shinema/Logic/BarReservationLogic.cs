public class BarReservationLogic
{
    private List<BarReservationModel>? _barreservations;

    public BarReservationLogic()
    {
        _barreservations = BarReservationAccess.GetAllBarReservations();
    }

    public BarReservationLogic(AccountModel user)
    {
        _barreservations = new List<BarReservationModel>() { };
        foreach (BarReservationModel r in BarReservationAccess.GetAllBarReservations())
        {
            if (r.Account_ID == user.Id)
            {
                _barreservations.Add(r);
            }
        }
        _barreservations.Sort();
    }


    public int FindByUniqueCode(string code)
    {
        BarReservationModel b = _barreservations.Find(i => i.Unique_code == code);
        if (b == null) return 0;
        return b.Number_of_seats;
    }
    public int CheckBarAvailability(DateTime currentDate)
    {
        //this function returns how many open bar seats there are at the given date


        //sets current open seats to the bar capacity which is located i BarReservationModel
        int currentOpenSeats = BarReservationModel.BarCapacity;
        DateTime currentReservationEnd = currentDate.AddHours(3);
        foreach (BarReservationModel barReservation in _barreservations)
        {

            //checks for each bar reservation if there is an overlapping date
            //check criteria
            //if date is larger than barReservation.Date from the json file this checks if the date is higher than the lower limit
            //if date is smaller than barReservation.Date + the hours that you reserve from the json file this checks if the date is lower than the upper limit
            //this creates a span of time in which the new reservation is supposed to take place, if the new date is in between the timespan the available seat counter gets decreased by the amount of seats that are reserved
            DateTime beginReservation = barReservation.Date;
            DateTime endReservation = barReservation.Date.AddHours(BarReservationModel.BarTimeReserve);

            //if date is equal to barReservation.Date
            // Console.WriteLine($"{(currentDate < endReservation && currentDate > beginReservation)}; {(currentReservationEnd < endReservation && currentReservationEnd > beginReservation)}");
            // Console.WriteLine($"({currentDate} < {endReservation} && {currentDate} > {beginReservation}); ({currentReservationEnd} < {endReservation} && {currentReservationEnd} > {beginReservation})");


            if (currentDate == beginReservation || (currentDate < endReservation && currentDate > beginReservation) || (currentReservationEnd < endReservation && currentReservationEnd > beginReservation))
            {
                currentOpenSeats -= barReservation.Number_of_seats;
            }
        }
        return currentOpenSeats;
    }
    public void ReserveBarSeats(int id, string reservationCode, DateTime date, int numberOfReservations)
    {
        AddOneItem(new BarReservationModel(id, reservationCode, date, numberOfReservations));
    }

    public void RemoveBarSeatReservation(string reservationCode)
    {
        _barreservations.RemoveAll(r => r.Unique_code == reservationCode);
        BarReservationAccess.WriteAllBarReservations(_barreservations);
    }

    public void RemoveBarSeatReservation(int id)
    {
        foreach (BarReservationModel reservation in _barreservations)
        {
            if (reservation.Account_ID == id)
            {
                _barreservations.Remove(reservation);
            }
        }
        BarReservationAccess.WriteAllBarReservations(_barreservations);
    }

    public void RemoveBarSeatReservation(List<string> reservationCodes)
    {
        if (reservationCodes is null)
        {
            return;
        }

        foreach (string code in reservationCodes)
        {
            RemoveBarSeatReservation(code);
        }

    }

    public void AddOneItem(BarReservationModel barReservation)
    {
        _barreservations.Add(barReservation);
        BarReservationAccess.WriteAllBarReservations(_barreservations);
    }

    public BarReservationModel FindBarReservationUsingCode(string reservationCode)
    {
        foreach (BarReservationModel reservation in _barreservations)
        {
            if (reservation.Unique_code == reservationCode)
            {
                return reservation;
            }
        }
        return null;
    }

    public List<BarReservationModel> FindBarReservationUsingID(int id)
    {
        List<BarReservationModel> userReservations = new List<BarReservationModel>();
        foreach (BarReservationModel reservation in _barreservations)
        {
            if (reservation.Account_ID == id)
            {
                userReservations.Add(reservation);
            }
        }
        return userReservations;
    }

    public void DisplayReservations()
    {
        if (_barreservations == null)
        {
            MyReservations<BarReservationModel>.PrintReservation(null);
        }
        else
        {
            int index_of_delete = MyReservations<BarReservationModel>.PrintReservation(_barreservations) - 1;
            if (index_of_delete != -1)
            {
                RemoveBarSeatReservation(_barreservations[index_of_delete].Unique_code);
            }
        }
    }

}