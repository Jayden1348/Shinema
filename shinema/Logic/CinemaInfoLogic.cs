using System.Net.Sockets;

public static class CinemaInfoLogic
{
    public static string GetCinemaInfo()
    {
        CinemaInformationModel cinemaInfo = GenericAccess<CinemaInformationModel>.LoadAll().First();
        // CinemaInformationModel cinemaInfo = CinemaInformationAccess.LoadInfo();

        return @$"The Cinema is located at {cinemaInfo.Address} {cinemaInfo.City}.
It Opens at {cinemaInfo.OpeningTime} and closes at {cinemaInfo.ClosingTime}

Cinema Contact Info: 
Phone Number: {cinemaInfo.PhoneNumber}
E-mail: {cinemaInfo.Email}
";
    }

    public static CinemaInformationModel GetCinemaInfoObject()
    {
        return GenericAccess<CinemaInformationModel>.LoadAll().FirstOrDefault();
    }

    public static string GetCinemaInfo(CinemaInformationModel cinemaInformationObject)
    {
        return @$"The Cinema is located at {cinemaInformationObject.Address} {cinemaInformationObject.City}.
It Opens at {cinemaInformationObject.OpeningTime} and closes at {cinemaInformationObject.ClosingTime}

Cinema Contact Info: 
Phone Number: {cinemaInformationObject.PhoneNumber}
E-mail: {cinemaInformationObject.Email}
";
    }

    public static void SaveCinemaInfo(CinemaInformationModel newCinemaInfoObject)
    {
        GenericAccess<CinemaInformationModel>.WriteAll(new List<CinemaInformationModel> { newCinemaInfoObject });
    }

    public static int CheckTimeValidity(string time)
    {
        // returns int based on validity check
        // returns -4 if ":" is not in time string
        // returns -3 if hours is not an integer
        // returns -2 if minutes is not an integer
        // returns -1 if hours is out of range (higher than 24 or lower than 0)
        // returns 0 if minutes is out of range (higher than 59 or lower than 0)
        // returns 1 if time string is correct

        if (!time.Contains(":"))
        {
            return -4;
        }

        string[] splitTime = time.Split(":");

        int hours;
        int minutes;

        bool isValidIntHours = int.TryParse(splitTime[0], out hours);
        bool isValidIntMinutes = int.TryParse(splitTime[1], out minutes);

        if (!isValidIntHours)
        {
            return -3;
        }

        if (!isValidIntMinutes)
        {
            return -2;
        }

        if (hours > 23 || hours < 0)
        {
            return -1;
        }

        if (minutes > 59 || minutes < 0)
        {
            return 0;
        }

        return 1;
    }
}