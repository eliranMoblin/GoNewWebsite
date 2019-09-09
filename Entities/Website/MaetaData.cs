using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Website
{
    public class Section
    {

        public string IdName { get; set; }

        public string Header { get; set; }

        public string Content { get; set; }

        public int OrderId { get; set; }

        public SectionType SectionType { get; set; }

        public List<CaseStudyCard> CaseStudyCard { get; set; }

    }

    public class HeaderPage
    {
        public string Main { get; set; }

        public string Content { get; set; }
    }


    

    public enum SectionType
    {
        Universal=1,
        Cols=2,
        Card=3,
        Carousel=4
    }
}
