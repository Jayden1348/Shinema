using System.Text.Json;

public static class GenericAccess<T>
{
    public static string GetFullPath()
    {
        Type t = typeof(T);
        string name = t.Name.Substring(0, t.Name.Length - 5);
        string location = "@DataSources/" + name + ".json";
        string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, location));
        return path;
    }
}