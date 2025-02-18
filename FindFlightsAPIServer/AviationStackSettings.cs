// Klasa przechowywania ustawieñ API
public class AviationStackSettings
{
    public string ApiKey { get; }

    public AviationStackSettings(string apiKey)
    {
        ApiKey = apiKey;
    }
}