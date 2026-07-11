using mangadex_api.Data;
using mangadex_api.Models;

namespace mangadex_api.Repositories
{
    public class MangaRepository : IMangaRepository
    {
        private readonly ApplicationDbContext _context;

        public MangaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Models.Manga manga)
        {
            await _context.Mangas.AddAsync(manga);
            await _context.SaveChangesAsync();
        }
    }
}