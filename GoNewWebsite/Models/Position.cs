using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace GoNewWebsite.Models
{

    public  class Position
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
        public object ExperienceLevel { get; set; }

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

        [JsonProperty("questionnaires")]
        public object[] Questionnaires { get; set; }
    }

  

  
}