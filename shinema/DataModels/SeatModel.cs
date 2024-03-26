using System.Text.Json.Serialization;


class SeatModel
{
    [JsonPropertyName("rank")]
    public int Rank { get; set; }

    [JsonPropertyName("position")]
    public string Position { get; set; }

    [JsonPropertyName("available")]
    public bool Available { get; set; }

    public SeatModel(int rank, string position)
    {
        Rank = rank;
        Position = position;
        Available = true;
    }

}
