using System.Text.Json;

public static class GenericAccess<T>
{

    public static List<T> LoadAll()
    {
        string json = File.ReadAllText(GetFullPath());
        return JsonSerializer.Deserialize<List<T>>(json);
    }
    public static List<List<T>> LoadAll(int which_hall)
    {
        string path = GetFullPath();
        string json = File.ReadAllText(path);
        List<List<T>> hall = new();
        int check_empty = json.Count();
        if (check_empty == 2)
        {
            WriteAll();
            json = File.ReadAllText(path);
        }
        return JsonSerializer.Deserialize<List<List<List<T>>>>(json)![which_hall - 1];
    }


    private static string GetFullPath()
    {
        Type t = typeof(T);
        string name = t.Name.Substring(0, t.Name.Length - 5);
        string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, $@"DataSources/{name}.json"));
        try
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"File not found at path: {path}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            // Handle the exception or rethrow it if necessary
        }
        return path;
    }

    public static void WriteAll(List<T> items)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(items, options);
        File.WriteAllText(GetFullPath(), json);
    }
    public static void WriteAll()
    {
        string path = GetFullPath();
        List<List<List<SeatModel>>> halls = new() { ReservationLogic.CreateMovieHall(1), ReservationLogic.CreateMovieHall(2), ReservationLogic.CreateMovieHall(3) };
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(halls, options);
        File.WriteAllText(path, json);
    }
}