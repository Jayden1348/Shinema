using System.Text.Json.Serialization;

public class SeatModel
{
    [JsonPropertyName ("id")]
    public int ID { get; set; }

    [JsonPropertyName ("rank")]
    public int Rank { get; set; }

    [JsonPropertyName("hallID")]
    public int HallID { get; set; }

    [JsonPropertyName ("position")]
    public string Position { get; set; }

    [JsonPropertyName ("available")]
    public bool Available { get; set; }

    public SeatModel(int id, int rank, int hallID, string position)
    {
        ID = id;
        Rank = rank;
        HallID = hallID;
        Position = position;
        Available = true;
    }

}
