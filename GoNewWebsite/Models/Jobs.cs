using System;

using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GoNewWebsite.Models
{

    public partial class Jobs
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("department")]
        public object Department { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("email_alias")]
        public string EmailAlias { get; set; }

        [JsonProperty("url_comeet_hosted_page")]
        public Uri UrlComeetHostedPage { get; set; }

        [JsonProperty("url_active_page")]
        public Uri UrlActivePage { get; set; }

        [JsonProperty("employment_type")]
        public string EmploymentType { get; set; }

        [JsonProperty("experience_level")]
        public string ExperienceLevel { get; set; }

        [JsonProperty("uid")]
        public string Uid { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("internal_use_custom_id")]
        public object InternalUseCustomId { get; set; }

        [JsonProperty("is_consent_needed")]
        public bool IsConsentNeeded { get; set; }

        [JsonProperty("referrals_reward")]
        public object ReferralsReward { get; set; }

        [JsonProperty("is_reward")]
        public bool IsReward { get; set; }

        [JsonProperty("is_company_reward")]
        public bool IsCompanyReward { get; set; }

        [JsonProperty("company_referrals_reward")]
        public string CompanyReferralsReward { get; set; }

        [JsonProperty("url_detected_page")]
        public object UrlDetectedPage { get; set; }

        [JsonProperty("picture_url")]
        public object PictureUrl { get; set; }

        [JsonProperty("time_updated")]
        public DateTimeOffset TimeUpdated { get; set; }

        [JsonProperty("company_name")]
        public string CompanyName { get; set; }

        [JsonProperty("position_url")]
        public Uri PositionUrl { get; set; }

        [JsonProperty("details")]
        public Detail[] Details { get; set; }
    }

    public partial class Detail
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("order")]
        public long Order { get; set; }
    }

    public partial class Location
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("postal_code")]
        public string PostalCode { get; set; }

        [JsonProperty("street_name")]
        public string StreetName { get; set; }

        [JsonProperty("arrival_instructions")]
        public object ArrivalInstructions { get; set; }

        [JsonProperty("street_number")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long StreetNumber { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("location_uid")]
        public string LocationUid { get; set; }
    }


    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}