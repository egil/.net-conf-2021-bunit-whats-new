namespace Sample.App.Services;

public class WeatherDataService : IWeatherDataService
{
    private readonly HttpClient httpClient;

    public WeatherDataService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<IReadOnlyList<WeatherForecast>> GetForecasts()
    {
        var result = await httpClient.GetFromJsonAsync<IReadOnlyList<WeatherForecast>>("sample-data/weather.json");
        return result ?? Array.Empty<WeatherForecast>();
    }
}
