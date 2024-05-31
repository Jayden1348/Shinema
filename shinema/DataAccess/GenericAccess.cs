using System.Text.Json;

public static class GenericAccess<T>
{

    public static List<T> LoadAll()
    {
        string json = File.ReadAllText(GetFullPath());
        return JsonSerializer.Deserialize<List<T>>(json);
    }


    private static string GetFullPath()
    {
        Type t = typeof(T);
        string name = t.Name.Substring(0, t.Name.Length - 5);
        string location = "DataSources/" + name + ".json";
        string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, location));
        return path;
    }

    public static void WriteAll(List<T> items)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(items, options);
        File.WriteAllText(GetFullPath(), json);
    }
}