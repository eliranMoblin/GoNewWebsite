using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Common.ExtentionMethods;
using Entities;

//using Image = Entities.Cache.ImageBase;

namespace Web.Extensions
{

    public static class ViewUtils
    {

        public static IHtmlString DynamicConfirm(string confirmQuestion,
            string waitTitle,
            string formAction,
            string fontAwsomIcon = null,
            string buttonText = null,
            string onFormSuccess = null,
            string refreshAction = null,
            string @class = "",
            FontAwesomeSize fontAwesomeSize = FontAwesomeSize.fa2X)
        {
            if (refreshAction != null)
            {
                refreshAction = refreshAction.Trim('\'', '(', ')');
                refreshAction = $"refreshDiv('{refreshAction}')";
                onFormSuccess = refreshAction;
            }
            var size = string.Empty;
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
                    throw new ArgumentOutOfRangeException(nameof(fontAwesomeSize), actualValue: fontAwesomeSize,
                        message: null);
            }

            var tag = new TagBuilder("a");
            tag.AddCssClass($"dynamicConfirm {@class}".TrimEnd());
            tag.Attributes["modal-title"] = confirmQuestion;
            tag.Attributes["wait-title"] = waitTitle;
            tag.Attributes["form-action"] = formAction;
            if (!string.IsNullOrWhiteSpace(value: onFormSuccess))
                tag.Attributes["form-success"] = onFormSuccess;
            if (string.IsNullOrWhiteSpace(buttonText))
            {
                var i = new TagBuilder("i");
                i.AddCssClass($"fa {fontAwsomIcon} {size}");
                tag.InnerHtml = i.ToString();
            }
            else
            {
                tag.SetInnerText(buttonText);
            }

            var retVal = MvcHtmlString.Create(tag.ToString(renderMode: TagRenderMode.Normal));
            return retVal;
        }

        public static IHtmlString DynamicAction(string modalTitle,
            string body,
            string formAction = null,
            string onFormSuccess = null,
            string refreshAction = null,
            string fontAwsomIcon = null,
            string @class = "",
            ModalSize modalSize = ModalSize.Default,
            FontAwesomeSize fontAwesomeSize = FontAwesomeSize.fa2X)
        {
            if (refreshAction != null)
            {
                refreshAction = refreshAction.Trim('\'', '(', ')');
                refreshAction = $"refreshDiv('{refreshAction}')";
                onFormSuccess = refreshAction;
            }
            var size = string.Empty;
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
                    throw new ArgumentOutOfRangeException(nameof(fontAwesomeSize), actualValue: fontAwesomeSize,
                        message: null);
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
                    throw new ArgumentOutOfRangeException(nameof(modalSize), actualValue: modalSize, message: null);
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

            var retVal = MvcHtmlString.Create(tag.ToString(renderMode: TagRenderMode.Normal));
            return retVal;
        }

        //public static IHtmlString ToTextInput(string name,
        //    string label = null,
        //    string value = "",
        //    string cssClass = "",
        //    bool required = false,
        //    FormStyle formStyle = FormStyle.Horizontal)
        //{
        //    var html = ToInput(name: name, value: value, cssClass: cssClass, type: InputType.text, label: label, required: required,
        //        formStyle: formStyle);

        //    var retVal = MvcHtmlString.Create(value: html);
        //    return retVal;
        //}

        public static IHtmlString ToPassword(string value,
            string name,
            string label = null,
            bool required = false,
            FormStyle formStyle = FormStyle.Horizontal)
        {
            var html = ToInput(name: name, value: value, type: InputType.password, label: label, required: required,
                formStyle: formStyle);
            var retVal = MvcHtmlString.Create(value: html);
            return retVal;
        }

        public static IHtmlString ToUrlInput(string value,
            string name,
            string label = null,
            bool required = false,
            FormStyle formStyle = FormStyle.Horizontal)
        {
            var html = ToInput(name: name, value: value, type: InputType.url, label: label, required: required,
                formStyle: formStyle);
            var retVal = MvcHtmlString.Create(value: html);
            return retVal;
        }

        public static IHtmlString ToColorPicker(string value, string name, string label = null, bool required = false)
        {
            var html = ToColorPickerInternal(name: name, value: value, label: label, required: required);
            var retVal = MvcHtmlString.Create(value: html);
            return retVal;
        }

        public static IHtmlString ToDateInput(string name,
            DateTime? value,
            string label = null,
            bool required = false,
            DateInputFormat inputFormat = DateInputFormat.Date,
            FormStyle formStyle = FormStyle.Horizontal)
        {
            var date = value.ToInputDate();
            InputType inputType = InputType.date;
            if (inputFormat == DateInputFormat.Month)
            {
                inputType = InputType.month;
                date = value.Value.ToString("yyyy-MM");
            }
            var html = ToInput(name: name, value: date, type: inputType, label: label, required: required,
                formStyle: formStyle);

            var retVal = MvcHtmlString.Create(value: html);
            return retVal;
        }

        public static IHtmlString ToDateTimeInput(string name,
            DateTime? value,
            string label = null,
            bool required = false,
            FormStyle formStyle = FormStyle.Horizontal)
        {
            var date = value?.ToString("s") ?? string.Empty;
            var html = ToInput(name: name, value: date, type: InputType.datetime_local, label: label,
                required: required, formStyle: formStyle);

            var retVal = MvcHtmlString.Create(value: html);
            return retVal;
        }

        public static IHtmlString ToInput(string name,
            string value,
            string label = null,
            string cssClass = "",
            bool required = false,
            bool readOnly = false,
            bool enableOnClick = false,
            string onclick = null,
            FormStyle formStyle = FormStyle.Horizontal)
        {
            var html = ToInput(name: name, value: value, type: InputType.text, label: label, readOnly: readOnly,enableOnClick: enableOnClick, cssClass: cssClass,
                onclick: onclick, required: required, formStyle: formStyle);

            var retVal = MvcHtmlString.Create(value: html);
            return retVal;
        }

        public static IHtmlString ToInput(string name,
            string label = null,
            decimal? value = 0,
            bool required = false,
            string step = "any",
            int? min = null,
            int? max = null,
            FormStyle formStyle = FormStyle.Horizontal)
        {
            var html = ToInput(name: name, value: value.ToString(), type: InputType.number, label: label,
                required: required, step: step, min: min, max: max, formStyle: formStyle);

            var retVal = MvcHtmlString.Create(value: html);
            return retVal;
        }

        public static IHtmlString ToInput(string name,
            string label = null,
            int value = 0,
            bool required = false,
            string step = "any",
            int? min = null,
            int? max = null,
            FormStyle formStyle = FormStyle.Horizontal)
        {
            var html = ToInput(name: name, value: value.ToString(), type: InputType.number, label: label,
                required: required, step: step, min: min, max: max, formStyle: formStyle);

            var retVal = MvcHtmlString.Create(value: html);
            return retVal;
        }

        public static IHtmlString ToInput(string name,
            string label = null,
            int? value = null,
            bool required = false,
            string step = "any",
            int? min = null,
            int? max = null,
            FormStyle formStyle = FormStyle.Horizontal)
        {
            var sVale = value?.ToString() ?? string.Empty;
            var html = ToInput(name: name, value: sVale, type: InputType.number, label: label, required: required,
                step: step, min: min, max: max, formStyle: formStyle);

            var retVal = MvcHtmlString.Create(value: html);
            return retVal;
        }

        public static IHtmlString ToInput(string name,
            string label = null,
            double? value = null,
            bool required = false,
            string step = "any",
            double? min = null,
            double? max = null,
            FormStyle formStyle = FormStyle.Horizontal)
        {
            var html = ToInput(name: name, value: value.ToString(), type: InputType.number, label: label,
                required: required, step: step, min: min, max: max, formStyle: formStyle);
            var retVal = MvcHtmlString.Create(value: html);
            return retVal;
        }

        public static IHtmlString ToFileInput(string name,
            string label = null,
            bool required = false,
            FormStyle formStyle = FormStyle.Horizontal)
        {
            var html = ToInput(name: name, value: null, type: InputType.file, label: label, required: required,
                formStyle: formStyle);

            var retVal = MvcHtmlString.Create(value: html);
            return retVal;
        }

        private static string ToColorPickerInternal(string name,
            string value,
            string label = null,
            bool required = false,
            bool readOnly = false,
            string onclick = null)
        {
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
            if (!string.IsNullOrWhiteSpace(value: onclick))
                input.Attributes["onclick"] = onclick; //"onclick=\"this.removeAttribute('readonly');\"";

            colorpicker.InnerHtml = input.ToString();

            var span = new TagBuilder("span");
            span.AddCssClass("input-group-addon");
            var i = new TagBuilder("i");
            span.InnerHtml = i.ToString();
            colorpicker.InnerHtml += span.ToString();

            //colorpicker.InnerHtml = colorpicker.ToString();
            divInputWrapper.InnerHtml = colorpicker.ToString();

            formGroup.InnerHtml += divInputWrapper.ToString();
            var html = formGroup.ToString(renderMode: TagRenderMode.Normal);

            return html;
        }

        private static string ToInput(string name,
            string value,
            InputType type,
            string label = null,
            string cssClass = "",
            bool required = false,
            bool readOnly = false,
            bool enableOnClick = false,
            string onclick = null,
            string step = "",
            double? min = null,
            double? max = null,
            FormStyle formStyle = FormStyle.Horizontal)
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

            if (!string.IsNullOrEmpty(value: step))
                input.Attributes["step"] = step;
            if (required)
                input.Attributes["required"] = "required";
            if (min.HasValue)
                input.Attributes["min"] = min.Value.ToString();
            if (max.HasValue)
                input.Attributes["max"] = max.Value.ToString();
            if (readOnly)
            {
                input.Attributes["readonly"] = "true";
                if (enableOnClick)
                {
                    input.AddCssClass("enableOnClick");
                }
            }
            if (!string.IsNullOrWhiteSpace(value: onclick))
                input.Attributes["onclick"] = onclick; //"onclick=\"this.removeAttribute('readonly');\"";
            var divInputWrapper = new TagBuilder("div");
            if (!string.IsNullOrWhiteSpace(value: label))
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

            var html = divWrapper.ToString(renderMode: TagRenderMode.Normal);

            return html;
        }

        public static IHtmlString ToTextarea(string name,
            string label = null,
            string value = "",
            bool required = false,
            int rows = 3,
            FormStyle formStyle = FormStyle.Horizontal)
        {
            var divWrapper = new TagBuilder("div");
            divWrapper.AddCssClass("form-group");

            var textarea = new TagBuilder("textarea");
            textarea.AddCssClass("form-control");
            textarea.SetInnerText(innerText: value);
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
            else
            {
                divWrapper.InnerHtml = textarea.ToString();
            }

            return MvcHtmlString.Create(divWrapper.ToString());
        }

        public static IHtmlString ToInput(string name,
            bool? value,
            string label = null,
            FormStyle formStyle = FormStyle.Horizontal)
        {
            //if (!value.HasValue)
            //    value = false;
            return ToInput(name: name, value: value, label: label, formStyle: formStyle);
        }

        public static IHtmlString ToRadioButton(string[] names, string tagName)
        {
            var formGroup = new TagBuilder("div");
            formGroup.AddCssClass("form-group");
            var divOffset = new TagBuilder("div");
            divOffset.AddCssClass("col-lg-offset-3");
            foreach (var name in names)
            {
                var input = new TagBuilder("input");
                input.Attributes["type"] = "radio";
                input.Attributes["id"] = $"{name.ToLower()}Choice";
                input.Attributes["value"] = name;
                input.Attributes["name"] = tagName;

                var label = new TagBuilder("label");
                label.Attributes["for"] = $"{name.ToLower()}Choice";
                label.SetInnerText(name);

                divOffset.InnerHtml += input.ToString();
                divOffset.InnerHtml += label.ToString();
            }
            formGroup.InnerHtml += divOffset.ToString();
            var retVal = MvcHtmlString.Create(formGroup.ToString());
            return retVal;
        }

        public static IHtmlString ToInput(string name,
            bool value,
            string label = null,
            FormStyle formStyle = FormStyle.Horizontal)
        {
            var formGroup = new TagBuilder("div");
            formGroup.AddCssClass("form-group");
            var mdCheckbox = new TagBuilder("div");
            mdCheckbox.AddCssClass("md-checkbox");

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
            mdCheckbox.InnerHtml += checkboxInput.ToString(renderMode: TagRenderMode.SelfClosing);

            var labelTag = new TagBuilder("label");
            labelTag.Attributes["for"] = name;
            labelTag.AddCssClass("control-label");
            //labelTag.SetInnerText(name);
            labelTag.InnerHtml += "<span></span><span class=\"check\"></span><span class=\"box\"></span> " + label;

            mdCheckbox.InnerHtml += labelTag.ToString(renderMode: TagRenderMode.Normal);
            if (formStyle == FormStyle.Horizontal)
            {
                mdCheckbox.AddCssClass("col-md-offset-3");
            }
            formGroup.InnerHtml = mdCheckbox.ToString();

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
                    selectTag.AddCssClass("enableOnClick");
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
                options.Add(emptyOption);
            }
            else
            {
                selectTag.Attributes["placeholder"] = placeholder;
            }
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
                        if (!(selected is string) && selected is IEnumerable enumerable)
                        {
                            foreach (var selectedItem in enumerable)
                            {
                                if (item.Value.Equals(selectedItem.ToString(), StringComparison.CurrentCultureIgnoreCase))
                                {
                                    itemSelected = true;
                                    break;
                                }
                            }
                        }
                        else if (selected is Enum)
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



        public static IHtmlString Button(string modalBody,
            string title,
            string btnText,
            string formAction = null,
            string refreshAction = null,
            string btnColor = null,
            string btnFont = null,
            ModalSize modalSize = ModalSize.Default)
        {
            string success = null;
            if (refreshAction != null)
            {
                refreshAction = refreshAction.Trim('\'', '(', ')');
                refreshAction = $"refreshDiv('{refreshAction}')";
                success = refreshAction;
            }

            string submitbtnInnerText = "Submit";

            var addButton = ModalButton(modalBody: modalBody, formAction: formAction, success: success, title: title,
                submitbtnInnerText: submitbtnInnerText, btnColor: btnColor, btnInnerText: btnText,
                modalSize: modalSize);
            addButton.AddCssClass("btn");
            addButton.AddCssClass(btnColor);
            addButton.InnerHtml = $"{btnText}";
            if (!string.IsNullOrWhiteSpace(btnFont))
            {
                var item = new TagBuilder("i");
                item.AddCssClass("fa");
                item.AddCssClass(btnFont);
                addButton.InnerHtml = addButton.InnerHtml + " " + item;

            }
            var retVal = MvcHtmlString.Create(addButton.ToString());
            return retVal;
        }
        public static IHtmlString AddButton(string href)
        {
            string btnColor = "purple";
            string btnSize = "btn-sm";
            var aHref = new TagBuilder("a");
            aHref.Attributes["href"] = href;
            aHref.AddCssClass("btn");
            aHref.AddCssClass(btnSize);
            aHref.AddCssClass(btnColor);


            var item = new TagBuilder("i");
            item.AddCssClass("fa");
            item.AddCssClass("fa-plus");

            aHref.InnerHtml = "Add " + item;
            var retVal = MvcHtmlString.Create(aHref.ToString());
            return retVal;
        }

        public static IHtmlString AddButton(string modalBody,
            string title,
            string formAction = null,
            string refreshAction = null,
            string btnText = null,
            ModalSize modalSize = ModalSize.Default)
        {
            string success = null;
            if (refreshAction != null)
            {
                refreshAction = refreshAction.Trim('\'', '/', '(', ')');
                refreshAction = $"refreshDiv('/{refreshAction}')";
                success = refreshAction;
            }

            string submitbtnInnerText = "Add";
            string btnColor = null;

            var addButton = ModalButton(modalBody: modalBody, formAction: formAction, success: success, title: title,
                submitbtnInnerText: submitbtnInnerText, btnColor: btnColor, btnInnerText: btnText,
                modalSize: modalSize);
            addButton.AddCssClass("btn");
            addButton.AddCssClass("purple");
            var item = new TagBuilder("i");
            item.AddCssClass("fa");
            item.AddCssClass("fa-plus");
            if (btnText.IsNullOrWhiteSpace())
                btnText = "Add";
            addButton.InnerHtml = $"{btnText} " + item;
            var retVal = MvcHtmlString.Create(addButton.ToString());
            return retVal;
        }

        public static IHtmlString EditButton(string modalBody,
            string formAction = null,
        string tableId = null,
        string success = null,
        string title = null,
            string submitbtnInnerText = "Save",
        FontAwesomeSize? fontSize = null,
        ModalSize modalSize = ModalSize.Default)
        {

            if (success == null && tableId != null)
            {
                tableId = tableId.Trim('#');
                success = $"refreshParent('#{tableId}')";
            }

            if (string.IsNullOrWhiteSpace(formAction))
                formAction = modalBody;

            var aHref = ModalButton(modalBody: modalBody, formAction: formAction, success: success, title: title,
                modalSize: modalSize, submitbtnInnerText: submitbtnInnerText, btnColor: null);
            //var aHref = ViewUtils.Modal(title: title, formSuccess: success, formAction: $"/{controller}/{action}/{id}",
            //    modalBody: $"/{controller}/{body}/{id}", modalSize: modalSize, submitButtonText: "Save");

            var item = new TagBuilder("i");
            item.AddCssClass("fa");
            string faSize;
            switch (fontSize)
            {
                case FontAwesomeSize.fa2X:
                    faSize = "fa-2x";
                    break;
                case FontAwesomeSize.fa1x:
                    faSize = "";
                    break;
                case FontAwesomeSize.fa3X:
                    faSize = "fa-3x";
                    break;
                default:
                    faSize = "fa-2x";
                    break;
            }

            item.AddCssClass(value: faSize);
            item.AddCssClass("fa-edit");
            item.Attributes["style"] = "color:#a48334;";
            aHref.InnerHtml = item.ToString();

            var retVal = MvcHtmlString.Create(aHref.ToString());
            return retVal;
        }

        public static IHtmlString EditLink(string modalBody,
            string linkText,
            string formAction = null,
            string tableId = null,
            string success = null,
            string title = null,
            string submitbtnInnerText = "Save",
            ModalSize modalSize = ModalSize.Default)
        {
            if (success == null && tableId != null)
            {
                tableId = tableId.Trim('#');
                success = $"refreshParent('#{tableId}')";
            }

            if (string.IsNullOrWhiteSpace(formAction))
                formAction = modalBody;

            var aHref = ModalButton(modalBody: modalBody, formAction: formAction, success: success, title: title,
                modalSize: modalSize, submitbtnInnerText: submitbtnInnerText, btnColor: null);

            aHref.SetInnerText(linkText);

            var retVal = MvcHtmlString.Create(aHref.ToString());
            return retVal;
        }
        public static TagBuilder ModalButton(string modalBody,
            string formAction,
            string success,
            string title,
            string submitbtnInnerText = null,
            string btnColor = null,
            string btnInnerText = null,
            ModalSize modalSize = ModalSize.Default)
        {
            if (formAction == null)
                formAction = modalBody;
            var aHref = Modal(btnColor: btnColor, title: title, btnSize: "btn-sm", formSuccess: success,
                formAction: formAction, modalBody: modalBody, modalSize: modalSize,
                submitButtonText: submitbtnInnerText);

            if (btnInnerText != null)
                aHref.SetInnerText(innerText: btnInnerText);

            return aHref;
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
                aHref.AddCssClass(value: btnSize);
                aHref.AddCssClass(value: btnColor);
            }

            if (modalId == null)
                modalId = Guid.NewGuid().ToString("N");
            var modalS = "";
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
                    throw new ArgumentOutOfRangeException(nameof(modalSize), actualValue: modalSize, message: null);
            }

            aHref.Attributes["modal-Name"] = modalId;
            if (!string.IsNullOrWhiteSpace(value: submitButtonText))
                aHref.Attributes["modal-submit"] = submitButtonText;
            if (!string.IsNullOrWhiteSpace(value: modalS))
                aHref.Attributes["modal-class"] = modalS;
            if (!string.IsNullOrWhiteSpace(value: title))
                aHref.Attributes["modal-title"] = title;
            if (!string.IsNullOrWhiteSpace(value: modalBody))
                aHref.Attributes["modal-body"] = modalBody;
            if (!string.IsNullOrWhiteSpace(value: formAction))
                aHref.Attributes["form-action"] = formAction;
            if (!string.IsNullOrWhiteSpace(value: formSuccess))
                aHref.Attributes["form-success"] = formSuccess;

            return aHref;
        }

        public static IHtmlString Modal(this WebViewPage webViewPage,
            string title = null,
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
                modalTag.AddCssClass(value: btnSize);
                modalTag.AddCssClass(value: btnColor);
            }

            if (modalId == null)
                modalId = Guid.NewGuid().ToString("N");
            var modalS = "";
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
                    throw new ArgumentOutOfRangeException(nameof(modalSize), actualValue: modalSize, message: null);
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

            if (!string.IsNullOrWhiteSpace(value: icon))
                item.AddCssClass(value: icon);

            if (!string.IsNullOrWhiteSpace(value: innerText))
                modalTag.SetInnerText(innerText: innerText);

            var retVal = MvcHtmlString.Create(modalTag.ToString());
            return retVal;
        }

        public static IHtmlString DetailsButton(string action)
        {
            action = action.Trim('/');
            var aHref = new TagBuilder("a");
            aHref.Attributes["href"] = $"/{action}";

            var item = new TagBuilder("i");
            item.AddCssClass("fa");
            item.AddCssClass("fa-2x");
            item.AddCssClass("fa-wrench");
            item.Attributes["style"] = "color:#3598dc;";
            aHref.InnerHtml = item.ToString();

            var retVal = MvcHtmlString.Create(aHref.ToString());
            return retVal;
        }


        public static IHtmlString DeleteButton(string action, string title, bool deleted)
        {
            var deleteLink = new TagBuilder("a");
            deleteLink.Attributes["modal-title"] = title;
            deleteLink.Attributes["form-action"] = action;
            deleteLink.Attributes["title"] = "Delete";
            deleteLink.AddCssClass("dynamicDelete");

            var deletei = new TagBuilder("i");
            deletei.AddCssClass("fa");
            deletei.AddCssClass("fa-2x");
            deletei.AddCssClass("fa-trash");
            deletei.Attributes["style"] = "color:#e12330;";
            if (deleted)
                deleteLink.Attributes["style"] = "display:none";

            deleteLink.InnerHtml = deletei.ToString();

            if (title == null)
                deleteLink.Attributes["modal-title"] = $"Are you sure want to delete it?";

            deleteLink.Attributes["modal-title"] = title;

            var restoreLink = new TagBuilder("a");
            restoreLink.Attributes["title"] = "Restore";
            restoreLink.Attributes["modal-title"] = title;
            restoreLink.AddCssClass("dynamicRestore");

            var restorei = new TagBuilder("i");
            restorei.AddCssClass("fa");
            restorei.AddCssClass("fa-2x");
            restorei.AddCssClass("fa-undo");
            restorei.Attributes["style"] = "color:#2B3643;";
            if (!deleted)
                restoreLink.Attributes["style"] = "display:none";
            restoreLink.Attributes["modal-title"] = $"Are you sure want to restore ?";
            restoreLink.Attributes["form-action"] = action;

            restoreLink.InnerHtml = restorei.ToString();

            var retVal = MvcHtmlString.Create(deleteLink + restoreLink.ToString());
            return retVal;
        }


    }
}