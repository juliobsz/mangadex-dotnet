namespace mangadex_api.Repositories
{
    public interface IMangaRepository
    {
        Task AddAsync(Models.Manga manga);
        Task<IEnumerable<Models.Manga>> GetAllAsync();
        Task<IEnumerable<Models.Manga>> GetFavoritesAsync();
        Task<IEnumerable<Models.Manga>> GetRecentsAsync();
    }
}