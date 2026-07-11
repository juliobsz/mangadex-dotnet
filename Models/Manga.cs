using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mangadex_api.Models;

[Table("mangas")]
public class Manga
{
    [Column("id")]
    public int Id { get; set; }
    [Column("mangadex_id")]
    [MaxLength(50)]
    public string MangaDexId { get; set; } = string.Empty;
    [Column("name")]
    [MaxLength(256)]
    public string Name { get; set; } = string.Empty;
    [Column("cover")]
    [MaxLength(256)]
    public string Cover { get; set; } = string.Empty;
    [Column("url")]
    [MaxLength(256)]
    public string Url { get; set; } = string.Empty;
    [Column("status")]
    [MaxLength(50)]
    public string Status { get; set; } = string.Empty;
    [Column("favorite")]
    public bool Favorite { get; set; } = false;
}