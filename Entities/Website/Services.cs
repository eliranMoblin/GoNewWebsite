using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.System;

namespace Entities.Website
{
    public class Services : IDocument
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }

        public Document Document { get; set; }
    }
}
