using System;
using Common;
using Entities.System;
using Newtonsoft.Json.Linq;

namespace Entities.Documents
{
    public class CreationConverter : JsonCreationConverter<IDocument>
    {
        public bool ParseType { get; set; }

            
        public CreationConverter()
        {
            ParseType = true;
        }

        protected override IDocument Create(Type objectType, JObject jsonObject)
        {
            var jtype = jsonObject["$type"];
            if (jtype == null)
            {
                return (IDocument)Activator.CreateInstance(objectType);
            }

            var typeName = jtype.ToString();
            var type = Type.GetType(typeName);
            if (type == null)
            {
                return (IDocument)Activator.CreateInstance(objectType);
            }
            //return null;
            //throw  new Exception($"Unknown Type {typeName}");

            var sss = Activator.CreateInstance(type);
            return (IDocument)sss;
        }
    }
}
