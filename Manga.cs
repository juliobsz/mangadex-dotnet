using System.Text.Json.Serialization;

namespace mangadex_api;

public class Manga
{
    public string? Id { get; set; }
    public string? Type { get; set; }
    public MangaAttributes? Attributes {  get; set; }
}

public class MangaAttributes
{
    [JsonPropertyName("title")]
    public string Title { get; } = "";
    [JsonPropertyName("altTitle")]
    public string AltTitle { get; } = "";
    [JsonPropertyName("description")]
    public string Description { get; } = "";
    [JsonPropertyName("isLocked")]
    public string isLocked { get; } = "";
    [JsonPropertyName("links")]
    public string Links { get; } = "";
    [JsonPropertyName("OfficialLinks")]
    public string OfficialLinks { get; } = "";
    [JsonPropertyName("originalLanguage")]
    public string OriginalLanguage { get; } = "";
    [JsonPropertyName("lastVolume")]
    public string LastVolume { get; } = "";
    [JsonPropertyName("lastChapter")]
    public string LastChapter { get; } = "";
    [JsonPropertyName("publicationDemographic")]
    public string PublicationDemographic { get; } = "";
    [JsonPropertyName("status")]
    public string Status { get; } = "";
    [JsonPropertyName("year")]
    public string Year { get; } = "";
    [JsonPropertyName("contentRating")]
    public string ContentRating { get; } = "";
    [JsonPropertyName("state")]
    public string State { get; } = "";
    [JsonPropertyName("chapterNumbersResetOnNewVolume")]
    public string ChapterNumbersResetOnNewVolume { get; } = "";
    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; } = DateTime.Now;
    [JsonPropertyName("updatedAt")]
    public DateTime UpdatedAt { get; } = DateTime.Now;
    [JsonPropertyName("version")]
    public string Version { get; } = "";
    [JsonPropertyName("availableTranslatedLanguages")]
    public string AvailableTranslatedLanguages { get; } = "";
    [JsonPropertyName("latestUploadedChapter")]
    public string LatestUploadedChapter { get; } = "";
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
    public Manga[]? Data { get; set; }
    public int? Total { get; set; }
}