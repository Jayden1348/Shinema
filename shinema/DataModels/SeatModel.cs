public class SeatModel
{
    public int Rank { get; set; }
    public string Position { get; set; }
    public bool Available { get; set; }

    public SeatModel(int rank, string position)
    {
        Rank = rank;
        Position = position;
        Available = true;
    }

}
