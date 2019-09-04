using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.ExtentionMethods;
using Entities;
using Entities.Cache;
using Microsoft.CSharp.RuntimeBinder;
using Country = Entities.Backend.Country;
using Currency = Entities.Backend.Currency;
using Language = Entities.Backend.Language;

namespace Web.Extentions
{
    public static class InputExtentions
    {

        public static SelectOption ToSelectOption(this Entities.Cache.Currency currency)
        {
            return new SelectOption($"{currency.Name} - {currency.Id}", currency.Id)
            {
                Index = currency.Priority ?? 100
            };
        }

        public static SelectOption ToSelectOption(this Currency currency)
        {
            return new SelectOption($"{currency.Name} - {currency.Id}", currency.Id)
            {
                Index = currency.Priority ?? 100
            };
        }

        public static SelectOption ToSelectOption(this Entities.Cache.Language language)
        {
            return new SelectOption($"{language.Name} - {language.Id} ({language.NativeName})", language.Id)
            {
                Index = language.Priority ?? 100
            };
        }

        public static SelectOption ToSelectOption(this Language language)
        {
            return new SelectOption($"{language.Name} ({language.NativeName}) - {language.Id}", language.Id)
            {
                Index = language.Priority ?? 100
            };
        }

        public static SelectOption ToSelectOption(this Entities.Cache.Country country)
        {
            return new SelectOption($"{country.Name} - {country.Id}", country.Id)
            {
                Index = country.Priority ?? 100
            };
        }

        public static SelectOption ToSelectOption(this Country country)
        {
            return new SelectOption($"{country.Name} - {country.Id}", country.Id)
            {
                Index = country.Priority ?? 100
            };
        }

        public static List<SelectOption> ToSelectOptions(this IEnumerable<string> enumerable, string selected = null)
        {
            if (enumerable == null)
                return new List<SelectOption>();
            return enumerable.Select(x => new SelectOption(x, x) { Selected = selected != null && x.Equals(selected, StringComparison.CurrentCultureIgnoreCase) }).ToList();
        }

        public static List<SelectOption> ToSelectOptions(this IEnumerable<BaseEntity> enumerable, bool showId = false)
        {
            bool hasPriority = true;
            var options = enumerable.Select(e =>
            {
                var index = 100;
                try
                {
                    if (hasPriority && (!(e.Data.Priority is DBNull) && e.Data.Priority != null))
                    {
                        index = e.Data.Priority;
                    }
                }
                catch (RuntimeBinderException)
                {
                    hasPriority = false;
                }
                string text = showId ? $"{e.Data.Name} - {e.Data.Id}" : $"{e.Data.Name}";
                var option = new SelectOption(text, e.Data.Id.ToString()) { Index = index };
                return option;
            }).ToList();
            return options;
        }

        public static string ToJson(this IEnumerable<SelectOption> items)
        {
            var retVal = items.Select(i => new { i.Value, i.Text });
            return retVal.ToString();
        }

        public static IHtmlString ToSelect(this IEnumerable<SelectOption> items,
            string tagName,
            bool required ,
            string label = null,
            string cssClass = "form-control select2me",
            bool allowEmpty = false,
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
            if (required)
                allowEmpty = false;
            var toSelect = ViewUtils.ToSelect(items: items, tagName: tagName, displayText: label, cssClass: cssClass,
                allowEmpty: allowEmpty, required: required,
                multiple: multiple, closeOnSelect: closeOnSelect, allowAddNew: allowAddNew, selected: selected, disabled: disabled, enableOnClick: enableOnClick,
                target: target, url: url, placeholder: placeholder, onchange: onchange, onselect: onselect, formStyle: formStyle);
            return toSelect;
        }


        public static IHtmlString ToInput(this bool value, string name, string label = null, FormStyle formStyle = FormStyle.Horizontal)
        {
            return ViewUtils.ToInput(name: name, value: value, label: label, formStyle: formStyle);
        }

        public static IHtmlString ToInput(this bool? value, string name, string label = null, FormStyle formStyle = FormStyle.Horizontal)
        {

            if (!value.HasValue)
                value = false;
            return ViewUtils.ToInput(name: name, value: value.Value, label: label, formStyle: formStyle);
        }

        public static IHtmlString ToDateTimeInput(this DateTime? value, string name, string label = null, bool required = false, FormStyle formStyle = FormStyle.Horizontal)
        {
            return ViewUtils.ToDateTimeInput(name: name, value: value, label: label, required: required, formStyle: formStyle);
        }

        public static IHtmlString ToDateTimeInput(this DateTime value, string name, string label = null, bool required = false, FormStyle formStyle = FormStyle.Horizontal)
        {
            return ViewUtils.ToDateTimeInput(name: name, value: value, label: label, required: required, formStyle: formStyle);
        }
        public static IHtmlString ToDateInput(this DateTime value, string name, string label = null, bool required = false, FormStyle formStyle = FormStyle.Horizontal)
        {
            return ViewUtils.ToDateInput(name: name, value: value, label: label, required: required, formStyle: formStyle);
        }

        public static IHtmlString ToDateInput(this DateTime? value, string name, string label = null, bool required = false, FormStyle formStyle = FormStyle.Horizontal)
        {
            return ViewUtils.ToDateInput(name: name, value: value, label: label, required: required, formStyle: formStyle);
        }


        public static IHtmlString ToInput(this byte? value, string name, string label = null, double step = 1, int? min = null, int? max = null, bool required = false, FormStyle formStyle = FormStyle.Horizontal)
        {
            return ViewUtils.ToInput(name: name, value: value, label: label, required: required, formStyle: formStyle);
        }

        public static IHtmlString ToInput(this int? value, string name, string label = null, string step = "any", int? min = null, int? max = null, bool required = false, FormStyle formStyle = FormStyle.Horizontal)
        {
            return ViewUtils.ToInput(name: name, value: value, label: label, step: step, min: min, max: max, required: required, formStyle: formStyle);
        }

        public static IHtmlString ToInput(this int value, string name, string label = null, string step = "any", int? min = null, int? max = null, bool required = false, FormStyle formStyle = FormStyle.Horizontal)
        {
            return ViewUtils.ToInput(name: name, value: value, label: label, step: step, min: min, max: max, required: required, formStyle: formStyle);
        }

        public static IHtmlString ToInput(this decimal? value, string name, string label = null, string step = "any", int? min = null, int? max = null, bool required = false, FormStyle formStyle = FormStyle.Horizontal)
        {
            return ViewUtils.ToInput(name: name, value: value, label: label, min: min, max: max, required: required, formStyle: formStyle);
        }
        public static IHtmlString ToInput(this decimal value, string name, string label = null, string step = "any", int? min = null, int? max = null, bool required = false, FormStyle formStyle = FormStyle.Horizontal)
        {
            return ViewUtils.ToInput(name: name, value: value, label: label, step: step, min: min, max: max, required: required, formStyle: formStyle);
        }

        public static IHtmlString ToInput(this string value, string name, string label = null, bool required = false, bool readOnly = false, string onclick = null, FormStyle formStyle = FormStyle.Horizontal)
        {
            return ViewUtils.ToInput(name: name, value: value, label: label, required: required, readOnly: readOnly, onclick: onclick, formStyle: formStyle);
        }
        //public static IHtmlString ToInput(this string value, string name, string label = null,string[] classes,  bool required = false, FormStyle formStyle = FormStyle.Horizontal)
        //{
        //    return ViewUtils.ToInput(name: name, value: value, label: label, helpText: helpText, required: required, formStyle: formStyle);
        //}

        public static IHtmlString ToTextarea(this string value, string name, string label = null, bool required = false, int rows = 3, FormStyle formStyle = FormStyle.Horizontal)
        {
            return ViewUtils.ToTextarea(name: name, value: value, label: label, required: required, rows: rows, formStyle: formStyle);
        }

        public static IHtmlString ToUrlInput(this string value, string name, string label = null, bool required = false, FormStyle formStyle = FormStyle.Horizontal)
        {
            return ViewUtils.ToUrlInput(name: name, value: value, label: label, required: required, formStyle: formStyle);
        }

        public static IHtmlString ToColorPicker(this string value, string name, string label = null, bool required = false)
        {
            return ViewUtils.ToColorPicker(name: name, value: value, label: label, required: required);
        }

        public static IHtmlString ToInput(this string value, IEnumerable<SelectOption> items,
            string tagName,
            string label = null,
            string cssClass = "form-control select2me",
            bool allowEmpty = false,
            bool required = true,
            bool multiple = false,
            bool closeOnSelect = true,
            bool allowAddNew = false,
            bool disabled = false,
            string target = null,
            string url = null,
            string placeholder = "Select one or more items",
            string onchange = null,
            string onselect = null,
            FormStyle formStyle = FormStyle.Horizontal)
        {
            if (required)
                allowEmpty = false;
            var toSelect = ViewUtils.ToSelect(items: items, tagName: tagName, displayText: label, cssClass: cssClass,
                allowEmpty: allowEmpty, required: required,
                multiple: multiple, closeOnSelect: closeOnSelect, allowAddNew: allowAddNew, selected: value, disabled: disabled,
                target: target, url: url, placeholder: placeholder, onchange: onchange, onselect: onselect, formStyle: formStyle);
            return toSelect;
        }


    }
}
