using DBLayer;
using Models;

namespace CLFacade
{
    public class Repository
    {
        private DB db;
        public Repository()
        {
            db = new DB();
        }
        public async Task<Rootobject> GetMovieAsync()
        {
            return await db.GetMovieAsync();
        }
        public async Task<Rootobject> GetMovieFromSearchQueryAsync(string query)
        {
            if(!string.IsNullOrEmpty(query))
            {
                return await db.GetMovieFromSearchQueryAsync(query);
            }
            return null;
        }
    }
}