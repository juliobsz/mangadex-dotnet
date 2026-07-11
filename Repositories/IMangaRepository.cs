namespace mangadex_api.Repositories
{
    public interface IMangaRepository
    {
        //Task<Models.Manga> GetByIdAsync(int id);
        Task AddAsync(Models.Manga manga);
    }
}