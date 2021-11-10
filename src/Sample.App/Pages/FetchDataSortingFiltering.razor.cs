namespace Sample.App.Pages;

public sealed partial class FetchDataSortingFiltering : IDisposable
{
    private IReadOnlyList<WeatherForecast> forecasts = Array.Empty<WeatherForecast>();
    private string[] filterColumns = new[] { nameof(WeatherForecast.Date), nameof(WeatherForecast.TemperatureC), nameof(WeatherForecast.TemperatureF), nameof(WeatherForecast.Summary) };

    private SortingDirection sortDir = SortingDirection.Asc;
    private string sortCol = nameof(WeatherForecast.Date);

    [Inject]
    private IWeatherDataService WeatherService { get; set; } = default!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;

    [Parameter]
    [SupplyParameterFromQuery(Name = "sortBy")]
    public string SortColumn
    {
        get => sortCol;
        set => sortCol = value ?? nameof(WeatherForecast.Date);
    }

    [Parameter]
    [SupplyParameterFromQuery(Name = "sortDir")]
    public string SortDirection
    {
        get => sortDir.ToString();
        set
        {
            if (!Enum.TryParse(value, out sortDir))
            {
                sortDir = SortingDirection.Asc;
            }
        }
    }

    protected override async Task OnInitializedAsync()
    {
		NavigationManager.LocationChanged += NavigationManager_LocationChanged;
        forecasts = await WeatherService.GetForecasts();
        ApplySort();
    }

	public void Dispose() => NavigationManager.LocationChanged -= NavigationManager_LocationChanged;

	private void NavigationManager_LocationChanged(object? sender, LocationChangedEventArgs e) => ApplySort();

	private string GetSortIndicator(string sortColumn) => sortDir switch
    {
        SortingDirection.Asc when sortColumn == SortColumn => "⬆️",
        SortingDirection.Desc when sortColumn == SortColumn => "⬇️",
        _ => string.Empty
    };

    private void OnSorting(string sortColumn)
    {
        if (sortColumn == SortColumn)
        {
            sortDir = sortDir switch
            {
                SortingDirection.Asc => SortingDirection.Desc,
                SortingDirection.Desc => SortingDirection.Asc,
                _ => throw new InvalidOperationException("Unknown sort direction.")
            };
        }
        else
        {
            sortDir = SortingDirection.Asc;
        }

        SortColumn = sortColumn;

        ApplySort();
    }

    private void ApplySort()
    {
        var sorted = SortColumn switch
        {
            nameof(WeatherForecast.Date)
                => sortDir == SortingDirection.Asc
                    ? forecasts.OrderBy(x => x.Date)
                    : forecasts.OrderByDescending(x => x.Date),
            nameof(WeatherForecast.TemperatureC)
                => sortDir == SortingDirection.Asc
                    ? forecasts.OrderBy(x => x.TemperatureC)
                    : forecasts.OrderByDescending(x => x.TemperatureC),
            nameof(WeatherForecast.TemperatureF)
                => sortDir == SortingDirection.Asc
                    ? forecasts.OrderBy(x => x.TemperatureF)
                    : forecasts.OrderByDescending(x => x.TemperatureF),
            nameof(WeatherForecast.Summary)
                => sortDir == SortingDirection.Asc
                    ? forecasts.OrderBy(x => x.Summary)
                    : forecasts.OrderByDescending(x => x.Summary),
            _ => throw new InvalidOperationException($"Sorting by unsupported column '{SortColumn}'"),
        };

        forecasts = sorted.ToArray();

        var newUri = NavigationManager.GetUriWithQueryParameters(new Dictionary<string, object?>
        {
            ["sortBy"] = SortColumn,
            ["sortDir"] = SortDirection,
        });

        if(newUri != NavigationManager.Uri)
            NavigationManager.NavigateTo(newUri);
    }
}