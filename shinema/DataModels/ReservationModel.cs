using System.Text.Json.Serialization;


public class ReservationModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("showing_id")]
    public int Showing_ID { get; set; }

    [JsonPropertyName("account_id")]
    public int Account_ID { get; set; }

    [JsonPropertyName("seats")]
    public List<string> Seats { get; set; }

    public ReservationModel(int id, int showing_id, int account_id, List<string> seats)
    {
        Id = id;
        Showing_ID = showing_id;
        Account_ID = account_id;
        Seats = seats;
    }

}