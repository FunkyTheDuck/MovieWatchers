using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Rootobject
    {
        public string[] resultsSectionOrder { get; set; }
        public Findpagemeta findPageMeta { get; set; }
        public Keywordresults keywordResults { get; set; }
        public Titleresults titleResults { get; set; }
        public Nameresults nameResults { get; set; }
        public Companyresults companyResults { get; set; }
    }
}
