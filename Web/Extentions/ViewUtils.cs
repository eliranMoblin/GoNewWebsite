using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Entities.Backend;
using Entities.Frontend;
using Common.ExtentionMethods;
using Entities;
using Image = Entities.Cache.Image;

namespace Web.Extentions
{
    public static class ViewUtils
    {
        public static Image GetBestImage(this Image image)
        {
            if (image == null)
                return null;
            var compressedImage = image.Images.FirstOrDefault(i => i.Options.HasValue && i.ImageOptions.Value.HasFlag(ImageOptions.GoogleCompression));

            if (compressedImage != null)
            {
                return compressedImage;
            }
            return image;
        }

        public static IHtmlString GetImageTag(this Image image, bool lazyLoad, bool showDebug, string title = null, string cssClass = null, bool marginTop = false)
        {
            //@media(min-width:320px)  { /* smartphones, iPhone, portrait 480x320 phones */ }
            //@media(min-width:481px)  { /* portrait e-readers (Nook/Kindle), smaller tablets @ 600 or @ 640 wide. */ }
            //@media(min-width:641px)  { /* portrait tablets, portrait iPad, landscape e-readers, landscape 800x480 or 854x480 phones */ }
            //@media(min-width:961px)  { /* tablet, landscape iPad, lo-res laptops ands desktops */ }
            //@media(min-width:1025px) { /* big landscape tablets, laptops, and desktops */ }
            //@media(min-width:1281px) { /* hi-res laptops and desktops */ }
            var pictureTag = new TagBuilder("picture");
            Image fallbackImage = null;
            Image mobileImage = null;
            Image tabletImage = null;
            if (image != null)
            {
                foreach (var childImage in image.Images)
                {
                    if (childImage.ImageOptions.HasFlag(ImageOptions.Mobile))
                    {
                        mobileImage = childImage;
                    }
                    else if (childImage.ImageOptions.HasFlag(ImageOptions.Tablet))
                    {
                        tabletImage = childImage;
                    }
                    else if (childImage.ImageOptions.HasFlag(ImageOptions.GoogleCompression) && fallbackImage == null)
                    {
                        fallbackImage = childImage;
                    }
                }
                if (fallbackImage == null)
                    fallbackImage = image;
            }


            var img = new TagBuilder("img");
            if (fallbackImage != null)
            {
                var desktopSource = new TagBuilder("source");
                desktopSource.Attributes["media"] = "(min-width: 961px)";
                if (showDebug)
                {
                    desktopSource.Attributes["debug-id"] = fallbackImage.Id.ToString();
                    desktopSource.Attributes["debug-parentid"] = fallbackImage.ParentImageId.ToString();
                }

                desktopSource.Attributes["srcset"] = Uri.EscapeUriString(fallbackImage.Url);
                pictureTag.InnerHtml += desktopSource.ToString(TagRenderMode.SelfClosing);

                if (tabletImage != null)
                {
                    var tabletSource = new TagBuilder("source");
                    tabletSource.Attributes["media"] = "(min-width: 641px)";
                    tabletSource.Attributes["srcset"] = Uri.EscapeUriString(tabletImage.Url);
                    if (marginTop)
                        tabletSource.Attributes["style"] = $"margin-top: -{tabletImage.Height / 2}px;";
                    if (showDebug)
                    {
                        tabletSource.Attributes["debug-id"] = tabletImage.Id.ToString();
                        tabletSource.Attributes["debug-parentid"] = tabletImage.ParentImageId.ToString();
                    }
                    pictureTag.InnerHtml += tabletSource.ToString(TagRenderMode.SelfClosing);
                }
                if (mobileImage != null)
                {
                    var mobileSource = new TagBuilder("source");
                    mobileSource.Attributes["media"] = "(min-width: 320px)";
                    mobileSource.Attributes["srcset"] = Uri.EscapeUriString(mobileImage.Url);
                    if (marginTop)
                        mobileSource.Attributes["style"] = $"margin-top: -{mobileImage.Height / 2}px;";
                    if (showDebug)
                    {
                        mobileSource.Attributes["debug-id"] = mobileImage.Id.ToString();
                        mobileSource.Attributes["debug-parentid"] = mobileImage.ParentImageId.ToString();
                    }

                    pictureTag.InnerHtml += mobileSource.ToString(TagRenderMode.SelfClosing);
                }
                if (!string.IsNullOrWhiteSpace(title))
                    img.Attributes["title"] = title;
                else
                    img.Attributes["title"] = image.AlternateText;
                if (!string.IsNullOrWhiteSpace(cssClass))
                    img.AddCssClass(cssClass); ;
                if (marginTop)
                    img.Attributes["style"] = $"margin-top: -{fallbackImage.Height / 2}px;";
                //img.Attributes["height"] = $"{fallbackImage.Height}px";
                //img.Attributes["width"] = $"{fallbackImage.Width}px";

                if (!string.IsNullOrWhiteSpace(image.AlternateText))
                {
                    img.Attributes["alt"] = image.AlternateText;
                }
                if (string.IsNullOrWhiteSpace(image.AlternateText) && string.IsNullOrWhiteSpace(title))
                {
                    img.Attributes["alt"] = image.OriginalFileName;
                }
                img.Attributes[lazyLoad ? "data-src" : "SRC"] = Uri.EscapeUriString(fallbackImage.Url);

                if (!string.IsNullOrWhiteSpace(image.BackgroundColor))
                {
                    img.Attributes["style"] = $"background-color: {image.BackgroundColor}";
                }
                if (lazyLoad)
                {
                    img.AddCssClass("js-lazy-image");
                }
            }

            pictureTag.InnerHtml += img.ToString(TagRenderMode.SelfClosing);
            var picTag = pictureTag.ToString(TagRenderMode.Normal);
            return MvcHtmlString.Create(picTag);
        }

        public static IHtmlString GetLink(this AffiliateProgram affiliateProgram)
        {
            var tag = new TagBuilder("a");
            if (affiliateProgram == null)
            {
                return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
            }
            var retVal = GetAffiliateProgramLink(affiliateProgram.Id, affiliateProgram.Name);

            return retVal;
        }

        public static IHtmlString GetLink(this Entities.Cache.AffiliateProgram affiliateProgram)
        {
            var tag = new TagBuilder("a");
            if (affiliateProgram == null)
            {
                return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
            }

            var retVal = GetAffiliateProgramLink(affiliateProgram.Id, affiliateProgram.Name);
            return retVal;
        }

        public static IHtmlString GetLink(this Brand brand)
        {
            var tag = new TagBuilder("a");
            if (brand == null)
            {
                return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
            }

            var retVal = GetBrandLink(brand.Id, brand.Name);
            return retVal;
        }

        public static IHtmlString GetLink(this Entities.Cache.Brand brand)
        {
            var tag = new TagBuilder("a");
            if (brand == null)
            {
                return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
            }
            var retVal = GetBrandLink(brand.Id, brand.Name);

            return retVal;
        }

        public static IHtmlString GetAffiliateProgramLink(this Entities.Cache.Brand brand)
        {
            var tag = new TagBuilder("a");
            if (brand == null)
            {
                return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
            }
            var retVal = GetAffiliateProgramLink(brand.AffiliateProgramId, brand.AffiliateProgram);

            return retVal;
        }

        public static IHtmlString GetLink(this Promotion promotion)
        {
            var tag = new TagBuilder("a");
            if (promotion == null)
            {
                return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
            }
            tag.Attributes["href"] = $"{Controllers.Brands}/Details/{promotion.BrandId}#promotions";
            tag.Attributes["title"] = promotion.Name;
            tag.SetInnerText(promotion.Name);
            var editButton = promotion.EditButton(controller: Controllers.Promotions, action: "Edit",
                title: $"Edit Promotion '{promotion.Name}'", fontSize: FontAwesomeSize.fa1x, modalSize: ModalSize.Large);

            var retVal = MvcHtmlString.Create($"{editButton.ToHtmlString()} {tag.ToString(TagRenderMode.Normal)}");

            return retVal;
        }

        public static IHtmlString GetBrandLink(this Entities.Cache.Promotion promotion)
        {
            var tag = new TagBuilder("a");
            if (promotion == null)
            {
                return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
            }
            var retVal = GetBrandLink(promotion.BrandId, promotion.Brand);

            return retVal;
        }

        public static IHtmlString GetAffiliateProgramLink(this Entities.Cache.Promotion promotion)
        {
            var tag = new TagBuilder("a");
            if (promotion == null)
            {
                return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
            }
            var retVal = GetAffiliateProgramLink(promotion.AffiliateProgramId, promotion.AffiliateProgram);

            return retVal;
        }

        public static IHtmlString GetLink(this Entities.Cache.Promotion promotion)
        {
            var tag = new TagBuilder("a");
            if (promotion == null)
            {
                return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
            }
            tag.Attributes["href"] = $"{Controllers.Brands}/Details/{promotion.BrandId}#promotions";
            tag.Attributes["title"] = promotion.Name;
            tag.SetInnerText(promotion.Name);
            var editButton = promotion.EditButton(controller: Controllers.Promotions, action: "Edit",
                title: $"Edit Promotion '{promotion.Name}'", fontSize: FontAwesomeSize.fa1x, modalSize: ModalSize.Large);

            var retVal = MvcHtmlString.Create($"{editButton.ToHtmlString()} {tag.ToString(TagRenderMode.Normal)}");


            return retVal;
        }

        public static IHtmlString GetLink(this Game game)
        {
            var tag = new TagBuilder("a");
            if (game == null)
            {
                return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
            }

            return GetGameLink(game.Name, game.Id);
        }

        public static IHtmlString GetLink(this Entities.Cache.Game game)
        {
            var tag = new TagBuilder("a");

            if (game == null)
            {
                return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
            }
            return GetGameLink(game.Name, game.Id);
        }

        public static IHtmlString GetGameLink(string name, int id)
        {
            var tag = new TagBuilder("a");
            tag.Attributes["href"] = $"{Controllers.Games}/?dts={name}";
            tag.Attributes["title"] = name;
            tag.InnerHtml = $"{name}";

            var editButton = name.EditButton(controller: Controllers.Games, id: id, action: "Edit",
                title: $"Edit Game '{name}'", fontSize: FontAwesomeSize.fa1x, modalSize: ModalSize.Large);
            var retVal = MvcHtmlString.Create($"{editButton.ToHtmlString()} {tag.ToString(TagRenderMode.Normal)}");
            return retVal;
        }

        public static IHtmlString GetAffiliateProgramLink(int? id, string name)
        {
            var tag = new TagBuilder("a");
            if (id.HasValue)
            {
                tag.Attributes["href"] = $"{Controllers.AffiliatePrograms}/Details/{id}";
                tag.Attributes["title"] = name;
                tag.SetInnerText(name.First(30));
            }

            var retVal = MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
            return retVal;
        }

        public static IHtmlString GetAffiliateProgramAccountLink(int? id, string name)
        {
            var tag = new TagBuilder("a");
            if (id.HasValue)
            {
                tag.Attributes["href"] = $"{Controllers.AffiliateProgramAccounts}/Details/{id}";
                tag.Attributes["title"] = name;
                tag.SetInnerText(name.First(30));
            }

            var retVal = MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
            return retVal;
        }

        public static IHtmlString GetBrandLink(int? id, string name)
        {
            var tag = new TagBuilder("a");
            if (id.HasValue)
            {
                tag.Attributes["href"] = $"{Controllers.Brands}/Details/{id}";
                tag.Attributes["title"] = name;
                tag.SetInnerText(name.First(30));
            }

            var retVal = MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
            return retVal;
        }

        public static IHtmlString GetBrandLinkLink(int? id, string name)
        {
            var tag = new TagBuilder("a");
            if (id.HasValue)
            {
                tag.Attributes["href"] = $"{Controllers.Brands}/Details/{id}#";
                tag.Attributes["title"] = name;
                tag.SetInnerText(name.First(30));
            }

            var retVal = MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
            return retVal;
        }

        public static IHtmlString GetPromotionLink(int? brandId, int? promotionId, string name)
        {
            var tag = new TagBuilder("a");
            if (brandId.HasValue)
            {
                var hiddenSpan = new TagBuilder("span");
                hiddenSpan.Attributes["style"] = "display:none";
                hiddenSpan.SetInnerText($"{name} {promotionId}");

                tag.Attributes["href"] = $"{Controllers.Brands}/Details/{brandId}#promotions";
                tag.Attributes["title"] = name;
                tag.SetInnerText(name.First(30));
                tag.InnerHtml += hiddenSpan.ToString();
            }
            var retVal = MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
            return retVal;
        }

        public static IHtmlString DynamicConfirm(string modalTitle, string waitTitle, string formAction, string fontAwsomIcon, string onFormSuccess = "", string @class = "", FontAwesomeSize fontAwesomeSize = FontAwesomeSize.fa2X)
        {
            string size = string.Empty;
            switch (fontAwesomeSize)
            {
                case FontAwesomeSize.fa1x:
                    break;
                case FontAwesomeSize.fa2X:
                    size = "fa-2x";
                    break;
                case FontAwesomeSize.fa3X:
                    size = "fa-3x";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(fontAwesomeSize), fontAwesomeSize, null);
            }

            var tag = new TagBuilder("a");
            tag.AddCssClass($"dynamicConfirm {@class}".TrimEnd());
            tag.Attributes["modal-title"] = modalTitle;
            tag.Attributes["wait-title"] = waitTitle;
            tag.Attributes["form-action"] = formAction;
            if (!string.IsNullOrWhiteSpace(onFormSuccess))
                tag.Attributes["form-success"] = onFormSuccess;

            var i = new TagBuilder("i");
            i.AddCssClass($"fa {fontAwsomIcon} {size}");
            tag.InnerHtml = i.ToString();

            var retVal = MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
            return retVal;
        }

        public static IHtmlString DynamicAction(string modalTitle, string body, string formAction = null, string onFormSuccess = null, string fontAwsomIcon = null, string @class = "", ModalSize modalSize = ModalSize.Default, FontAwesomeSize fontAwesomeSize = FontAwesomeSize.fa2X)
        {

            string size = string.Empty;
            switch (fontAwesomeSize)
            {
                case FontAwesomeSize.fa1x:
                    break;
                case FontAwesomeSize.fa2X:
                    size = "fa-2x";
                    break;
                case FontAwesomeSize.fa3X:
                    size = "fa-3x";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(fontAwesomeSize), fontAwesomeSize, null);
            }

            string modal_Size;
            switch (modalSize)
            {
                case ModalSize.Default:
                    modal_Size = "";
                    break;
                case ModalSize.Small:
                    modal_Size = "modal-sm";
                    break;
                case ModalSize.Large:
                    modal_Size = "modal-lg";
                    break;
                case ModalSize.FullScreen:
                    modal_Size = "modal-full";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(modalSize), modalSize, null);
            }
            var tag = new TagBuilder("a");
            tag.AddCssClass($"dynamicAction {@class}".TrimEnd());
            tag.Attributes["modal-class"] = modal_Size;
            tag.Attributes["modal-title"] = modalTitle;
            tag.Attributes["modal-body"] = body;
            tag.Attributes["form-action"] = formAction;
            tag.Attributes["form-success"] = onFormSuccess;
            tag.Attributes["modal-Name"] = Guid.NewGuid().ToString("N");

            var i = new TagBuilder("i");
            i.AddCssClass($"fa {fontAwsomIcon} {size}");
            tag.InnerHtml = i.ToString();

            var retVal = MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
            return retVal;
        }

        public static IHtmlString ToTextInput(string name, string label = null, string value = "", bool required = false, FormStyle formStyle = FormStyle.Horizontal)
        {
            string html = ToInput(name: name, value: value, type: InputType.text, label: label, required: required, formStyle: formStyle);

            var retVal = MvcHtmlString.Create(html);
            return retVal;
        }

        public static IHtmlString ToPassword(string value, string name, string label = null, bool required = false, FormStyle formStyle = FormStyle.Horizontal)
        {
            string html = ToInput(name: name, value: value, type: InputType.password, label: label, required: required, formStyle: formStyle);
            var retVal = MvcHtmlString.Create(html);
            return retVal;
        }

        public static IHtmlString ToUrlInput(string value, string name, string label = null, bool required = false, FormStyle formStyle = FormStyle.Horizontal)
        {
            string html = ToInput(name: name, value: value, type: InputType.url, label: label, required: required, formStyle: formStyle);
            var retVal = MvcHtmlString.Create(html);
            return retVal;
        }

        public static IHtmlString ToColorPicker(string value, string name, string label = null, bool required = false)
        {
            var html = ToColorPickerInternal(name: name, value: value, label: label, required: required);
            var retVal = MvcHtmlString.Create(html);
            return retVal;
        }

        public static IHtmlString ToDateInput(string name, DateTime? value, string label = null, bool required = false, FormStyle formStyle = FormStyle.Horizontal)
        {
            var date = value.ToInputDate();
            string html = ToInput(name: name, value: date, type: InputType.date, label: label, required: required, formStyle: formStyle);

            var retVal = MvcHtmlString.Create(html);
            return retVal;
        }

        public static IHtmlString ToDateTimeInput(string name, DateTime? value, string label = null, bool required = false, FormStyle formStyle = FormStyle.Horizontal)
        {
            var date = value?.ToString("s") ?? string.Empty;
            string html = ToInput(name: name, value: date, type: InputType.datetime_local, label: label, required: required, formStyle: formStyle);

            var retVal = MvcHtmlString.Create(html);
            return retVal;
        }

        public static IHtmlString ToInput(string name, string value, string label = null,  bool required = false, bool readOnly = false, string onclick = null, FormStyle formStyle = FormStyle.Horizontal)
        {
            string html = ToInput(name: name, value: value, type: InputType.text, label: label,  readOnly: readOnly, onclick: onclick, required: required, formStyle: formStyle);

            var retVal = MvcHtmlString.Create(html);
            return retVal;
        }

        public static IHtmlString ToInput(string name, string label = null, decimal? value = 0, bool required = false, string step = "any", int? min = null, int? max = null, FormStyle formStyle = FormStyle.Horizontal)
        {
            string html = ToInput(name: name, value: value.ToString(), type: InputType.number, label: label, required: required, step: step, min: min, max: max, formStyle: formStyle);

            var retVal = MvcHtmlString.Create(html);
            return retVal;
        }

        public static IHtmlString ToInput(string name, string label = null, int value = 0, bool required = false, string step = "any", int? min = null, int? max = null, FormStyle formStyle = FormStyle.Horizontal)
        {
            string html = ToInput(name: name, value: value.ToString(), type: InputType.number, label: label, required: required, step: step, min: min, max: max, formStyle: formStyle);

            var retVal = MvcHtmlString.Create(html);
            return retVal;
        }

        public static IHtmlString ToInput(string name, string label = null, int? value = null, bool required = false, string step = "any", int? min = null, int? max = null, FormStyle formStyle = FormStyle.Horizontal)
        {
            var sVale = value?.ToString() ?? string.Empty;
            string html = ToInput(name: name, value: sVale, type: InputType.number, label: label, required: required, step: step, min: min, max: max, formStyle: formStyle);

            var retVal = MvcHtmlString.Create(html);
            return retVal;
        }

        public static IHtmlString ToInput(string name, string label = null, double? value = null, bool required = false, string step = "any", double? min = null, double? max = null, FormStyle formStyle = FormStyle.Horizontal)
        {
            string html = ToInput(name: name, value: value.ToString(), type: InputType.number, label: label, required: required, step: step, min: min, max: max, formStyle: formStyle);
            var retVal = MvcHtmlString.Create(html);
            return retVal;
        }

        public static IHtmlString ToFileInput(string name, string label = null, bool required = false, FormStyle formStyle = FormStyle.Horizontal)
        {
            string html = ToInput(name: name, value: null, type: InputType.file, label: label, required: required, formStyle: formStyle);

            var retVal = MvcHtmlString.Create(html);
            return retVal;
        }

        private static string ToColorPickerInternal(string name, string value, string label = null, bool required = false, bool readOnly = false, string onclick = null)
        {
            /*
              <div class="form-group">
                                    <label class="col-md-3 control-label">Cover Background Color</label>
                                    <div class="col-md-7">
                                        <div data-format="alias" class="input-group colorpicker colorpicker-component">
                                            <input type="text" name="BackgroundColor" value="@Model.BackgroundColor" class="form-control" />
                                            <span class="input-group-addon"><i></i></span>
                                        </div>
                                    </div>
                                </div>
             */
            var formGroup = new TagBuilder("div");
            formGroup.AddCssClass("form-group");
            //divWrapper.AddCssClass("form-md-line-input");
            //divWrapper.AddCssClass("form-md-floating-label");

            var labelTag = new TagBuilder("label");
            labelTag.Attributes["for"] = name;
            labelTag.AddCssClass("control-label");
            labelTag.InnerHtml = label;
            labelTag.AddCssClass("col-md-3");
            formGroup.InnerHtml = labelTag.ToString();

            var divInputWrapper = new TagBuilder("div");
            divInputWrapper.AddCssClass("col-md-7");
            var colorpicker = new TagBuilder("div");
            colorpicker.Attributes["data-format"] = "alias";
            colorpicker.AddCssClass("input-group");
            colorpicker.AddCssClass("colorpicker");
            colorpicker.AddCssClass("colorpicker-component");


            var input = new TagBuilder("input");
            input.AddCssClass("form-control");
            input.Attributes["type"] = "text";
            if (value != null)
                input.Attributes["value"] = value;
            input.Attributes["name"] = name;
            input.Attributes["id"] = name.Replace(".", "_");
            if (required)
                input.Attributes["required"] = "required";
            if (readOnly)
                input.Attributes["readonly"] = "true";
            if (!string.IsNullOrWhiteSpace(onclick))
                input.Attributes["onclick"] = onclick;//"onclick=\"this.removeAttribute('readonly');\"";


            colorpicker.InnerHtml = input.ToString();

            var span = new TagBuilder("span");
            span.AddCssClass("input-group-addon");
            var i = new TagBuilder("i");
            span.InnerHtml = i.ToString();
            colorpicker.InnerHtml += span.ToString();

            //colorpicker.InnerHtml = colorpicker.ToString();
            divInputWrapper.InnerHtml = colorpicker.ToString();

            formGroup.InnerHtml += divInputWrapper.ToString();
            string html = formGroup.ToString(TagRenderMode.Normal);

            return html;
        }
        private static string ToInput(string name, string value, InputType type, string label = null,  string cssClass = "", bool required = false, bool readOnly = false, string onclick = null, string step = "any", double? min = null, double? max = null, FormStyle formStyle = FormStyle.Horizontal)
        {
            var divWrapper = new TagBuilder("div");
            divWrapper.AddCssClass("form-group");
            //divWrapper.AddCssClass("form-md-line-input");
            //divWrapper.AddCssClass("form-md-floating-label");

            var input = new TagBuilder("input");
            input.AddCssClass($"form-control {cssClass}".Trim());
            input.Attributes["type"] = type.ToString().Replace("_", "-");
            if (value != null)
                input.Attributes["value"] = value;
            input.Attributes["name"] = name;
            input.Attributes["id"] = name.Replace(".", "_");

            if (!string.IsNullOrEmpty(step))
                input.Attributes["step"] = step.ToString();
            if (required)
                input.Attributes["required"] = "required";
            if (min.HasValue)
                input.Attributes["min"] = min.Value.ToString();
            if (max.HasValue)
                input.Attributes["max"] = max.Value.ToString();
            if (readOnly)
                input.Attributes["readonly"] = "true";
            if (!string.IsNullOrWhiteSpace(onclick))
                input.Attributes["onclick"] = onclick;//"onclick=\"this.removeAttribute('readonly');\"";
            var divInputWrapper = new TagBuilder("div");
            if (!string.IsNullOrWhiteSpace(label))
            {
                var labelTag = new TagBuilder("label");
                labelTag.Attributes["for"] = name;
                labelTag.AddCssClass("control-label");
                labelTag.InnerHtml = label;
                if (formStyle == FormStyle.Horizontal)
                {
                    labelTag.AddCssClass("col-md-3");
                    divInputWrapper.AddCssClass("col-md-7");
                }
                divWrapper.InnerHtml += labelTag.ToString();
            }

            divInputWrapper.InnerHtml = input.ToString();

            divWrapper.InnerHtml += divInputWrapper.ToString();

            string html = divWrapper.ToString(TagRenderMode.Normal);

            return html;
        }

        public static IHtmlString ToTextarea(string name, string label = null, string value = "", bool required = false, int rows = 3, FormStyle formStyle = FormStyle.Horizontal)
        {
            var divWrapper = new TagBuilder("div");
            divWrapper.AddCssClass("form-group");

            var textarea = new TagBuilder("textarea");
            textarea.AddCssClass("form-control");
            textarea.SetInnerText(value);
            textarea.Attributes["name"] = name;
            textarea.Attributes["id"] = name.Replace(".", "_");
            textarea.Attributes["rows"] = rows.ToString();
            if (required)
                textarea.Attributes["required"] = "required";

            var labelTag = new TagBuilder("label");
            labelTag.Attributes["for"] = name;
            labelTag.InnerHtml = label;
            if (formStyle == FormStyle.Horizontal)
            {
                labelTag.AddCssClass("col-md-3");
                labelTag.AddCssClass("control-label");
                var divInputWrapper = new TagBuilder("div");
                divInputWrapper.AddCssClass("col-md-7");
                divInputWrapper.InnerHtml = textarea.ToString();
                divWrapper.InnerHtml += labelTag.ToString();
                divWrapper.InnerHtml += divInputWrapper.ToString();
            }


            return MvcHtmlString.Create(divWrapper.ToString());
        }

        public static IHtmlString ToInput(string name, bool? value, string label = null, FormStyle formStyle = FormStyle.Horizontal)
        {
            //if (!value.HasValue)
            //    value = false;
            return ToInput(name: name, value: value, label: label, formStyle: formStyle);
        }

        public static IHtmlString ToInput(string name, bool value, string label = null, FormStyle formStyle = FormStyle.Horizontal)
        {

            var formGroup = new TagBuilder("div");
            formGroup.AddCssClass("form-group");
            var mdCheckbox = new TagBuilder("div");
            mdCheckbox.AddCssClass("md-checkbox col-md-offset-3");


            var checkboxInput = new TagBuilder("input");
            checkboxInput.Attributes["type"] = "checkbox";
            checkboxInput.Attributes["id"] = name;
            //checkboxInput.Attributes["id"] = name.Replace(".","_");
            checkboxInput.Attributes["name"] = name;
            checkboxInput.Attributes["value"] = value.ToString().ToLower();
            if (value)
                checkboxInput.Attributes["checked"] = "checked";

            //var checkClass = value ? "checked" : "";

            checkboxInput.AddCssClass($"md-check checked");
            mdCheckbox.InnerHtml += checkboxInput.ToString(TagRenderMode.SelfClosing);

            var labelTag = new TagBuilder("label");
            labelTag.Attributes["for"] = name;
            labelTag.AddCssClass("control-label");
            //labelTag.SetInnerText(name);
            labelTag.InnerHtml += "<span></span><span class=\"check\"></span><span class=\"box\"></span> " + label;

            mdCheckbox.InnerHtml += labelTag.ToString(TagRenderMode.Normal);
            formGroup.InnerHtml = mdCheckbox.ToString();
            if (formStyle == FormStyle.Horizontal)
            {
                //var horizontal = new TagBuilder("div");
                //horizontal.AddCssClass("col-md-offset-3");
                //horizontal.InnerHtml = formGroup.ToString();
                //return MvcHtmlString.Create(horizontal.ToString());
            }



            var retVal = MvcHtmlString.Create(formGroup.ToString());
            return retVal;
        }

        public static IHtmlString ToSelect(IEnumerable<SelectOption> items,
            string tagName,
            string displayText = null,
            string cssClass = "form-control select2me",
            bool allowEmpty = false,

            bool required = true,
            bool multiple = false,
            bool closeOnSelect = true,
            bool allowAddNew = false,
            object selected = null,
            bool disabled = false,
            bool enableOnClick = false,
            string target = null,
            string url = null,
            string placeholder = "Select one or more items",
            string onchange = null,
            string onselect = null,
            FormStyle formStyle = FormStyle.Horizontal)
        {
            
            var divWrapper = new TagBuilder("div");
            divWrapper.AddCssClass("form-group");


            var selectTag = new TagBuilder("select");
            selectTag.AddCssClass(cssClass);
            selectTag.Attributes["name"] = tagName;
            selectTag.Attributes["id"] = tagName.Replace(".", "");
            selectTag.Attributes["style"] = "width: 100%";


            if (required)
                selectTag.Attributes["required"] = "required";
            if (disabled)
            {
                selectTag.Attributes["disabled"] = "true";
                if (enableOnClick)
                {
                    selectTag.Attributes["enableOnClick"] = "true";
                }
            }
            if (multiple)
                selectTag.Attributes["multiple"] = "multiple";
            if (!closeOnSelect)
                selectTag.Attributes["closeOnSelect"] = "false";
            if (allowAddNew)
                selectTag.Attributes["addNew"] = "true";
            if (onchange != null)
                selectTag.Attributes["onchange"] = onchange;
            if (!onselect.IsNullOrWhiteSpace())
                selectTag.Attributes["onselect"] = onselect;
            if (!target.IsNullOrWhiteSpace())
            {
                selectTag.Attributes["data-target"] = target.Replace(".", "");
                selectTag.Attributes["data-url"] = url;
            }

            List<TagBuilder> options = new List<TagBuilder>();
            if (items != null)
            {
                var sortedItems = items.OrderBy(i => i.Index).ThenBy(x => x.Text).ToArray();
                for (var i = 0; i < sortedItems.Length; i++)
                {
                    SelectOption item = sortedItems[i];
                    if (item.Hidden)
                        continue;

                    bool? itemSelected = null;
                    if (selected != null)
                    {
                        if (selected is Enum)
                        {
                            var enumType = selected.GetType();
                            if (enumType.GetCustomAttributes<FlagsAttribute>().Any())
                            {
                                int val = int.Parse(item.Value);
                                var ssss = (int)selected & val;
                                if (ssss == val)
                                {
                                    itemSelected = true;

                                }
                                else
                                {

                                }
                            }
                            else
                            {
                                var e = (Enum)selected;
                                var code = e.GetTypeCode();
                                var valueSelected = Convert.ChangeType(selected, code);

                                if (item.Value.Equals(valueSelected.ToString(), StringComparison.CurrentCultureIgnoreCase)
                                    || item.Value.Equals(selected.ToString(), StringComparison.CurrentCultureIgnoreCase))
                                {
                                    itemSelected = true;
                                }
                            }

                        }
                        else if (item.Value.Equals(selected.ToString(), StringComparison.CurrentCultureIgnoreCase))
                        {

                            itemSelected = true;
                        }
                    }
                    //if (isTags)
                    //    itemSelected = true;

                    var optionTag = item.ToTag(itemSelected);
                    optionTag.SetInnerText(item.Text);
                    options.Add(optionTag);
                    //selectTag.InnerHtml += optionTag.ToString(TagRenderMode.Normal);
                }
            }
            if (allowEmpty)
            {
                selectTag.Attributes["allowClear"] = "true";
                selectTag.Attributes["placeholder"] = "";
                var emptyOption = new TagBuilder("option");
                emptyOption.Attributes["value"] = "";
                emptyOption.SetInnerText("");
                if (selected == null)
                    emptyOption.Attributes["selected"] = "selected";
                //if (emptyOptionDisabled)
                //    emptyOption.Attributes["disabled"] = "disabled";
                selectTag.InnerHtml = emptyOption.ToString(TagRenderMode.Normal);
            }
            else
            {
                selectTag.Attributes["placeholder"] = placeholder;
            }

            var sortedOptions = options.OrderBy(x => x.InnerHtml).ToArray();
            foreach (var tagBuilder in sortedOptions)
            {
                selectTag.InnerHtml += tagBuilder.ToString(TagRenderMode.Normal);
            }
            string elements = string.Empty;
            if (displayText != null)
            {
                var label = new TagBuilder("label");
                label.AddCssClass("control-label");
                label.SetInnerText(displayText);
                if (formStyle == FormStyle.Horizontal)
                {
                    label.AddCssClass("col-md-3");
                    label.AddCssClass("control-label");
                }
                elements = label.ToString(TagRenderMode.Normal);

            }
            switch (formStyle)
            {
                case FormStyle.Inline:
                    elements += selectTag.ToString(TagRenderMode.Normal);
                    break;
                case FormStyle.Horizontal:
                    var divInputWrapper = new TagBuilder("div");
                    divInputWrapper.AddCssClass("col-md-7");
                    divInputWrapper.InnerHtml = selectTag.ToString();
                    elements += divInputWrapper.ToString();
                    break;
                default:
                    elements += selectTag.ToString(TagRenderMode.Normal);
                    break;
            }
            divWrapper.InnerHtml = elements;
            var retVal = MvcHtmlString.Create(divWrapper.ToString());
            return retVal;
        }

        public static IHtmlString AddButton(
            string body,
            string formAction,
            string success,
            string title,
            string submitbtnInnerText = null,
            string btnColor = null,
            string btnInnerText = null,
            ModalSize modalSize = ModalSize.Default)
        {

            var aHref = Modal(btnColor: btnColor, title: title, btnSize: "btn-sm", formSuccess: success,
                formAction: formAction, modalBody: body, modalSize: modalSize,
                submitButtonText: "Add");
            aHref.AddCssClass("btn");
            aHref.AddCssClass("btn-sm");
            aHref.AddCssClass("purple");

            var item = new TagBuilder("i");
            item.AddCssClass("fa");
            item.AddCssClass("fa-plus");
            aHref.InnerHtml = "Add " + item;
            var retVal = MvcHtmlString.Create(aHref.ToString());
            return retVal;

        }

        public static TagBuilder Button(
            string modalBody,
            string formAction,
            string success,
            string title,
            string submitbtnInnerText = null,
            string btnColor = null,
            string btnInnerText = null,
            ModalSize modalSize = ModalSize.Default)
        {
            var aHref = Modal(btnColor: btnColor, title: title, btnSize: "btn-sm", formSuccess: success,
                formAction: formAction, modalBody: modalBody, modalSize: modalSize,
                submitButtonText: submitbtnInnerText);
            if (btnInnerText == null)
            {
                var item = new TagBuilder("i");
                item.AddCssClass("fa");
                item.AddCssClass("fa-plus");
                aHref.InnerHtml = "Add " + item;
            }
            else
            {
                aHref.SetInnerText(btnInnerText);
            }
            return aHref;
            //var retVal = MvcHtmlString.Create(aHref.ToString());
            //return retVal;
        }

        public static TagBuilder Modal(string title = null,
            string btnColor = null,
            string btnSize = null,
            string formSuccess = null,
            string formAction = null,
            string modalBody = null,
            ModalSize modalSize = ModalSize.Default,
            string modalId = null,
            string submitButtonText = "Submit")
        {

            var aHref = new TagBuilder("a");
            aHref.AddCssClass("dynamicAction");
            if (btnColor != null)
            {
                aHref.AddCssClass("btn");
                aHref.AddCssClass(btnSize);
                aHref.AddCssClass(btnColor);

            }
            if (modalId == null)
                modalId = Guid.NewGuid().ToString("N");
            string modalS = "";
            switch (modalSize)
            {
                case ModalSize.Default:
                    modalS = "";
                    break;
                case ModalSize.Small:
                    modalS = "modal-sm";
                    break;
                case ModalSize.Large:
                    modalS = "modal-lg";
                    break;
                case ModalSize.FullScreen:
                    modalS = "modal-full";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(modalSize), modalSize, null);
            }
            aHref.Attributes["modal-Name"] = modalId;
            aHref.Attributes["modal-submit"] = submitButtonText;
            aHref.Attributes["modal-class"] = modalS;
            aHref.Attributes["modal-title"] = title;
            aHref.Attributes["modal-body"] = modalBody;
            aHref.Attributes["form-action"] = formAction;
            aHref.Attributes["form-success"] = formSuccess;

            return aHref;
        }

        public static IHtmlString Modal(this WebViewPage webViewPage, string title = null,
            string btnColor = null,
            string btnSize = null,
            string formSuccess = null,
            string formAction = null,
            string modalBody = null,
            ModalSize modalSize = ModalSize.Default,
            string icon = null,
            string innerText = null,
            string modalId = null,
            string submitButtonText = "Submit")
        {

            var modalTag = new TagBuilder("a");
            modalTag.AddCssClass("dynamicAction");
            if (btnColor != null)
            {
                modalTag.AddCssClass("btn");
                modalTag.AddCssClass(btnSize);
                modalTag.AddCssClass(btnColor);

            }
            if (modalId == null)
                modalId = Guid.NewGuid().ToString("N");
            string modalS = "";
            switch (modalSize)
            {
                case ModalSize.Default:
                    modalS = "";
                    break;
                case ModalSize.Small:
                    modalS = "modal-sm";
                    break;
                case ModalSize.Large:
                    modalS = "modal-lg";
                    break;
                case ModalSize.FullScreen:
                    modalS = "modal-full";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(modalSize), modalSize, null);
            }
            modalTag.Attributes["modal-Name"] = modalId;
            modalTag.Attributes["modal-submit"] = submitButtonText;
            modalTag.Attributes["modal-class"] = modalS;
            modalTag.Attributes["modal-title"] = title;
            modalTag.Attributes["modal-body"] = modalBody;
            modalTag.Attributes["form-action"] = formAction;
            modalTag.Attributes["form-success"] = formSuccess;

            var item = new TagBuilder("i");
            item.AddCssClass("fa");

            if (!string.IsNullOrWhiteSpace(icon))
                item.AddCssClass(icon);

            if (!string.IsNullOrWhiteSpace(innerText))
                modalTag.SetInnerText(innerText);

            var retVal = MvcHtmlString.Create(modalTag.ToString());
            return retVal;
        }
    }
}