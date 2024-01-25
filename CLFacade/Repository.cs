using DBLayer;
using Models.MovieModels;
using Microsoft.Extensions.Configuration;
using MyModels;

namespace CLFacade
{
    public class Repository : IRepository
    {
        private DB db;
        public Repository(IConfiguration configuration)
        {
            db = new DB(configuration);
        }
        public async Task<Rootobject> GetMovieAsync()
        {
            return await db.GetMovieAsync();
        }
        public async Task<List<Movie>> GetMoviesFromSearchQueryAsync(string query)
        {
            if (!string.IsNullOrEmpty(query))
            {
                Rootobject robj = await db.GetMoviesFromSearchQueryAsync(query);
                if (robj != null)
                {
                    List<Movie> movies = new List<Movie>();
                    foreach (Result movie in robj.results)
                    {
                        Movie uiMovie = new Movie();
                        uiMovie.Title = movie.title;
                        uiMovie.Id = movie.id.ToString();
                        if(!string.IsNullOrEmpty(movie.poster_path))
                        {
                            uiMovie.Poster_Url = $"https://image.tmdb.org/t/p/original/{movie.poster_path}";
                        }
                        else
                        {
                            uiMovie.Poster_Url = string.Empty;
                        }
                        uiMovie.Description = movie.overview;
                        movies.Add(uiMovie);
                    }
                    return movies;
                }
            }
            return null;
        }
    }
}