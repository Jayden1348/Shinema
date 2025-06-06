public static class DrinkLogic
{
    private static List<DrinkModel> _drinks;

    static DrinkLogic()
    {
        _drinks = GenericAccess<DrinkModel>.LoadAll();
    }

    public static bool AddDrink(string title, int amount, double price, Sizes size)
    {
        if (!string.IsNullOrEmpty(title) && amount != default && price != default)
        {
            int id;
            if (_drinks.Any())
            {
                id = _drinks.Max(drink => drink.ID) + 1;
            }
            else
            {
                id = 1;
            }

            _drinks.Add(new DrinkModel(id, title, amount, price, size));

            GenericAccess<DrinkModel>.WriteAll(_drinks);
            return true;
        }

        return false;
    }

    public static void BuyDrink(DrinkModel item)
    {

        foreach (DrinkModel drinkItem in _drinks)
        {
            if (drinkItem.ID == item.ID)
            {
                drinkItem.Amount = item.Amount;
            }
        }

        GenericAccess<DrinkModel>.WriteAll(_drinks);
    }


    public static List<DrinkModel> GetAvailableDrinks() 
    {
        return _drinks.Where(drink => drink.Amount > 0).ToList();
    }
    public static bool CheckStock(DrinkModel drink, int amount)
    {
        return drink.Amount >= amount;
    }
  
    public static void UpdateDrinks(List<DrinkModel> drinks)
    {
        _drinks = drinks;
        GenericAccess<DrinkModel>.WriteAll(_drinks);
    }

    public static List<DrinkModel> GetAllDrinks()
    {
        return _drinks;
    }

    public static void DeleteDrink(DrinkModel item)
    {
        _drinks.Remove(item);
        GenericAccess<DrinkModel>.WriteAll(_drinks);

    }

}