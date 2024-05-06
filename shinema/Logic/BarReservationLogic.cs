public static class BarReservationLogic
{
    public static int CheckBarAvailability(DateTime date)
    {
        //this function returns how many open bar seats there are at the given date


        List<BarReservationModel> barReservations = BarReservationAccess.GetAllBarReservations();

        //sets current open seats to the bar capacity which is located i BarReservationModel
        int currentOpenSeats = BarReservationModel.BarCapacity;

        foreach (BarReservationModel barReservation in barReservations)
        {

            //checks for each bar reservation if there is an overlapping date
            //check criteria
            //if date is larger than barReservation.Date from the json file this checks if the date is higher than the lower limit
            //if date is smaller than barReservation.Date + the hours that you reserve from the json file this checks if the date is lower than the upper limit
            //this creates a span of time in which the new reservation is supposed to take place, if the new date is in between the timespan the available seat counter gets decreased by the amount of seats that are reserved

            //if date is equal to barReservation.Date
            if (barReservation.Date < date && barReservation.Date.AddHours(BarReservationModel.BarTimeReserve) > date || barReservation.Date == date)
            {
                currentOpenSeats -= barReservation.BarReservationAmount;
            }
        }
        return currentOpenSeats;
    }

    public static void ReserveBarSeats(DateTime date, int numberOfReservations, string reservationCode, int id)
    {
        AddOneItem(new BarReservationModel(id, reservationCode, date, numberOfReservations));
    }

    public static void RemoveBarSeatReservation(string reservationCode)
    {
        List<BarReservationModel> barReservations = BarReservationAccess.GetAllBarReservations();
        List<BarReservationModel> newBarReservations = new List<BarReservationModel>();
        foreach(BarReservationModel reservation in barReservations)
        {
            if (reservation.UniqueCode != reservationCode)
            {
                newBarReservations.Add(reservation);
            }
        }

        BarReservationAccess.WriteAllBarReservations(newBarReservations);
    }

    public static void RemoveBarSeatReservation(int id)
    {
        List<BarReservationModel> barReservations = BarReservationAccess.GetAllBarReservations();
        foreach(BarReservationModel reservation in barReservations)
        {
            if (reservation.UserID == id)
            {
                barReservations.Remove(reservation);
            }
        }
        BarReservationAccess.WriteAllBarReservations(barReservations);
    }

    public static void AddOneItem(BarReservationModel barReservation)
    {
        List<BarReservationModel> barReservations = BarReservationAccess.GetAllBarReservations();
        barReservations.Add(barReservation);
        BarReservationAccess.WriteAllBarReservations(barReservations);
    }

    public static BarReservationModel FindBarReservationUsingCode(string reservationCode)
    {
        List<BarReservationModel> reservations = BarReservationAccess.GetAllBarReservations();
        foreach (BarReservationModel reservation in reservations)
        {
            if (reservation.UniqueCode == reservationCode)
            {
                return reservation;
            }
        }
        return null;
    }

    public static List<BarReservationModel> FindBarReservationUsingID(int id)
    {
        List<BarReservationModel> reservations = BarReservationAccess.GetAllBarReservations();
        List<BarReservationModel> userReservations = new List<BarReservationModel>();
        foreach (BarReservationModel reservation in reservations)
        {
            if (reservation.UserID == id)
            {
                userReservations.Add(reservation);
            }
        }
        return userReservations;
    }
}