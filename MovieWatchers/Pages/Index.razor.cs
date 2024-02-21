using CLFacade;
using Microsoft.AspNetCore.Components;
using Models.MovieModels;
using MyModels;
using System.Linq;

namespace MovieWatchers.Pages
{
    partial class Index
    {
        public List<Movie> Movies { get; set; }
        public string SearchQuery { get; set; }
        public int MovieListCount { get; set; }

        [Inject]
        IRepository repo { get; set; }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if(firstRender)
            {

            }
        }
        public async Task GetMovieFromSearchQueryAsync()
        {
            if(!string.IsNullOrEmpty(SearchQuery))
            {
                Movies = await repo.GetMoviesFromSearchQueryAsync(SearchQuery);
                Movies = Movies.OrderBy(x => x.Popularity).ToList();
                if(Movies.Count < 8)
                {
                    MovieListCount = 7;
                }
                else if(Movies.Count < 15)
                {
                    MovieListCount = 14;
                }
                else
                {
                    MovieListCount = 21;
                }
                StateHasChanged();
            }
        }
    }
}