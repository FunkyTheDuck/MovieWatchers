using Models;
using Newtonsoft.Json;

namespace DBLayer
{
    public class DB
    {
        private HttpClient httpClient;
        public DB()
        {
            httpClient = new HttpClient();
        }

        public async Task<Rootobject> GetMovieAsync()
        {
            //delete later
            return null;
        }
        public async Task<Rootobject> GetMovieFromSearchQueryAsync(string query)
        {
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://imdb146.p.rapidapi.com/v1/find/?query={query}?limit=10"),
                Headers =
                    {
                        { "X-RapidAPI-Key", "943bc96754mshe84051ff549103ap1db219jsn19262270f0e3" },
                        { "X-RapidAPI-Host", "imdb146.p.rapidapi.com" }
                    }
            };
            HttpResponseMessage responseMessage = await httpClient.SendAsync(request);
            responseMessage.EnsureSuccessStatusCode();
            string json = await responseMessage.Content.ReadAsStringAsync();
            Rootobject movie = JsonConvert.DeserializeObject<Rootobject>(json);
            return movie;
        }
    }
}
