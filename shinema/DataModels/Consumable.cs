using System.Text.Json.Serialization;

public abstract class Consumable 
{
    [JsonPropertyName("id")]
    public int ID { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("amount")]
    public int Amount { get; set; }

    [JsonPropertyName("price")]
    public double Price { get; set; }

    public Consumable(int id, string title, int amount, double price) 
    {
        ID = id;
        Title = title;
        Amount = amount;
        Price = price;
    }

    public override string ToString()
    {
        return $"{Title} | \u20AC{Price.ToString("F2")}";
    }
}