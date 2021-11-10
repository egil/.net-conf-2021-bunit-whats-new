namespace Sample.App.Services;

public interface IWeatherDataService
{
    Task<IReadOnlyList<WeatherForecast>> GetForecasts();
}

