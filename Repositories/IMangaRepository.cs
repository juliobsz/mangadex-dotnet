namespace mangadex_api.Repositories
{
    public interface IMangaRepository
    {
        Task AddAsync(Models.Manga manga);
        Task<IEnumerable<Models.Manga>> GetAllAsync();
    }
}