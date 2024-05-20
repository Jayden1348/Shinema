using System.Text.Json;

static class FoodAcces {
    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/food.json"));

    public static List<FoodModel> LoadAll()
    {
        string json = File.ReadAllText(path); 
        return JsonSerializer.Deserialize<List<FoodModel>>(json);
    }

    public static void WriteAll(List<FoodModel> food)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(food, options);
        File.WriteAllText(path, json);
    }
}