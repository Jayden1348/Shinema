using System.Text.Json;

static class HallAccess
{
    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/defaulthalls.json"));


    public static List<List<SeatModel>> LoadAll(int which_hall)
    {
        string json = File.ReadAllText(path);
        List<List<SeatModel>> hall = new();
        int check_empty = json.Count();
        if (check_empty == 2)
        {
            WriteAll();
            json = File.ReadAllText(path);
        }
        return JsonSerializer.Deserialize<List<List<List<SeatModel>>>>(json)![which_hall - 1];
    }

    public static void WriteAll()
    {
        List<List<List<SeatModel>>> halls = new() { HallLogic.CreateMovieHall(1), HallLogic.CreateMovieHall(2), HallLogic.CreateMovieHall(3) };
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(halls, options);
        File.WriteAllText(path, json);
    }
}