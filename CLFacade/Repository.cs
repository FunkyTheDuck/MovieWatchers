using DBLayer;
using Models.MovieModels;
using Microsoft.Extensions.Configuration;
using MyModels;
using System.Drawing;
using AForge.Imaging.Filters;
using Accord.MachineLearning;
using Accord.MachineLearning.VectorMachines;
using Accord.MachineLearning.VectorMachines.Learning;
using Accord.Statistics.Kernels;

namespace CLFacade
{
    public class Repository : IRepository
    {
        private DB db;
        private List<string> listOfDominatColors { get; set; }
        public Repository(IConfiguration configuration)
        {
            db = new DB(configuration);
            listOfDominatColors = new();
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

                                                    uiMovie.PosterDominantColor = ColorTranslator.ToHtml(await GetDominantColor(bitmap)).ToString();
                                                    listOfDominatColors.Add(uiMovie.PosterDominantColor);
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
        private async Task<Color> GetDominantColor(Bitmap image)
        {
            // Convert the image to a list of colors
            Color[] pixels = await GetMostUsedColor(image);

            double[][] data = pixels.Select(c => new double[] { c.R, c.G, c.B }).ToArray();
            KMeans kmeans = new KMeans(k: 5); 
            var clusters = kmeans.Learn(data);
            int[] largestCluster = clusters.Decide(data);
            var clusterCounts = largestCluster.GroupBy(x => x).Select(g => new { Value = g.Key, Count = g.Count() });
            var dominantCluster = clusterCounts.OrderByDescending(x => x.Count).First().Value;
            var centroid = clusters.Clusters[dominantCluster].Centroid;
            var dominantColor = Color.FromArgb((int)centroid[0], (int)centroid[1], (int)centroid[2]);

            return dominantColor;
        }
        private async Task<Color[]> GetMostUsedColor(Bitmap bitMap)
        {
            ResizeBicubic filter = new ResizeBicubic(50, 50);
            Bitmap resizedImage = filter.Apply(bitMap);
            Color[] pixels = new Color[resizedImage.Width * resizedImage.Height];
            int index = 0;

            // Iterate through each pixel in the image
            for (int x = 0; x < resizedImage.Width; x++)
            {
                for (int y = 0; y < resizedImage.Height; y++)
                {
                    // Get the color of the current pixel
                    Color pixelColor = resizedImage.GetPixel(x, y);
                    pixels[index++] = pixelColor;
                }
            }

            return pixels;
        }
    }
}