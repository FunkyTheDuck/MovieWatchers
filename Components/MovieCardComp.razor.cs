using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MyModels;

namespace Components
{
    partial class MovieCardComp
    {
        [Parameter]
        public Movie movie { get; set; }

    }
}