using System.Text.Json.Serialization;

public class MovieModel
{

    [JsonPropertyName("id")]
    public int ID { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("length")]
    public int Length { get; set; }

    [JsonPropertyName("age")]

    public string Age { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }


    [JsonPropertyName("genre")]
    public List<string> Genre { get; set; }

    [JsonPropertyName("releaseDate")]
    public string Release_Date { get; set; }


    public MovieModel(int id, string title, int length, string age, string description, List<string> genre, string release_date)
    {
        ID = id;
        Title = title;
        Length = length;
        Age = age;
        Description = description;
        Genre = genre;
        Release_Date = release_date;
    }

    public override string ToString() => $"{this.Title}";
}