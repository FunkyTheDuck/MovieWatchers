using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components
{
    partial class LogInComp
    {
        [Parameter]
        public EventCallback SignInEvent { get; set; }
        public async void SignInClickedAsync()
        {
            await SignInEvent.InvokeAsync();
        }
    }
}