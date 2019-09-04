//using System;
//using System.Web;
//using System.Web.Mvc;
//using Entities;
//using Entities.System;

//namespace Web.Extensions
//{
//    public static class ModelExtensions
//    {
//        public static IHtmlString GetImageTag(this Image image,
//            bool lazyLoad,
//            bool showDebug,
//            string title = null,
//            string cssClass = null,
//            bool marginTop = false)
//        {
//            //@media(min-width:320px)  { /* smartphones, iPhone, portrait 480x320 phones */ }
//            //@media(min-width:481px)  { /* portrait e-readers (Nook/Kindle), smaller tablets @ 600 or @ 640 wide. */ }
//            //@media(min-width:641px)  { /* portrait tablets, portrait iPad, landscape e-readers, landscape 800x480 or 854x480 phones */ }
//            //@media(min-width:961px)  { /* tablet, landscape iPad, lo-res laptops ands desktops */ }
//            //@media(min-width:1025px) { /* big landscape tablets, laptops, and desktops */ }
//            //@media(min-width:1281px) { /* hi-res laptops and desktops */ }
//            var pictureTag = new TagBuilder("picture");
//            Image fallbackImage = null;
//            Image mobileImage = null;
//            Image tabletImage = null;
//            if (image != null)
//            {
//                foreach (var childImage in image.Images)
//                    if (childImage.ImageOptions.HasFlag(flag: ImageOptions.Mobile))
//                        mobileImage = childImage;
//                    else if (childImage.ImageOptions.HasFlag(flag: ImageOptions.Tablet))
//                        tabletImage = childImage;
//                    else if (childImage.ImageOptions.HasFlag(flag: ImageOptions.GoogleCompression) &&
//                             fallbackImage == null)
//                        fallbackImage = childImage;
//                if (fallbackImage == null)
//                    fallbackImage = image;
//            }

//            var img = new TagBuilder("img");
//            if (fallbackImage != null)
//            {
//                var desktopSource = new TagBuilder("source");
//                desktopSource.Attributes["media"] = "(min-width: 961px)";
//                if (showDebug)
//                {
//                    desktopSource.Attributes["debug-id"] = fallbackImage.Id.ToString();
//                    desktopSource.Attributes["debug-parentid"] = fallbackImage.ParentImageId.ToString();
//                }

//                desktopSource.Attributes["srcset"] = Uri.EscapeUriString(stringToEscape: fallbackImage.Url);
//                pictureTag.InnerHtml += desktopSource.ToString(renderMode: TagRenderMode.SelfClosing);

//                if (tabletImage != null)
//                {
//                    var tabletSource = new TagBuilder("source");
//                    tabletSource.Attributes["media"] = "(min-width: 641px)";
//                    tabletSource.Attributes["srcset"] = Uri.EscapeUriString(stringToEscape: tabletImage.Url);
//                    if (marginTop)
//                        tabletSource.Attributes["style"] = $"margin-top: -{tabletImage.Height / 2}px;";
//                    if (showDebug)
//                    {
//                        tabletSource.Attributes["debug-id"] = tabletImage.Id.ToString();
//                        tabletSource.Attributes["debug-parentid"] = tabletImage.ParentImageId.ToString();
//                    }

//                    pictureTag.InnerHtml += tabletSource.ToString(renderMode: TagRenderMode.SelfClosing);
//                }

//                if (mobileImage != null)
//                {
//                    var mobileSource = new TagBuilder("source");
//                    mobileSource.Attributes["media"] = "(min-width: 320px)";
//                    mobileSource.Attributes["srcset"] = Uri.EscapeUriString(stringToEscape: mobileImage.Url);
//                    if (marginTop)
//                        mobileSource.Attributes["style"] = $"margin-top: -{mobileImage.Height / 2}px;";
//                    if (showDebug)
//                    {
//                        mobileSource.Attributes["debug-id"] = mobileImage.Id.ToString();
//                        mobileSource.Attributes["debug-parentid"] = mobileImage.ParentImageId.ToString();
//                    }

//                    pictureTag.InnerHtml += mobileSource.ToString(renderMode: TagRenderMode.SelfClosing);
//                }

//                if (!string.IsNullOrWhiteSpace(value: title))
//                    img.Attributes["title"] = title;
//                else
//                    img.Attributes["title"] = image.AlternateText;
//                if (!string.IsNullOrWhiteSpace(value: cssClass))
//                    img.AddCssClass(value: cssClass);
//                ;
//                if (marginTop)
//                    img.Attributes["style"] = $"margin-top: -{fallbackImage.Height / 2}px;";
//                //img.Attributes["height"] = $"{fallbackImage.Height}px";
//                //img.Attributes["width"] = $"{fallbackImage.Width}px";

//                if (!string.IsNullOrWhiteSpace(value: image.AlternateText))
//                    img.Attributes["alt"] = image.AlternateText;
//                if (string.IsNullOrWhiteSpace(value: image.AlternateText) && string.IsNullOrWhiteSpace(value: title))
//                    img.Attributes["alt"] = image.OriginalFileName;
//                img.Attributes[lazyLoad ? "data-src" : "SRC"] = Uri.EscapeUriString(stringToEscape: fallbackImage.Url);

//                if (!string.IsNullOrWhiteSpace(value: image.BackgroundColor))
//                    img.Attributes["style"] = $"background-color: {image.BackgroundColor}";
//                if (lazyLoad)
//                    img.AddCssClass("js-lazy-image");
//            }

//            pictureTag.InnerHtml += img.ToString(renderMode: TagRenderMode.SelfClosing);
//            var picTag = pictureTag.ToString(renderMode: TagRenderMode.Normal);
//            return MvcHtmlString.Create(value: picTag);
//        }

//        public static IHtmlString DetailsButton(this object model,
//       string controller,
//       string action = "Details",
//       object id = null)
//        {
//            dynamic modelD = model;
//            if (id == null)
//                id = modelD.Id;

//            controller = controller.Trim('/');
//            var aHref = new TagBuilder("a");
//            aHref.Attributes["href"] = $"/{controller}/{action}/{id}";

//            var item = new TagBuilder("i");
//            item.AddCssClass("fa");
//            item.AddCssClass("fa-2x");
//            item.AddCssClass("fa-wrench");
//            item.Attributes["style"] = "color:#3598dc;";
//            aHref.InnerHtml = item.ToString();

//            var retVal = MvcHtmlString.Create(aHref.ToString());
//            return retVal;
//        }



//        //public static IHtmlString EditButton(string controller,
//        //    object id,
//        //    string tableId = null,
//        //    string success = null,
//        //    string action = null,
//        //    string body = null,
//        //    string title = null,
//        //    FontAwesomeSize? fontSize = null,
//        //    ModalSize modalSize = ModalSize.Default)
//        //{
//        //    controller = controller.Trim('/');

//        //    if (success == null && tableId != null)
//        //    {
//        //        tableId = tableId.Trim('#');
//        //        success = $"refreshParent('#{tableId}')";
//        //    }

//        //    if (action == null)
//        //        action = "edit";
//        //    if (body == null)
//        //        body = action;
//        //    var modalBody = $"/{controller}/{body}/{id}";
//        //    var formAction = $"/{controller}/{action}/{id}";
//        //    var submitbtnInnerText = "Save";
//        //    var aHref = ViewUtils.ModalButton(modalBody: modalBody, formAction: formAction, success: success, title: title,
//        //        modalSize: modalSize, submitbtnInnerText: submitbtnInnerText, btnColor: null);
//        //    //var aHref = ViewUtils.Modal(title: title, formSuccess: success, formAction: $"/{controller}/{action}/{id}",
//        //    //    modalBody: $"/{controller}/{body}/{id}", modalSize: modalSize, submitButtonText: "Save");

//        //    var item = new TagBuilder("i");
//        //    item.AddCssClass("fa");
//        //    string faSize;
//        //    switch (fontSize)
//        //    {
//        //        case FontAwesomeSize.fa2X:
//        //            faSize = "fa-2x";
//        //            break;
//        //        case FontAwesomeSize.fa1x:
//        //            faSize = "";
//        //            break;
//        //        case FontAwesomeSize.fa3X:
//        //            faSize = "fa-3x";
//        //            break;
//        //        default:
//        //            faSize = "fa-2x";
//        //            break;
//        //    }

//        //    item.AddCssClass(value: faSize);
//        //    item.AddCssClass("fa-edit");
//        //    item.Attributes["style"] = "color:#a48334;";
//        //    aHref.InnerHtml = item.ToString();

//        //    var retVal = MvcHtmlString.Create(aHref.ToString());
//        //    return retVal;
//        //}


//        public static IHtmlString EditButton(this object model,
//            string controller,
//            string tableId = null,
//            string success = null,
//            string action = null,
//            string body = null,
//            string title = null,
//            object id = null,
//            FontAwesomeSize? fontSize = null,
//            ModalSize modalSize = ModalSize.Default)
//        {
//            if (id == null)
//            {
//                dynamic modelD = model;
//                id = modelD.Id;
//            }

//            controller = controller.Trim('/');

//            if (success == null && tableId != null)
//            {
//                tableId = tableId.Trim('#');
//                success = $"refreshParent('#{tableId}')";
//            }

//            if (title == null)
//                title = "Edit";
//            if (action == null)
//                action = "edit";
//            if (body == null)
//                body = action;
//            var modalBody = $"/{controller}/{body}/{id}";
//            var formAction = $"/{controller}/{action}/{id}";
//            var aHref = ViewUtils.EditButton(modalBody: modalBody, formAction: formAction, success: success, title: title,
//                modalSize: modalSize);
         
//            var retVal = MvcHtmlString.Create(aHref.ToString());
//            return retVal;
//        }

//        public static IHtmlString DeleteButton(this object model,
//            string controller,
//            string action = null,
//            object id = null,
//            string name = null,
//            string title = null)
//        {
//            dynamic modelD = model;
//            if (id == null)
//                id = modelD.Id;
//            if (name == null && title == null)
//                name = modelD.Name;
//            var deleted = false;
//            if (modelD.IsDeleted != null)
//                deleted = modelD.IsDeleted;

//            controller = controller.Trim('/');
            
//            var retVal = ViewUtils.DeleteButton(action: $"/{controller}/{action ?? "Delete"}/{id}",
//                title: $"Are you sure want to delete {name}?", deleted: deleted);
         
//            return retVal;
//        }


//    }
//}