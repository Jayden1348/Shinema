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

    public double GetPrice()
    {
        switch (Rank)
        {
            case 1: return 15.00;
            case 2: return 12.50;
            case 3: return 10.00;
            default: return 0;
        }
    }

}
