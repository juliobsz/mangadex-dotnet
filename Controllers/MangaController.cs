using mangadex_api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace mangadex_api.Controllers;

[ApiController]
[Route("v1")]
public class MangaController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _config;
    private readonly IMangaRepository _mangaRepository;

    public MangaController(IHttpClientFactory httpClientFactory, IConfiguration config, IMangaRepository mangaRepository)
    {
        _config = config;
        _mangaRepository = mangaRepository;
        _httpClientFactory = httpClientFactory;
    }

    [HttpGet("mangas")]
    public async Task<IActionResult> GetMangas()
    {
        return StatusCode(200, await _mangaRepository.GetAllAsync());
    }

    [HttpGet("sync")]
    public async Task<IActionResult> SyncFollows()
    {
        var client = _httpClientFactory.CreateClient("mangadex");
        client.DefaultRequestHeaders.UserAgent.ParseAdd("mangadex-api/1.0");
        
        var accessToken = await Auth(client);
        if (accessToken == null) return StatusCode(401);
        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
        
        var res = await client.GetAsync("https://api.mangadex.org/manga/status");
        if (!res.IsSuccessStatusCode) return StatusCode(401);
        
        var body = await res.Content.ReadFromJsonAsync<MangaStatusResponse>();
        if (body?.Stats == null || body.Stats.Count == 0) return StatusCode(401);

        for (var i = 0; i < body.Stats.Count; i++)
        {
            var stats = body.Stats.ElementAt(i);
            var mangaResponse = await client.GetAsync($"https://api.mangadex.org/manga/{stats.Key}?includes[]=cover_art");
            if (!mangaResponse.IsSuccessStatusCode) return StatusCode(401);
            var manga = await mangaResponse.Content.ReadFromJsonAsync<MangaResponse>();
            
            if  (manga?.Data == null) continue;
            var mangaData = manga.Data;

            var coverArt = mangaData.Relationships?.FirstOrDefault(r => r.Type == "cover_art");
            var data = new Models.Manga
            {
                MangaDexId = mangaData.Id ?? "",
                Name = mangaData.Attributes?.Title?.FirstOrDefault().Value ?? "",
                Cover = $"https://mangadex.org/covers/{mangaData.Id}/{coverArt?.Attributes?.FileName}",
                Url = "https://mangadex.org/title/" + mangaData.Id,
                Status = stats.Value,
                Favorite = false
            };
            await _mangaRepository.AddAsync(data);
        }
        
        return StatusCode(200, new { message = $"{body.Stats.Count} synced" });
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
