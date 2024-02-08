using DBLayer;
using Models.MovieModels;
using Microsoft.Extensions.Configuration;
using MyModels;
using System.Drawing;

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
                            try
                            {
                                using (var httpClient = new HttpClient())
                                {
                                    using (var response = await httpClient.GetAsync(uiMovie.Poster_Url))
                                    {
                                        if (response.IsSuccessStatusCode)
                                        {
                                            using (var stream = await response.Content.ReadAsStreamAsync())
                                            {
                                                using (var memoryStream = new MemoryStream())
                                                {
                                                    await stream.CopyToAsync(memoryStream);
                                                    memoryStream.Seek(0, SeekOrigin.Begin);

                                                    Bitmap bitmap = new Bitmap(memoryStream);

                                                    uiMovie.PosterDominantColor = ColorTranslator.ToHtml(await GetMostUsedColor(bitmap)).ToString();
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Failed to download image: " + response.StatusCode);
                                        }
                                    }
                                }
                            }
                            catch (Exception ex) 
                            { 
                                string t = ex.ToString();
                            }
                            //Color temp2 = await GetMostUsedColor(temp);
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
        private async Task<Color> GetMostUsedColor(Bitmap bitMap)
        {
            var colorIncidence = new Dictionary<int, int>();
            for (var x = 0; x < bitMap.Size.Width; x++)
                for (var y = 0; y < bitMap.Size.Height; y++)
                {
                    var pixelColor = bitMap.GetPixel(x, y).ToArgb();
                    if (colorIncidence.Keys.Contains(pixelColor))
                        colorIncidence[pixelColor]++;
                    else
                        colorIncidence.Add(pixelColor, 1);
                }
            return Color.FromArgb(colorIncidence.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value).First().Key);
        }
    }
}