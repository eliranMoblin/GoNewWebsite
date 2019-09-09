using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Entities.Website
{
    public class CaseStudyCard:IDocument
    {
        [JsonProperty("Id")]
        public Guid Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("MainImageUrl")]
        public string MainImageUrl { get; set; }

        [JsonProperty("MainImageAlt")]
        public string MainImageAlt { get; set; }

        [JsonProperty("MainLogoUrl")]
        public string MainLogoUrl { get; set; }

        [JsonProperty("MainLogoAlt")]
        public string MainLogoAlt { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("SubTitle")]
        public string SubTitle { get; set; }

        [JsonProperty("Content")]
        public string Content { get; set; }

        [JsonProperty("ClickToRead")]
        public string ClickToRead { get; set; }

        [JsonProperty("Language")]
        public Language Language { get; set; }

        [JsonProperty("Document")]
        public Document Document { get; set; }
    }

}
