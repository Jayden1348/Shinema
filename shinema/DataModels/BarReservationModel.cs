using System.Reflection.Metadata;
using System.Text.Json.Serialization;


public class BarReservationModel
{
    [JsonPropertyName("userid")]
    public int UserID { get; set; }

    [JsonPropertyName("uniqueCode")]
    public string UniqueCode { get; set; }

    [JsonPropertyName("date")]
    public DateTime Date { get; set; }

    [JsonPropertyName("barReservationAmount")]
    public int BarReservationAmount { get; set; }

    public const int BarCapacity = 40;
    public const int BarTimeReserve = 3;

    public BarReservationModel(int userID, string uniquecode, DateTime date, int barreservationAmount)
    {
        UserID = userID;
        UniqueCode = uniquecode;
        Date = date;
        BarReservationAmount = barreservationAmount;
    }

}