using System.Text.Json.Serialization;

namespace mangadex_api;

public class Manga
{
    public string? Id { get; set; }
    public string? Type { get; set; }
    public MangaAttributes? Attributes {  get; set; }
    public MangaRelationships[]? Relationships { get; set; }
}

public class MangaAttributes
{
    [JsonPropertyName("title")]
    public Dictionary<string, string>? Title { get; set; }
}

public class MangaRelationships
{
    public string? Id { get; set; } = "";
    public string? Type { get; set; } = "";
    public RelationshipAttributes? Attributes { get; set; }
}

public class RelationshipAttributes
{
    [JsonPropertyName("fileName")]
    public string? FileName { get; set; }
    public string? Volume { get; set; }
}

public class Auth
{
    [JsonPropertyName("access_token")] 
    public string? AccessToken { get; set; }
    [JsonPropertyName("refresh_token")]
    public string? RefreshToken { get; set; }
}

public class MangaResponse
{
    public string? Result { get; set; }
    public string? Response { get; set; }
    public Manga? Data { get; set; }
}

public class MangaStatusResponse
{
    public string? Result { get; set; }
    public string? Response { get; set; }
    [JsonPropertyName("statuses")]
    public Dictionary<string, string>? Stats { get; set; }
}