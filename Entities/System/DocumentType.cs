using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Entities.System
{
    public class DocumentType
    {
        private static Dictionary<int, DocumentType> _documentTypes = new Dictionary<int, DocumentType>();

        public static readonly DocumentType WebsiteSetting = new DocumentType(1, "WebsiteSetting");
        public static readonly DocumentType Provider = new DocumentType(2, "Provider");
        public static readonly DocumentType Product = new DocumentType(3, "Product");
        public static readonly  DocumentType Groups = new DocumentType(4, "Groups");
        public static readonly DocumentType AlertSettings= new DocumentType(5, "AlertSettings");



        public static readonly DocumentType Customer = new DocumentType(20, "Customer");
        public static readonly DocumentType Application = new DocumentType(21, "Application");
        public static readonly DocumentType Campaign = new DocumentType(22, "Campaign");
        public static readonly DocumentType Contact = new DocumentType(23, "Contact");
        //public static readonly DocumentType MailSetting = new DocumentType(24, "MailSetting");
        public static readonly DocumentType Condition = new DocumentType(25, "Condition");
        public static readonly DocumentType Banner = new DocumentType(26, "Banner");
        public static readonly DocumentType Template=new DocumentType(27,"Template");
        public static readonly DocumentType EmailToUser = new DocumentType(28, "EmailToUser");
        public static readonly DocumentType SmsToUser = new DocumentType(29, "SmsToUser");
        public static readonly DocumentType FtpSite = new DocumentType(30, "FtpSite");
        public static readonly DocumentType CarouselBanner = new DocumentType(31, "CarouselBanner");



        public static readonly DocumentType Lead = new DocumentType(70, "DocumentLead");

        public static  readonly  DocumentType VersionControl = new DocumentType(90, "VersionControl");


        public static readonly DocumentType WebsitePage = new DocumentType(100, "WebsitePage");


        public byte Value { get; protected set; }
        public string Name { get; protected set; }

        protected DocumentType(byte internalValue, string name)
        {
            Value = internalValue;
            Name = name;
            _documentTypes.Add(internalValue, this);
        }

        //public static List<SelectOption> GetSelectOptions()
        //{
        //    return _documentTypes.Select(x => new SelectOption(x.Value.Name, x.Key)).ToList();
        //}

        public static explicit operator DocumentType(int x)
        {
            if (!_documentTypes.ContainsKey(x))
                return new DocumentType((byte)x, x.ToString());
            return _documentTypes[x];
        }

        public static explicit operator DocumentType(byte x)
        {
            if (!_documentTypes.ContainsKey(x))
                return new DocumentType(x, x.ToString());
            return _documentTypes[x];
        }

        public static explicit operator byte(DocumentType x)
        {
            return (byte)x.Value;
        }

        public static explicit operator int(DocumentType x)
        {
            return x.Value;
        }

        [OnSerializing]
        internal void OnSerializingMethod(StreamingContext context)
        {
        }

        [OnSerialized]
        internal void OnSerializedMethod(StreamingContext context)
        {
        }

        [OnDeserializing]
        internal void OnDeserializingMethod(StreamingContext context)
        {
        }

        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
        }

        public override string ToString()
        {
            return Value.ToString();
        }


        public static bool operator ==(int num, DocumentType documentType)
        {
            return num == documentType.Value;
        }
        public static bool operator !=(int num, DocumentType documentType)
        {
            return num != documentType.Value;

        }
    }
}
