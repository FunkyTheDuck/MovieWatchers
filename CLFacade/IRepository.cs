using Models.MovieModels;
using MyModels;

namespace CLFacade
{
    public interface IRepository
    {
        Task<Rootobject> GetMovieAsync();
        Task<List<Movie>> GetMoviesFromSearchQueryAsync(string query);
    }
}