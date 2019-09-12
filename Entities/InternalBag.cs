//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Entities
//{
//    public class InternalBag
//    {
//        public List<SelectOption> ColsData { get; set; }

//        public List<SelectOption> Sections { get; set; }

//        public List<SelectOption> Appliaction { get; set; }

//        public List<SelectOption> lkCultures { get; set; }

//        public List<SelectOption> lkResources { get; set; }
//        private static readonly List<string> _dateFormat = new List<string>
//        {
//            "dd-MM-yyyy",
//            "dd-MMM-yyyy",
//            "d/M/yyyy",
//            "d/MM/yyyy",
//            "dd/MM/yyyy",
//            "M/d/yyyy",
//            "M/dd/yyyy",
//            "yyyy-MM-dd",
//            "yyyy-MM"
//        };


//        public InternalBag()
//        {
//        }

//        public void AddDateFormat(string format)
//        {
//            if (string.IsNullOrWhiteSpace(format))
//                return;

//            if (!_dateFormat.Contains(format))
//                _dateFormat.Add(format);
//        }

//        static readonly Dictionary<Type, List<SelectOption>> EnumOptions = new Dictionary<Type, List<SelectOption>>();

//        public List<SelectOption> GetPropValue(string propName)
//        {
//            return (List<SelectOption>)this.GetType().GetProperty(propName).GetValue(this, null);
//        }

//        private static List<SelectOption> GetEnumSelect<T>()
//        {
//            if (EnumOptions.ContainsKey(typeof(T)))
//            {
//                return EnumOptions[typeof(T)];
//            }

//            var values = Enum.GetValues(typeof(T)).Cast<T>();
//            var selectOptions = values.Select(x => new SelectOption(x.DescriptionAttr(), Convert.ToInt32(x))).ToList();
//            EnumOptions[typeof(T)] = selectOptions;
//            return selectOptions;
//        }
//    }
//}
