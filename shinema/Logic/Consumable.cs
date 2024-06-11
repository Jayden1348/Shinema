public static class ConsumableLogic
{   
    public static double GetTotalSnackPrice<T>(Dictionary<int, int> reservedConsumable) where T : Consumable
    {
        double totalPrice = 0.0;
        
        Dictionary<string, double> consumablePriceDict = GetConsumablePriceDictionary<T>();
        if (reservedConsumable is null)
        {
            return 0.0;
        }
        foreach(KeyValuePair<int, int> reservedSnack in reservedConsumable)
        {
            totalPrice += consumablePriceDict[Convert.ToString(reservedSnack.Key)] * reservedSnack.Value;
        }
        return totalPrice;
    }

    public static Dictionary<string, double> GetConsumablePriceDictionary<T>() where T : Consumable
    {
        List<T> consumableList = GetConsumableList<T>();
        if (!consumableList.Any())
        {
            return default;
        }
        

        Dictionary<string, double> consumablePriceDict = new Dictionary<string, double>();

        foreach (T consumable in consumableList)
        {
            consumablePriceDict.Add(Convert.ToString(consumable.ID), consumable.Price);
        }

        return consumablePriceDict;
    }

    public static List<T> GetConsumableList<T>()
    {
        List<T> consumableList;
        if (typeof(T) == typeof(FoodModel))
        {
            consumableList = FoodLogic.GetAllFood() as List<T>;
            if (consumableList.Any())
            {
                return consumableList;
            }
        }
        else if (typeof(T) == typeof(DrinkModel))
        {
            consumableList = DrinkLogic.GetAllDrink() as List<T>;
            if (consumableList.Any())
            {
                return consumableList;
            }
            
        }
        return default;
    }
}