using Microsoft.EntityFrameworkCore;

namespace mangadex_api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    public DbSet<Models.Manga> Mangas { get; set; }
}