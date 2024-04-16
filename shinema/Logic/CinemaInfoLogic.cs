using System.Net.Sockets;

public static class CinemaInfoLogic
{
    public static string GetCinemaInfo()
    {
        CinemaInformationModel cinemaInfo = CinemaInformationAccess.LoadInfo();
        
        return @$"The Cinema is located at {cinemaInfo.Address} {cinemaInfo.City}.
It Opens at {cinemaInfo.OpeningTime} and closes at {cinemaInfo.ClosingTime}

Cinema Contact Info: 
Phone Number: {cinemaInfo.PhoneNumber}
E-mail: {cinemaInfo.Email}
";
    }

    public static CinemaInformationModel GetCinemaInfoObject()
    {
        return CinemaInformationAccess.LoadInfo();
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
        CinemaInformationAccess.WriteInfoCinema(newCinemaInfoObject);
    }
}