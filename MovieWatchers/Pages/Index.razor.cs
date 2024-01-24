using CLFacade;
using Models;

namespace MovieWatchers.Pages
{
    partial class Index
    {
        public Rootobject Movie { get; set; }
        public string SearchQuery { get; set; }

        Repository repo;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if(firstRender)
            {
                repo = new Repository();
            }
        }
        public async Task GetMovieFromSearchQueryAsync()
        {
            if(!string.IsNullOrEmpty(SearchQuery))
            {
                Movie = await repo.GetMovieFromSearchQueryAsync(SearchQuery);
                StateHasChanged();
            }
        }
    }
}