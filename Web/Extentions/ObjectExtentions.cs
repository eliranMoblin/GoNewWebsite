using System;
using System.Web;
using System.Web.Mvc;
using Common.ExtentionMethods;
using Entities.Backend;
using Entities.Frontend;

namespace Web.Extentions
{
    public static class ObjectExtentions
    {
        const string LongDateTimeFormat = "yyyy-MM-dd HH:mm:ss";

        const string ShortDateTimeFormat = "yyyy-MM-dd";

        //const string ClientInput = "MM/dd/yyyy";
        const string ClientInput = "MM/dd/yyyy";

        const string TimeZone = "Israel Standard Time";

        public static IHtmlString ToCell(this Country value)
        {
            TagBuilder tag = new TagBuilder("span");
            if (value != null)
            {
                tag.Attributes["Title"] = value.Name;
                tag.SetInnerText($"{value.Name} - {value.Id}");
            }

            var htmlString = MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
            return htmlString;
        }

        public static IHtmlString ToCell(this Entities.Cache.Country value)
        {
            TagBuilder tag = new TagBuilder("span");
            if (value != null)
            {
                tag.Attributes["Title"] = value.Name;
                tag.SetInnerText($"{value.Name} - {value.Id}");
            }

            var htmlString = MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
            return htmlString;
        }

        public static IHtmlString ToCell(this Language value)
        {
            TagBuilder tag = new TagBuilder("span");
            if (value != null)
            {
                tag.Attributes["Title"] = value.Name;
                tag.SetInnerText($"{value.Name} - {value.Id}");
            }

            var htmlString = MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
            return htmlString;
        }

        public static IHtmlString ToCell(this Entities.Cache.Language value)
        {
            TagBuilder tag = new TagBuilder("span");
            if (value != null)
            {
                tag.Attributes["Title"] = value.Name;
                tag.SetInnerText($"{value.Name} - {value.Id}");
            }

            var htmlString = MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
            return htmlString;
        }


        public static IHtmlString ToCell(this DateTime? value)
        {
            TagBuilder tag = new TagBuilder("span");
            if (value.HasValue)
            {
                return value.Value.ToCell();
            }

            var htmlString = MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
            return htmlString;
        }

        public static IHtmlString ToCell(this DateTime value, string timeZone = TimeZone)
        {
            TagBuilder tag = new TagBuilder("span");
            var text = value.ToClientTimeText(timeZone);
            tag.InnerHtml = text;
            tag.Attributes["title"] = text; ;

            var htmlString = MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
            return htmlString;
        }

        public static IHtmlString ToStars(this decimal value)
        {
            TagBuilder tag = new TagBuilder("span");
            tag.AddCssClass("stars");
            if (value > 5)
                value = value / 2;
            //tag.SetInnerText(value.ToString());
            tag.Attributes["data-star"] = value.ToString();
            tag.Attributes["title"] = $"{value} stars";

            TagBuilder hidden = new TagBuilder("span");
            hidden.SetInnerText(value.ToString());
            hidden.Attributes["style"] = "display:none";

            var htmlString = MvcHtmlString.Create(hidden.ToString() + tag.ToString(TagRenderMode.Normal));
            return htmlString;
        }

        public static IHtmlString ToCell(this string value, int maxLenth = 40, bool htmlDecode = false)
        {

            TagBuilder tag = new TagBuilder("span");

            if (!value.IsNullOrWhiteSpace())
            {
                if (htmlDecode)
                {
                    value = HttpUtility.HtmlDecode(value);
                }
                if (value.Length > maxLenth)
                {
                    TagBuilder hiddenTag = new TagBuilder("span");
                    hiddenTag.AddCssClass("hidden");
                    hiddenTag.InnerHtml = value;
                    string hiddenTablString = hiddenTag.ToString(TagRenderMode.Normal);
                    tag.AddCssClass("cell-view");
                    tag.InnerHtml = $"{hiddenTablString}{value.Substring(0, maxLenth - 3)}...";

                }
                else
                {
                    tag.SetInnerText(value);
                }
                tag.Attributes["title"] = HttpUtility.HtmlEncode(value);
            }
            var htmlString = MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
            return htmlString;
        }

        public static IHtmlString ToJsonCell(this string value)
        {
            TagBuilder tag = new TagBuilder("span");

            if (!value.IsNullOrWhiteSpace() && value.Length > 30)
            {
                tag.AddCssClass("json-view");
                tag.SetInnerText($"{value.Substring(0, 30)}...");

                //tag.SetInnerText("...");
                tag.Attributes["title"] = MvcHtmlString.Create(value).ToHtmlString();
            }
            var htmlString = MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
            return htmlString;
        }


        public static IHtmlString ToCellLink(this string value, string target,int chars= 40, string dynamicParameter = null, string displayText = null)
        {
            TagBuilder tag = new TagBuilder("a");
            tag.Attributes["target"] = target;
            if (string.IsNullOrWhiteSpace(displayText))
                displayText = value;
            if (!value.IsNullOrWhiteSpace())
            {
                string url = value.Replace(SystemPlaceHolders.Dynamic, dynamicParameter, StringComparison.CurrentCultureIgnoreCase);
                tag.Attributes["href"] = url;
            }

            if (!displayText.IsNullOrWhiteSpace())
            {
                if (displayText.Length > chars)
                {
                    tag.SetInnerText(displayText.Substring(0, chars-3) + "...");
                }
                else
                {
                    tag.SetInnerText(displayText);
                }
                tag.Attributes["title"] = MvcHtmlString.Create(displayText).ToHtmlString();
            }
            var htmlString = MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
            return htmlString;
        }

        public static string IsSelected(this bool id, string comapreTo)
        {
            var retVal = id.ToString().Equals(comapreTo, StringComparison.CurrentCultureIgnoreCase) ? "selected" : "";
            return retVal;
        }

        public static string IsSelected(this bool? id, string comapreTo)
        {
            if (!id.HasValue)
                return string.Empty;

            return id.Value.IsSelected(comapreTo);
        }

        public static string ToCell(this decimal? value)
        {
            if (!value.HasValue)
                value = 0;

            return value.Value.ToCell();
        }

        public static string ToCell(this decimal value)
        {
            return value == 0 ? "0" : $"{value:##,###.##}";
        }


        public static string ToCell(this int? value)
        {
            if (!value.HasValue)
                value = 0;

            return value.Value.ToCell();
        }


        public static string ToCell(this int value)
        {
            return value == 0 ? "0" : $"{value:##,###}";
        }

        public static string Percentage(this int value, decimal? fromVale)
        {
            decimal retVal;
            if (value == 0 || !fromVale.HasValue || fromVale == 0)
            {
                retVal = 0;
            }
            else
            {
                retVal = (decimal)(value * 1.0) / fromVale.Value;
                retVal = retVal * (decimal)100.0;
            }
            string retVals;
            if (retVal < 1)
            {
                retVals = $"{retVal.ToString("0.##")}%";
            }
            else
            {
                retVals = $"{retVal.ToString("##,###.##")}%";
            }
            return retVals;
        }

        #region Date Region

        public static DateTime? ToUtcDateTime(this DateTime? dateTime, string timeZone = TimeZone)
        {
            return dateTime?.ToUtcDateTimeZone(timeZone);
        }

        public static string ToArticleString(this DateTime? dateTime)
        {
            return dateTime?.ToArticleString();
        }

        public static string ToArticleString(this DateTime dateTime)
        {
            string format = "MMMM dd, yyyy hh:mm";
            if (dateTime.TimeOfDay == TimeSpan.Zero)
                format = "MMMM dd, yyyy";

            return dateTime.ToString(format);
        }

        public static string ToClientInputDate(this DateTime? dateTime, string timeZone = TimeZone)
        {
            return dateTime?.ToClientInputDate(timeZone);
        }

        public static string ToClientTimeText(this DateTime? dateTime, string timeZone = TimeZone)
        {
            return dateTime?.ToClientTimeText(timeZone);
        }

        public static string ToShortClientTime(this DateTime? dateTime, string timeZone = TimeZone)
        {
            return dateTime?.ToShortClientTime(timeZone);
        }

        public static DateTime? ToClientTimeZone(this DateTime? dateTime, string timeZone = TimeZone)
        {
            return dateTime?.ToClientTimeZone(timeZone);
        }

        public static string ToClientInputDate(this DateTime dateTime, string timeZone = TimeZone)
        {
            var localDateTime = dateTime.ToClientTimeZone(timeZone);
            var retVal = localDateTime.ToString(ClientInput);
            //var retVal = localDateTime.ToString("yyyy-MM-dd");
            return retVal;
        }


        public static string ToInputDate(this DateTime? dateTime, string timeZone = TimeZone)
        {
            return dateTime?.ToInputDate(timeZone);
        }
        public static string ToInputDate(this DateTime dateTime, string timeZone = TimeZone)
        {
            var localDateTime = dateTime.ToClientTimeZone(timeZone);
            var retVal = localDateTime.ToString("yyyy-MM-dd");
            return retVal;
        }

        public static string ToClientTimeText(this DateTime dateTime, string timeZone = TimeZone)
        {
            var clientTime = dateTime.ToClientTimeZone(timeZone);
            var retVal = clientTime.ToString(LongDateTimeFormat);
            return retVal;
        }

        public static string ToShortClientTime(this DateTime dateTime, string timeZone = TimeZone)
        {
            var localDateTime = dateTime.ToClientTimeZone(timeZone);
            return localDateTime.ToString(ShortDateTimeFormat);
        }

        public static string ToShortDate(this DateTime dateTime)
        {
            return dateTime.ToString(ShortDateTimeFormat);
        }

        public static string ToShortDate(this DateTime? dateTime)
        {
            return dateTime != null && dateTime.Value != DateTime.MinValue ? dateTime.Value.ToShortDate() : string.Empty;
        }
        public static string ToLongDate(this DateTime dateTime)
        {
            var clientTime = dateTime.ToClientTimeZone();
            return clientTime.ToString(LongDateTimeFormat);
        }

        public static string ToLongDate(this DateTime? dateTime)
        {
            return dateTime != null && dateTime.Value != DateTime.MinValue ? dateTime.Value.ToShortDate() : string.Empty;
        }
        public static DateTime ToClientTimeZone(this DateTime dateTime, string timeZone = TimeZone)
        {
            TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZone);

            dateTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
            var localDateTime = TimeZoneInfo.ConvertTimeFromUtc(dateTime, timeZoneInfo);
            return localDateTime;
        }

        public static DateTime ToUtcDateTimeZone(this DateTime dateTime, string timeZone = TimeZone)
        {
            TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZone);
            dateTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Unspecified);
            var localDateTime = TimeZoneInfo.ConvertTimeToUtc(dateTime, timeZoneInfo);
            return localDateTime;
        }

        #endregion

    }
}