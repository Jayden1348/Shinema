using System.Text.Json;
public static class CinemaInformationAccess
{
    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/cinemaInformation.json"));
    
    public static CinemaInformationModel LoadInfo()
    {
        //returns an object from CinemaInformation.json

        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<CinemaInformationModel>(json);
    }

    public static void WriteInfoCinema(CinemaInformationModel cinema)
    {
        //Writes CinemaInformation object to CinemaInformation.json
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(cinema, options);
        File.WriteAllText(path, json);
    }
}