using Microsoft.Extensions.Configuration;
using Models.MovieModels;
using Newtonsoft.Json;
using System.Text;

namespace DBLayer
{
    public class DB
    {
        private HttpClient httpClient;
        public DB(IConfiguration configuration)
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {configuration.GetConnectionString("Access-Token-Auth")}");
            httpClient.DefaultRequestHeaders.Add("accept", "application/json");
        }

        public async Task<Rootobject> GetMovieAsync()
        {
            //delete later
            return null;
        }
        public async Task<Rootobject> GetMoviesFromSearchQueryAsync(string query)
        {
            string apiUrl = $"https://api.themoviedb.org/3/search/movie?query={query}&include_adult=false&language=en-US&page=1";
            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                Rootobject robj = JsonConvert.DeserializeObject<Rootobject>(content);
                return robj;
            }
            return null;
        }
    }
}
