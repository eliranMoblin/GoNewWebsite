using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Entities.System
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Document : ImageBaseEntity
    {

        [JsonProperty("Id")]
        public Guid Id { get; set; }

        [JsonProperty("Json")]
        public string Json { get; set; }

        [JsonProperty("CreatedDate")]
        public DateTime CreatedDate { get; set; }


        [JsonProperty("DocumentTypeId")]
        public byte DocumentTypeId { get; set; }

        [JsonProperty("IsDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("ModifyDate")]
        public DateTime? ModifyDate { get; set; }

        [JsonProperty("Tstamp")]
        public long? Tstamp { get; set; }

        [JsonIgnore]
        public DocumentType Type
        {
            get => (DocumentType)DocumentTypeId;
            set => DocumentTypeId = (byte)value;
        }

        public dynamic UserObject { get; private set; }

        public T Deserialize<T>(JsonConverter jsonConverter = null)
            where T : IDocument
        {
            if (UserObject != null && UserObject is T)
                return (T)UserObject;
            if (InternalStatus == EntityStatus.New)
            {
                T deserializeObject;
                if (jsonConverter != null)
                {
                    try
                    {
                        deserializeObject = JsonConvert.DeserializeObject<T>(Json, jsonConverter);
                    }
                    catch (InvalidCastException)
                    {
                        deserializeObject = JsonConvert.DeserializeObject<T>(Json);
                    }
                }
                else
                {
                    deserializeObject = JsonConvert.DeserializeObject<T>(Json);
                }

                InternalStatus = EntityStatus.Created;
                UserObject = deserializeObject;
                deserializeObject.Document = this;
                return deserializeObject;
            }
            else
            {
                throw new Exception();
            }
        }

        public override string ToString()
        {
            return $"{Type.Name} - {Type.Value}";
        }


        public Image GetImage(ImageType imageType)
        {
            var image = Images.FirstOrDefault(x => x.ImageType == imageType);
            return image;
        }

    }
}
