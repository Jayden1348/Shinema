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
    public DateTime Datetime { get; set; }

    public ShowingModel(int id, int hallID, int movieID, DateTime datetime)
    {
        ID = id;
        HallID = hallID;
        MovieID = movieID;
        Datetime = datetime;
    }

    public bool IsSoldOut()
    {
        ReservationLogic r = new();
        List<List<SeatModel>> hall = HallAccess.LoadAll(this.HallID);
        hall = r.AddReservationsToHall(hall, this);
        return ReservationLogic.IsSoldOut(hall);
    }
    public override string ToString() => $"{this.Datetime.Date.ToShortDateString()} {this.Datetime.ToShortTimeString()} (hall {this.HallID}) {(IsSoldOut() ? " (SOLD OUT!)" : "")}";
}