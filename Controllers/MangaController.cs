using Microsoft.AspNetCore.Mvc;

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

    [HttpGet("sync")]
    public async Task<IActionResult> SyncFollows()
    {
        var client = _httpClientFactory.CreateClient("mangadex");
        client.DefaultRequestHeaders.UserAgent.ParseAdd("mangadex-api/1.0");
        
        var accessToken = await Auth(client);
        if (accessToken == null) return StatusCode(401);
        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

        var res = await client.GetAsync("https://api.mangadex.org/user/follows/manga");
        if (!res.IsSuccessStatusCode) return StatusCode(401);
        
        var body = await res.Content.ReadFromJsonAsync<MangaResponse>();
        return StatusCode(200, body?.Data?.Length);
    }
    
    public async Task<string?> Auth(HttpClient client)
    {
        var username = _config["MangaDex:Username"] ?? "";
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
        if (!res.IsSuccessStatusCode) return null;
        var body = await res.Content.ReadFromJsonAsync<Auth>();
        return body?.AccessToken;
    }
}
