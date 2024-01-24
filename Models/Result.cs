using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Result
    {
        public string id { get; set; }
        public string titleNameText { get; set; }
        public string titleReleaseText { get; set; }
        public string titleTypeText { get; set; }
        public Titleposterimagemodel titlePosterImageModel { get; set; }
        public string[] topCredits { get; set; }
        public string imageType { get; set; }
    }
}