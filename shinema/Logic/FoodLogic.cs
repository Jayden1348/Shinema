public static class FoodLogic {

    private static List<FoodModel> _food;

    static FoodLogic()
    {
        _food = FoodAcces.LoadAll();
    }

    public static void AddFood(string title, int amount, double price) {

        int id;
        if (_food.Any()) {
            id = _food.Max(food => food.ID) + 1;
        } else {
            id = 1;
        }

        _food.Add(new(id, title, amount, price));

        FoodAcces.WriteAll(_food);
    }
}