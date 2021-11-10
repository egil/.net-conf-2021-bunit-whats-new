namespace Sample.App.Pages;

internal class TestWeatherDataService : IWeatherDataService
{
    public Task<IReadOnlyList<WeatherForecast>> GetForecasts()
        => Task.FromResult<IReadOnlyList<WeatherForecast>>(
            new WeatherForecast[]
            {
                new()
                {
                    Date = new DateTime(2018,5,6),
                    TemperatureC = 1,
                    Summary = "Freezing",
                },
                new()
                {
                    Date = new DateTime(2018,5,7),
                    TemperatureC = 14,
                    Summary = "Bracing",
                },
                new()
                {
                    Date = new DateTime(2018,5,8),
                    TemperatureC = -13,
                    Summary = "Freezing",
                },
                new()
                {
                    Date = new DateTime(2018,5,9),
                    TemperatureC = -16,
                    Summary = "Balmy",
                },
            });
}