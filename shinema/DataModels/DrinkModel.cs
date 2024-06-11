using System.Text.Json.Serialization;

public enum Sizes
{
    Small,
    Medium,
    Large
}
public class DrinkModel : Consumable {
    [JsonPropertyName("size")]
    public Sizes Size { get; set; }
    public DrinkModel(int id, string title, int amount, double price, Sizes size) : base(id, title, amount, price) 
    {
        Size = size;
    }

    public override string ToString()
    {
        return $"{Size} {Title} | \u20AC{Price.ToString("F2")}";
    }
}

