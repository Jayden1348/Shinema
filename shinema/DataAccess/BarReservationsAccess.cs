using System.Text.Json;
public static class BarReservationAccess
{
    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/BarReservation.json"));

    public static List<BarReservationModel> GetAllBarReservations()
    {
        //returns a list of BarReservationModels
        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<BarReservationModel>>(json)!;
    }

    public static void WriteAllBarReservations(List<BarReservationModel> barReservations)
    {
        //writes a list of BarReservationModels to barreservation.json
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(barReservations, options);
        File.WriteAllText(path, json);
    }


}