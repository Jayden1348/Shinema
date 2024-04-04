using System.Dynamic;
using System.Text.Json.Serialization;

public class ShowingModel
{
    [JsonPropertyName("id")]
    public int ID { get; set; }

    [JsonPropertyName("hallID")]
    public int HallID { get; set; }

    [JsonPropertyName("movieID")]
    public int MovieID { get; set; }

    [JsonPropertyName("date")]
    public DateTime Date { get; set; }

    public ShowingModel(int id, int hallID, int movieID, DateTime date)
    {
        ID = id;
        HallID = hallID;
        MovieID = movieID;
        Date = date;
    }

}