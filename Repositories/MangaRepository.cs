using mangadex_api.Data;
using Microsoft.EntityFrameworkCore;

namespace mangadex_api.Repositories
{
    public class MangaRepository(ApplicationDbContext context) : IMangaRepository
    {
        public async Task AddAsync(Models.Manga manga)
        {
            await context.Mangas.AddAsync(manga);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Models.Manga>> GetAllAsync()
        {
            return await context.Mangas.ToListAsync();
        }

        public async Task<IEnumerable<Models.Manga>> GetFavoritesAsync()
        {
            return await context.Mangas.Where(m => m.Favorite).ToListAsync();
        }

        public async Task<IEnumerable<Models.Manga>> GetRecentsAsync()
        {
            return await context.Mangas.OrderBy(m => m.UpdatedAt).Take(4).ToListAsync();
        }
    }
}