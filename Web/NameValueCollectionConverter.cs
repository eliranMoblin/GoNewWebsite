using System;
using System.Collections.Specialized;
using Newtonsoft.Json;

namespace Web
{
    /// <summary>
    /// Custom converter for (de)serializing NameValueCollection
    /// Add an instance to the settings Converters collection
    /// </summary>
    public class NameValueCollectionConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var collection = value as NameValueCollection;
            if (collection == null)
                return;

            writer.WriteStartObject();
            foreach (var key in collection.AllKeys)
            {
                writer.WritePropertyName(key);
                writer.WriteValue(collection.Get(key));
            }
            writer.WriteEndObject();
        }
            
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var nameValueCollection = new NameValueCollection();
            var key = "";
            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.StartObject)
                {
                    nameValueCollection = new NameValueCollection();
                }
                if (reader.TokenType == JsonToken.EndObject)
                {
                    return nameValueCollection;
                }
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    key = reader.Value.ToString();
                }
                if (reader.TokenType == JsonToken.String)
                {
                    nameValueCollection.Add(key, reader.Value.ToString());
                }
            }
            return nameValueCollection;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(NameValueCollection);
        }
    }
}