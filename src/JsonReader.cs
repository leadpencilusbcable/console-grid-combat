using System.Text.Json;

public static class JsonFileReader
{
    public static async Task<T> ReadAsync<T>(string filePath)
    {
        using FileStream stream = File.OpenRead(filePath);
        return await JsonSerializer.DeserializeAsync<T>(stream);
    }
}