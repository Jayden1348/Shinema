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
    public DateTime Datetime { get; set; }

    public ShowingModel(int id, int roomID, int movieID, DateTime datetime)
    {
        ID = id;
        RoomID = roomID;
        MovieID = movieID;
        Datetime = datetime;
    }

    public bool IsSoldOut()
    {
        ReservationLogic r = new();
        List<List<SeatModel>> hall = GenericAccess<SeatModel>.LoadAll(this.RoomID);
        hall = r.AddReservationsToHall(hall, this);
        return ReservationLogic.IsSoldOut(hall);
    }
    public override string ToString() => $"{MoviesLogic.GetById(MovieID).Title} | {this.Datetime.Date.ToShortDateString()} {this.Datetime.ToShortTimeString()} (hall {this.RoomID}) {(IsSoldOut() ? " (SOLD OUT!)" : "")}";
}