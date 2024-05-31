public static class FoodLogic {

    private static List<FoodModel> _food;

    static FoodLogic()
    {
        _food = FoodAccess.LoadAll();
    }

    public static bool AddFood(string title, int amount, double price) {
        
        if(!string.IsNullOrEmpty(title) && amount != default && price != default) {
            int id;
            if (_food.Any()) {
                id = _food.Max(food => food.ID) + 1;
            } else {
                id = 1;
            }

            _food.Add(new(id, title, amount, price));

            FoodAccess.WriteAll(_food);
            return true;
        }

        return false;
    }

    public static bool AddFood(int amount, double price){
        return AddFood(null, amount, price);
    }

    public static bool AddFood(string title, double price){
        return AddFood(title, default, price);
    }
    public static bool AddFood(string title, int amount){
        return AddFood(title, amount, default);
    }

    public static List<FoodModel> GetAllFood() {
        return _food;
    }

    public static void BuyFood(FoodModel item, int amount) {
        
        foreach(FoodModel foodItem in _food) {
            if(foodItem.ID == item.ID) {
                foodItem.Amount -= amount;
            }
        }

        FoodAccess.WriteAll(_food);
    }
}