using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Entities.System;
using Newtonsoft.Json;

namespace Entities.Website
{
    public class WebsitePage:IDocument
    {

        public Document Document { get; set; }

        [JsonProperty("Id")]
        public Guid Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("CommonStatuses")]
        public CommonStatus CommonStatus { get; set; }


        [JsonProperty("MetaTitle")]
        public string MetaTitle { get; set; }

        [JsonProperty("MetaDescription")]
        public string MetaDescription { get; set; }

        [JsonProperty("Language")]
        public Language Language { get; set; }

        [JsonProperty("HeaderPage")]
        public HeaderPage HeaderPage { get; set; }

        [JsonProperty("Section")]
        public List<Section> Section { get; set; }

        [JsonProperty("IsHomePage")]
        public bool IsHomePage { get; set; }
    }
}
