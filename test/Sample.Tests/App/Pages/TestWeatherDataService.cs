namespace Sample.App.Pages;

internal class TestWeatherDataService : IWeatherDataService
{
    public Task<IReadOnlyList<WeatherForecast>> GetForecasts()
        => Task.FromResult<IReadOnlyList<WeatherForecast>>(
            new WeatherForecast[]
            {
                new(){ Date = DateTime.Now, Summary = "Hot", TemperatureC = 42 },
                new(){ Date = DateTime.Now.AddDays(1), Summary = "Cold", TemperatureC = -2 },
                new(){ Date = DateTime.Now.AddDays(2), Summary = "Volcano", TemperatureC = 1_250 },
            });
}