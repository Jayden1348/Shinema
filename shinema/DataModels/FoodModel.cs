public class FoodModel : Consumable
{
    public FoodModel(int id, string title, int amount, double price) : base(id, title, amount, price)
    {
        ID = id;
        Title = title;
        Amount = amount;
        Price = price;
    }
}
