using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace mangadex_api.Controllers;

[ApiController]
[Route("v1")]
public class MangaController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _config;

    public MangaController(IHttpClientFactory httpClientFactory, IConfiguration config)
    {
        _config = config;
        _httpClientFactory = httpClientFactory;
    }

    [HttpPost("authenticate")]
    public async Task<IActionResult> Auth()
    {
        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.UserAgent.ParseAdd("mangadex-api/1.0");
        
        var username = _config["MangaDex:Username"] ?? "";
        Console.WriteLine(username);
        var password = _config["MangaDex:Password"] ?? "";
        var clientId = _config["MangaDex:ClientId"] ?? "";
        var clientSecret = _config["MangaDex:ClientSecret"] ?? "";
        var data = new Dictionary<string, string>
        {
            { "grant_type", "password" },
            { "username", username },
            { "password", password },
            { "client_id", clientId },
            { "client_secret", clientSecret }
        };
        var res = await client.PostAsync("https://auth.mangadex.org/realms/mangadex/protocol/openid-connect/token",
            new FormUrlEncodedContent(data));
        //res.EnsureSuccessStatusCode();

        var body = await res.Content.ReadAsStringAsync();
        return StatusCode(200, body);
    }
    
    /*[HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }*/
}
