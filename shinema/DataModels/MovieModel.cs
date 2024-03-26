using System.Text.Json.Serialization;

class MovieModel {

    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("length")]
    public int Length { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("time")]
    public string Time { get; set; }

    [JsonPropertyName("genre")]
    public string Genre { get; set; }

    [JsonPropertyName("releaseDate")]
    public string Release_Date { get; set; }


    public MovieModel(string title, int length, string description, string time, string genre, string release_date) {
        Title = title;
        Length = length;
        Description = description;
        Time = time;
        Genre = genre;
        Release_Date = release_date;
    }
}