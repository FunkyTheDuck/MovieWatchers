using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiModels
{
    public class Movie
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Poster_Url { get; set; }
        public float Popularity { get; set; }

    }
}