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
}