using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Titleresults
    {
        public Result[] results { get; set; }
        public string nextCursor { get; set; }
        public bool hasExactMatches { get; set; }
    }
}