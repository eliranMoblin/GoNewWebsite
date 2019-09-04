using System;
using System.Text;
using System.Web.Mvc;
using System.Xml;

namespace Web
{
    public sealed class XmlActionResult : ActionResult
    {
        private readonly XmlDocument _document;

        public Formatting Formatting { get; set; }

        public string MimeType { get; set; }

        public XmlActionResult(XmlDocument document)
        {
            if (document == null)
                throw new ArgumentNullException(nameof(document));

            _document = document;

            // Default values
            MimeType = "text/xml";
            Formatting = Formatting.Indented;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.Clear();
            context.HttpContext.Response.ContentType = MimeType;

            using (var writer = new XmlTextWriter(context.HttpContext.Response.OutputStream, Encoding.UTF8) { Formatting = Formatting })
            {
                _document.WriteTo(writer);
            }
        }

    }

    //public class XmlTextActionResult : ActionResult
    //{
    //    private readonly object _data;

    //    public XmlTextActionResult(string data)
    //    {
    //        _data = data;
    //    }

   
    //    public override void ExecuteResult(ControllerContext context)
    //    {
    //        context.HttpContext.Response.ContentType = "text/xml";

    //        // TODO: Use your preferred xml serializer 
    //        // to serialize the model to the response stream :
    //        // context.HttpContext.Response.OutputStream
    //    }
    //}

}