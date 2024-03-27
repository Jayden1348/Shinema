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

    public static string GetCinemaInfo(string city, string address, string openingTime, string closingTime, string phoneNumber, string email)
    {
        return @$"The Cinema is located at {address} {city}.
It Opens at {openingTime} and closes at {closingTime}

Cinema Contact Info: 
Phone Number: {phoneNumber}
E-mail: {email}
";
    }

    public static void SaveCinemaInfo(string city, string address, string openingTime, string closingTime, string phoneNumber, string email)
    {
        CinemaInformationModel cinemaInfoObject = new CinemaInformationModel(city, address, openingTime, closingTime, phoneNumber, email);
        CinemaInformationAccess.WriteInfoCinema(cinemaInfoObject);
    }
}