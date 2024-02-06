using MyApiModels;

namespace ApiCLFacade
{
    public interface IMovieRepository
    {
        Task<bool> SaveMovieAsync(Movie movie, int userId);
    }
}