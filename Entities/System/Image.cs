using System;
using Newtonsoft.Json;

namespace Entities.System
{
    [JsonObject(MemberSerialization.OptIn)]
    public partial class Image : ImageBaseEntity
    {
        [JsonProperty("AlternateText")]
        public string AlternateText { get; set; }

        [JsonProperty("BackgroundColor")]
        public string BackgroundColor { get; set; }

        [JsonProperty("ChildImages")]
        public int? ChildImages { get; set; }

        [JsonProperty("CloudFileId")]
        public Guid CloudFileId { get; set; }

        [JsonProperty("ContentType")]
        public string ContentType { get; set; }

        [JsonProperty("CreatedDate")]
        public DateTime CreatedDate { get; set; }

        [JsonProperty("DocumentId")]
        public Guid? DocumentId { get; set; }

        [JsonProperty("Height")]
        public int? Height { get; set; }

        [JsonProperty("Id")]
        public Guid Id { get; set; }

        [JsonProperty("ImageTypeId")]
        public byte? ImageTypeId { get; set; }

        [JsonProperty("IsDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("ModifyDate")]
        public DateTime? ModifyDate { get; set; }

        [JsonProperty("NewFileName")]
        public string NewFileName { get; set; }

        [JsonProperty("Options")]
        public int? Options { get; set; }

        [JsonProperty("OriginalFileName")]
        public string OriginalFileName { get; set; }

        [JsonProperty("ParentImageId")]
        public Guid? ParentImageId { get; set; }

        [JsonProperty("Size")]
        public int? Size { get; set; }

        [JsonProperty("ThemeId")]
        public byte? ThemeId { get; set; }

        [JsonProperty("Tstamp")]
        public long? Tstamp { get; set; }

        [JsonProperty("Url")]
        public string Url { get; set; }

        [JsonProperty("Width")]
        public int? Width { get; set; }

        [JsonProperty("Hash")]
        public string Hash { get; set; }

        public ImageOptions? ImageOptions
        {
            get
            {
                if (Options == null)
                {
                    return null;
                }
                return (ImageOptions)Options;
            }
            set => Options = (int?)value;
        }


        public ImageType ImageType
        {
            get
            {
                if (!ImageTypeId.HasValue)
                    return ImageType.Other;

                return (ImageType)ImageTypeId;
            }
        }


        public Theme? Theme
        {
            get
            {
                if (!ThemeId.HasValue)
                    return Entities.Theme.NoTheme;

                return (Theme?)ThemeId;
            }
            set => ThemeId = (byte?)value;
        }

        //public List<Image> Images { get; set; }
    }
}
