using System.Text.Json.Serialization;


public class BarReservationModel : IReservation
{
    [JsonPropertyName("account_id")]
    public int Account_ID { get; set; }

    [JsonPropertyName("unique_code")]
    public string Unique_code { get; set; }

    [JsonPropertyName("date")]
    public DateTime Date { get; set; }

    [JsonPropertyName("number_of_seats")]
    public int Number_of_seats { get; set; }

    public const int BarCapacity = 40;
    public const double BarTimeReserve = 3.0;

    public BarReservationModel(int userID, string uniquecode, DateTime date, int number_of_seats)
    {
        Account_ID = userID;
        Unique_code = uniquecode;
        Date = date;
        Number_of_seats = number_of_seats;
    }

    public override string ToString()
    {
        return $"Reservation for {Number_of_seats} seats, {DateOnly.FromDateTime(Date)} {TimeOnly.FromDateTime(Date)} - {DateOnly.FromDateTime(Date.AddHours(3))}";
    }

    public string AllDetails()
    {
        return $"Reservation for {Number_of_seats} seats\n - Date: {DateOnly.FromDateTime(Date)} {TimeOnly.FromDateTime(Date)} - {DateOnly.FromDateTime(Date.AddHours(3))}\n - Unique code: {Unique_code}";
    }

}