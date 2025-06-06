public static class FoodLogic
{

    private static List<FoodModel> _food;

    static FoodLogic()
    {
        _food = GenericAccess<FoodModel>.LoadAll();
    }

    public static bool AddFood(string title, int amount, double price)
    {

        if (!string.IsNullOrEmpty(title) && amount != default && price != default)
        {
            int id;
            if (_food.Any())
            {
                id = _food.Max(food => food.ID) + 1;
            }
            else
            {
                id = 1;
            }

            _food.Add(new(id, title, amount, price));

            GenericAccess<FoodModel>.WriteAll(_food);
            return true;
        }

        return false;
    }

    public static bool AddFood(int amount, double price)
    {
        return AddFood(null, amount, price);
    }

    public static bool AddFood(string title, double price)
    {
        return AddFood(title, default, price);
    }
    public static bool AddFood(string title, int amount)
    {
        return AddFood(title, amount, default);
    }

    public static List<FoodModel> GetAllFood()
    {
        return _food;
    }

    public static void BuyFood(FoodModel item)
    {

        foreach (FoodModel foodItem in _food)
        {
            if (foodItem.ID == item.ID)
            {
                foodItem.Amount = foodItem.Amount;
            }
        }

        GenericAccess<FoodModel>.WriteAll(_food);
    }

    public static void UpdateFood(List<FoodModel> food)
    {
        _food = food;
        GenericAccess<FoodModel>.WriteAll(_food);
    }

    // get food with amount bigger than 0
    public static List<FoodModel> GetAvailableFood()
    {
        return _food.Where(f => f.Amount > 0).ToList();
    }

    public static bool CheckStock(FoodModel foodItem, int amount)
    {
        return foodItem.Amount >= amount;
    }

    public static void DeleteFood(FoodModel item)
    {
        _food.Remove(item);
        GenericAccess<FoodModel>.WriteAll(_food);
    }
}