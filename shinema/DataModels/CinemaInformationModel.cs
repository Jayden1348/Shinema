using System.Text.Json.Serialization;
class CinemaInformationModel
{
    [JsonPropertyName("city")]
    public string City { get; set; }

    [JsonPropertyName("address")]
    public string Address { get; set; }

    [JsonPropertyName("openingtime")]
    public string OpeningTime { get; set; }

    [JsonPropertyName("closingtime")]
    public string ClosingTime { get; set; }

    [JsonPropertyName("phonenumber")]
    public string PhoneNumber { get; set; }
    [JsonPropertyName("email")]
    public string Email { get; set; }

    public CinemaInformationModel(string city, string address, string openingTime, string closingTime, string phoneNumber, string email)
    {
        City = city;
        Address = address;
        OpeningTime = openingTime;
        ClosingTime = closingTime;
        PhoneNumber = phoneNumber;
        Email = email;
    }
}