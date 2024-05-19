using System.Text.Json.Serialization;

public class FoodModel {
    [JsonPropertyName("id")]
    public int ID { get; set; }

    [JsonPropertyName("length")]
    public string Title { get; set; }

    [JsonPropertyName("amount")]
    public int Amount { get; set; }

    [JsonPropertyName("price")]
    public double Price { get; set; }

    public FoodModel(int id, string title, int amount, double price) {
        ID = id;
        Title = title;
        Amount = amount;
        Price = price;
    }
}