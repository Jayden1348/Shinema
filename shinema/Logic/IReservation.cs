public interface IReservation
{
    public string Unique_code { get; set; }
    public int Account_ID { get; set; }

    public string AllDetails();
    public string ToString();

}