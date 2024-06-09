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

    public static void BuyFood(FoodModel item, int amount)
    {

        foreach (FoodModel foodItem in _food)
        {
            if (foodItem.ID == item.ID)
            {
                foodItem.Amount -= amount;
            }
        }

        GenericAccess<FoodModel>.WriteAll(_food);
    }

    public static double GetTotalSnackPrice(Dictionary<int, int> reservedSnacks)
    {
        double totalPrice = 0.0;
        Dictionary<string, double> foodPriceDict = GetFoodPriceDictionary();
        if (reservedSnacks is null)
        {
            return 0.0;
        }
        foreach(KeyValuePair<int, int> reservedSnack in reservedSnacks)
        {
            totalPrice += foodPriceDict[Convert.ToString(reservedSnack.Key)] * reservedSnack.Value;
        }
        return totalPrice;
    }

    public static Dictionary<string,double> GetFoodPriceDictionary()
    {
        List<FoodModel> foodList = GetAllFood();
        if (!foodList.Any())
        {
            return default;
        }

        Dictionary<string, double> foodPriceDict = new Dictionary<string, double>();

        foreach (FoodModel food in foodList)
        {
            foodPriceDict.Add(Convert.ToString(food.ID), food.Price);
        }
        return foodPriceDict;

    }
}