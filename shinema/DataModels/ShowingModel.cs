using System.Dynamic;
using System.Text.Json.Serialization;

public class ShowingModel
{
    [JsonPropertyName("id")]
    public int ID { get; set; }

    [JsonPropertyName("roomID")]
    public int RoomID { get; set; }

    [JsonPropertyName("movieID")]
    public int MovieID { get; set; }

    [JsonPropertyName("date")]
    public DateTime Date { get; set; }

    [JsonPropertyName("time")]

    public DateTime Time { get; set; }

    public ShowingModel(int id, int roomID, int movieID, DateTime date, DateTime time)
    {
        ID = id;
        RoomID = roomID;
        MovieID = movieID;
        Date = date;
        Time = time;
    }

}