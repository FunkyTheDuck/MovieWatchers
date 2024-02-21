using System.Runtime.ExceptionServices;

namespace MovieWatchers.Pages
{
    partial class LogIn
    {
        public bool SigningUp { get; set; } = false;

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if(firstRender)
            {

            }
        }
        public void OnSignUpClick()
        {
            SigningUp = true;
            StateHasChanged();
        }
    }
}