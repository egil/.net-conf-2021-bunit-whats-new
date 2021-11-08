using System.IO.Compression;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var data = await GetData();

app.MapGet("/", () => data.Take(10));

app.Run();

static async Task<IReadOnlyList<JsonElement>> GetData()
{
    using var fileStream = File.OpenRead(@"weather_data.zip");
    using var archive = new ZipArchive(fileStream, ZipArchiveMode.Read);
    var json = archive.GetEntry("weather_data.json");

    if (json is not null)
    {
        var doc = await JsonDocument.ParseAsync(json.Open());
        return doc.RootElement.GetProperty("data").EnumerateArray().ToArray();
    }

    return Array.Empty<JsonElement>();
}