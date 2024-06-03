using System.Text.Json.Serialization;


public class ReservationModel : IReservation, IComparable<ReservationModel>

{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("showing_id")]
    public int Showing_ID { get; set; }

    [JsonPropertyName("account_id")]
    public int Account_ID { get; set; }

    [JsonPropertyName("seats")]
    public List<string> Seats { get; set; }

    [JsonPropertyName("price")]
    public double Price { get; set; }

    [JsonPropertyName("unique_code")]
    public string Unique_code { get; set; }

    [JsonPropertyName("snacks")]
    public Dictionary<int, int> Snacks { get; set; }

    public ReservationModel(int id, int showing_id, int account_id, List<string> seats, double price, string unique_code, Dictionary<int, int> snacks)
    {
        Id = id;
        Showing_ID = showing_id;
        Account_ID = account_id;
        Seats = seats;
        Price = price;
        Unique_code = unique_code;
        Snacks = snacks;
    }

    public override string ToString()
    {
        ShowingsLogic s = new ShowingsLogic();
        return $"Reservation for {MoviesLogic.GetById(s.GetById(Showing_ID).MovieID).Title}, {s.GetById(Showing_ID).Datetime}";
    }

    public string AllDetails()
    {
        ShowingsLogic s = new ShowingsLogic();
        int hall = s.GetById(Showing_ID).RoomID;
        string date = s.GetById(Showing_ID).Datetime.ToString();
        string title = MoviesLogic.GetById(s.GetById(Showing_ID).MovieID).Title;
        string seatstring = Seats[0];
        foreach (string seat in Seats[1..])
        {
            seatstring += $", {seat}";
        }
        return $"Reservation for {title}\n - Hall: {hall}\n - Date: {date}\n - Seats: {seatstring}\n - Total price: {Price}\n - Reservation code: {Unique_code}\n";
    }

    public int CompareTo(object other)
    {
        if (other == null) { return -1; }
        else if (other is ReservationModel r) { return CompareTo(r); }
        else return -1;
    }

    public int CompareTo(ReservationModel other)
    {
        ShowingsLogic s = new ShowingsLogic();
        if (other is null) return -1;
        else if (s.GetById(Showing_ID).Datetime > s.GetById(other.Showing_ID).Datetime) return 1;
        else if (s.GetById(Showing_ID).Datetime == s.GetById(other.Showing_ID).Datetime) return 0;
        else return -1;


    }

}