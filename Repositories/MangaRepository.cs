using mangadex_api.Data;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<Models.Manga>> GetAllAsync()
        {
            return await _context.Mangas.ToListAsync();
        }
    }
}