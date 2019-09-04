using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Entities.System
{
    public interface IDocument
    {
        [JsonIgnore]
        Document Document { get; set; }
    }
}
