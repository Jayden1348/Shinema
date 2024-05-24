using System.Text.Json.Serialization;


public class BarReservationModel : IReservation, IComparable<BarReservationModel>
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

    public BarReservationModel(int account_id, string unique_code, DateTime date, int number_of_seats)
    {
        Account_ID = account_id;
        Unique_code = unique_code;
        Date = date;
        Number_of_seats = number_of_seats;
    }

    public override string ToString()
    {
        return $"Reservation for {Number_of_seats} seats, {DateOnly.FromDateTime(Date)} {TimeOnly.FromDateTime(Date)} - {DateOnly.FromDateTime(Date.AddHours(3))}";
    }

    public string AllDetails()
    {
        return $"Reservation for {Number_of_seats} seats\n - Date: {DateOnly.FromDateTime(Date)} {TimeOnly.FromDateTime(Date)} - {TimeOnly.FromDateTime(Date.AddHours(3))}\n - Unique code: {Unique_code}\n";
    }


    public int CompareTo(object other)
    {
        if (other == null) { return -1; }
        else if (other is BarReservationModel r) { return CompareTo(r); }
        else return -1;
    }

    public int CompareTo(BarReservationModel other)
    {
        if (other is null) return -1;
        else if (Date > other.Date) return 1;
        else if (Date == other.Date) return 0;
        else return -1;
    }
}