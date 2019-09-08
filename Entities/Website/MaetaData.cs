using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Website
{
    public class UniversalSection
    {

        public string IdName { get; set; }

        public string Header { get; set; }

        public string Content { get; set; }

        public int OrderId { get; set; }
    }

    public class HeaderPage
    {
        public string Main { get; set; }

        public string Content { get; set; }
    }
}