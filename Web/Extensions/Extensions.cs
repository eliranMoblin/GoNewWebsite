using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Common.ExtentionMethods;
using Entities;
using Entities.Cache;

namespace Web.Extensions
{
    public enum ModalSize
    {
        Default = 0,
        Small = 1,
        Large = 2,
        FullScreen = 3,
    }

    public enum FontAwesomeSize
    {
        fa1x = 1,
        fa2X = 2,
        fa3X = 3,
    }

    public enum FormStyle
    {
        Horizontal = 1,
        Inline = 2,
    }

    public enum InputType
    {
        text,
        password,
        number,
        url,
        date,
        month,
        datetime_local,
        file,
    }

    public enum DateInputFormat
    {
        Date,
        DateTime,
        Month
    }

    public static class GlobalExtensions
    {

        public static byte[] GetBytes(this HttpPostedFileBase file)
        {
            file.InputStream.Position = 0;
            if (file.ContentType.Contains("txt", StringComparison.CurrentCultureIgnoreCase))
            {
                //text file need to be read as UTF8
                using (TextReader textReader = new StreamReader(file.InputStream))
                {
                    string text = textReader.ReadToEnd();
                    var bytes = Encoding.UTF8.GetBytes(text);
                    return bytes;
                }
            }
            else
            {
                using (MemoryStream target = new MemoryStream())
                {
                    file.InputStream.CopyTo(target);
                    byte[] data = target.ToArray();
                    return data;
                }
            }
        }

        //public static IHtmlString GetImageCell(this BaseEntity item, ImageType? imageType)
        //{
        //    int? height = 60;
        //    int? width = 100;
        //    var span = new TagBuilder("span");
        //    span.Attributes["style"] = $"width:{width}px";
        //    if (item != null)
        //    {
        //        Image image;
        //        if (item is Image)
        //        {
        //            image = (Image)item;
        //        }
        //        else
        //        {
        //            image = item.Images.Where(i => !imageType.HasValue || i.ImageTypeId == (int)imageType).RandomOrDefault();
        //        }
        //        var tag = new TagBuilder("img");
        //        var imgStyle = string.Empty;
        //        if (image != null)
        //        {
        //            tag.Attributes["id"] = image.Id.ToString();
        //            tag.Attributes["src"] = @"/img/loading.gif";
        //            tag.Attributes["data-src"] = image.Url;
        //            //tag.Attributes["alt"] = image.OriginalFileName;
        //            if (!image.BackgroundColor.IsNullOrWhiteSpace())
        //            {
        //                imgStyle += $"background-color: {image.BackgroundColor};";
        //            }
        //            if (image.Width.HasValue && image.Height.HasValue)
        //            {
        //                tag.Attributes["onmouseover"] = "changeImgSize(this)";
        //                tag.Attributes["onmouseout"] = "changeImgSize(this)";
        //                tag.Attributes["data-width"] = image.Width.ToString();
        //                tag.Attributes["data-height"] = image.Height.ToString();
        //            }
        //        }


        //        imgStyle += $"max-height: {height}px;max-width: {width}px;";
        //        tag.Attributes["style"] = imgStyle;

        //        span.InnerHtml = tag.ToString(TagRenderMode.Normal);
        //    }
        //    else
        //    {
        //        span.SetInnerText($"Unable to find '{imageType}' Image");
        //    }

        //    return MvcHtmlString.Create(span.ToString());
        //}

        //public static IHtmlString AddHiddenProperties(this WebViewPage webViewPage, string prefix = "")
        //{
        //    if (webViewPage.Model != null)
        //    {
        //        return webViewPage.Model.AddHiddenProperties(prefix);
        //    }
        //    var wrapperDiv = new TagBuilder("div");
        //    wrapperDiv.Attributes["class"] = "generated-properties";
        //    var retVal = MvcHtmlString.Create(wrapperDiv.ToString());
        //    return retVal;
        //}

        //public static IHtmlString AddHiddenProperties(this object model, string prefix = "")
        //{
        //    if (model == null)
        //        return null;
        //    StringBuilder hiddenInputs = new StringBuilder();
        //    if (!string.IsNullOrWhiteSpace(prefix))
        //    {
        //        prefix = prefix.Trim('.');
        //        prefix = prefix + ".";
        //    }
        //    foreach (var prop in model.GetType().GetProperties())
        //    {
        //        if (prop.PropertyType.IsArray)
        //            continue;
        //        if (prop.Name.Equals("Tstamp", StringComparison.CurrentCultureIgnoreCase))
        //            continue;

        //        if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
        //            continue;
        //        if (prop.PropertyType.FullName.StartsWith("Entities") && !prop.PropertyType.IsEnum)
        //            continue;

        //        if (!prop.PropertyType.FullName.Contains("Entities.Backend"))
        //        {
        //            var value = prop.GetValue(model, null);
        //            if (value == null)
        //            {
        //                continue;
        //            }
        //            var valueString = value?.ToString() ?? string.Empty;
        //            if (valueString.Equals("System.Dynamic.ExpandoObject", StringComparison.CurrentCultureIgnoreCase))
        //            {
        //                continue;
        //            }
        //            if (prop.PropertyType.IsEnum)
        //            {
        //                var e = (Enum)value;
        //                var code = e.GetTypeCode();
        //                valueString = Convert.ChangeType(value, code).ToString();
        //            }
        //            var hiddenInput = new TagBuilder("input");
        //            hiddenInput.Attributes["type"] = "hidden";

        //            hiddenInput.Attributes["name"] = $"{prefix}{prop.Name}";

        //            hiddenInput.Attributes["value"] = valueString;
        //            hiddenInputs.AppendLine(hiddenInput.ToString());
        //        }
        //    }

        //    var wrapperDiv = new TagBuilder("div");
        //    wrapperDiv.Attributes["class"] = "generated-properties";
        //    wrapperDiv.InnerHtml = hiddenInputs.ToString();
        //    if (model is IDocument)
        //    {
        //        var iDocument = (IDocument)model;
        //        if (iDocument.Document != null)
        //        {
        //            var properties = AddHiddenProperties(iDocument.Document, "Document");
        //            wrapperDiv.InnerHtml += properties.ToHtmlString();
        //        }

        //    }
        //    var retVal = MvcHtmlString.Create(wrapperDiv.ToString());
        //    return retVal;
        //}

        public static IHtmlString FileInput(this WebViewPage webViewPage)
        {
            /*
             <div class="fileinput fileinput-new" data-provides="fileinput">
                <div class="fileinput-preview thumbnail" data-trigger="fileinput" style="width: 200px; height: 150px;"></div>
                <div>
                    <span class="btn red btn-outline btn-file">
                        <span class="fileinput-new"> Select image </span>
                        <span class="fileinput-exists"> Change </span>
                        <input type="file" name="...">
                    </span>
                    <a href="javascript:;" class="btn red fileinput-exists" data-dismiss="fileinput"> Remove </a>
                </div>
            </div>
            */

            var wrapperDiv = new TagBuilder("div");
            wrapperDiv.AddCssClass("fileinput");
            wrapperDiv.AddCssClass("fileinput-new");
            wrapperDiv.Attributes["data-provides"] = "fileinput";
            var wrapperDiv2 = new TagBuilder("div");
            wrapperDiv2.AddCssClass("fileinput-preview");
            wrapperDiv2.AddCssClass("thumbnail");
            wrapperDiv2.Attributes["data-trigger"] = "fileinput";
            //wrapperDiv2.Attributes["style"] = "width: 200px; height: 150px;";

            var emptyDiv = new TagBuilder("div");
            var wrapperSpan = new TagBuilder("span");
            wrapperSpan.AddCssClass("btn");
            wrapperSpan.AddCssClass("btn-outline");
            wrapperSpan.AddCssClass("btn-file");
            wrapperSpan.AddCssClass("red");
            var span1 = new TagBuilder("span");
            span1.AddCssClass("fileinput-new");
            span1.SetInnerText(" Select image ");

            var span2 = new TagBuilder("span");
            span2.AddCssClass("fileinput-exists");
            span2.SetInnerText(" Change ");

            var input = new TagBuilder("input");
            input.Attributes["type"] = "file";
            input.Attributes["name"] = "...";

            wrapperSpan.InnerHtml += span1.ToString();
            wrapperSpan.InnerHtml += span2.ToString();
            wrapperSpan.InnerHtml += input.ToString();

            var a = new TagBuilder("a");
            a.Attributes["href"] = "javascript:;";
            a.AddCssClass("btn");
            a.AddCssClass("red");
            a.AddCssClass("fileinput-exists");
            a.Attributes["data-dismiss"] = "fileinput";
            a.SetInnerText(" Remove ");

            emptyDiv.InnerHtml += wrapperSpan.ToString();
            emptyDiv.InnerHtml += a.ToString();


            wrapperDiv.InnerHtml += wrapperDiv2.ToString();
            wrapperDiv.InnerHtml += emptyDiv.ToString();


            var retVal = MvcHtmlString.Create(wrapperDiv.ToString());
            return retVal;
        }

    
        public static IHtmlString GetExternalLink(this string url)
        {
            var aHref = new TagBuilder("a");
            aHref.Attributes["target"] = "_blank";
            if (!url.IsNullOrWhiteSpace())
            {
                aHref.Attributes["href"] = url;
                aHref.Attributes["title"] = $"Go to {url}";

            }
            else
            {
                aHref.AddCssClass("disabled");

            }
            aHref.Attributes["target"] = "_blank";
            aHref.AddCssClass("fa");
            aHref.AddCssClass("fa-2x");
            aHref.AddCssClass("fa-external-link");

            var retVal = MvcHtmlString.Create(aHref.ToString());
            return retVal;
        }

        //public static IHtmlString GetReviewLink(this string name)
        //{
        //    var aHref = new TagBuilder("a");
        //    aHref.Attributes["target"] = "_blank";
        //    if (!url.IsNullOrWhiteSpace())
        //    {
        //        aHref.Attributes["href"] = url;
        //        aHref.Attributes["target"] = "_blank";
        //        aHref.Attributes["title"] = $"Read Review of {url}";
        //        aHref.AddCssClass("fa");
        //        aHref.AddCssClass("fa-2x");
        //        aHref.AddCssClass("fa-external-link");
        //    }


        //    var retVal = MvcHtmlString.Create(aHref.ToString());
        //    return retVal;
        //}

        //public static IHtmlString GetLink(this string url)
        //{
        //    var aHref = new TagBuilder("a");
        //    aHref.Attributes["target"] = "_blank";
        //    if (!url.IsNullOrWhiteSpace())
        //    {
        //        aHref.Attributes["href"] = url;
        //        aHref.Attributes["target"] = "_blank";
        //        aHref.Attributes["title"] = $"Go to {url}";
        //        aHref.AddCssClass("fa");
        //        aHref.AddCssClass("fa-2x");
        //        aHref.AddCssClass("fa-external-link");
        //    }


        //    var retVal = MvcHtmlString.Create(aHref.ToString());
        //    return retVal;
        //}

        public static IHtmlString DynamicAction(this WebViewPage webViewPage, string title, string btnSubmitText)
        {
            var modalTag = new TagBuilder("a");
            modalTag.AddCssClass("dynamicAction");
            if (!string.IsNullOrWhiteSpace(title))
                modalTag.Attributes.Add("modal-title", title);

            if (!string.IsNullOrWhiteSpace(btnSubmitText))
                modalTag.Attributes.Add("modal-submit", btnSubmitText);

            var retVal = MvcHtmlString.Create(modalTag.ToString());
            return retVal;
        }

        public static string ValueOrEmpty(this int? obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            return obj.Value.ToString();
        }

        public static dynamic Value(this object obj)
        {
            dynamic dyn = obj;
            if (dyn is DBNull)
            {
                return null;
            }
            else if (obj is int?)
            {

            }
            return obj;
        }
    }
}
