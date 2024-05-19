public static class FoodLogic {

    private static List<FoodModel> _food;

    static FoodLogic()
    {
        _food = FoodAcces.LoadAll();
    }

    public static void AddFood() {
        
    }
}