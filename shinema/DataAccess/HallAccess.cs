using System.Text.Json;

static class HallAccess
{
    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/defaulthalls.json"));


    public static List<List<SeatModel>> LoadAll(int which_hall)
    {
        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<List<List<SeatModel>>>>(json)![which_hall - 1];
    }
}