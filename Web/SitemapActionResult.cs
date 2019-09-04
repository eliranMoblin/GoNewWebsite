using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Xml;
using Common.ExtentionMethods;

namespace Web
{
    public class SitemapActionResult : ActionResult
    {
        private readonly List<SitemapItem> _sitemapItems;
        private readonly string _website;

        public SitemapActionResult(List<SitemapItem> sitemapItems, string website)
        {
            _sitemapItems = sitemapItems;
            _website = website.TrimEnd('/');
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.ContentType = "text/xml";
            const string nameSpaceUri = @"http://www.w3.org/1999/xhtml";
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "\t";
            //settings.OmitXmlDeclaration = true;
            using (XmlWriter writer = XmlWriter.Create(context.HttpContext.Response.Output, settings))
            {
                writer.WriteStartElement("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9");
                writer.WriteAttributeString("xmlns", "xhtml", null, nameSpaceUri);
                foreach (var sitemapItem in _sitemapItems)
                {
                    writer.WriteStartElement("url");
                    if (!sitemapItem.AbsoluteUrl.IsNullOrWhiteSpace())
                    {
                        writer.WriteElementString("loc", sitemapItem.AbsoluteUrl);
                    }
                    else
                    {
                        writer.WriteElementString("loc", $"{_website}/{sitemapItem.Url.Trim('/')}");
                    }
                    if (sitemapItem.LastModify != null)
                    {
                        writer.WriteElementString("lastmod", $"{sitemapItem.LastModify:yyyy-MM-dd}");
                    }
                    foreach (var sitemapItemLink in sitemapItem.Links)
                    {
                        writer.WriteStartElement("xhtml", "link", nameSpaceUri);
                        writer.WriteAttributeString("rel", sitemapItemLink.rel);
                        writer.WriteAttributeString("hreflang", sitemapItemLink.hreflang);
                        writer.WriteAttributeString("href", sitemapItemLink.href);
                        writer.WriteEndElement();
                    }
                    if (sitemapItem.ChangeFrequently.HasValue)
                    {
                        writer.WriteElementString("changefreq", sitemapItem.ChangeFrequently.Value.ToString().ToLower());
                    }
                    if (sitemapItem.Priority.HasValue)
                    {
                        writer.WriteElementString("priority", sitemapItem.Priority.ToString());
                    }
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.Flush();
                writer.Close();
            }
        }
    }


    public class SitemapItem
    {
        public ChangeFrequently? ChangeFrequently { get; set; }

        public DateTime? LastModify { get; set; }

        public string AbsoluteUrl { get; set; }

        public string Url { get; set; }

        public double? Priority { get; set; }

        public List<SitemapLinkItem> Links { get; private set; }

        public SitemapItem()
        {
            Links = new List<SitemapLinkItem>();
        }

        public override string ToString()
        {
            return $"AbsoluteUrl: {AbsoluteUrl} Url:{Url}";
        }
    }

    public enum ChangeFrequently
    {
        Always,
        Hourly,
        Daily,
        Weekly,
        Monthly,
        Yearly,
        Never
    }

    public class SitemapLinkItem
    {
        public string rel { get; set; }
        public string hreflang { get; set; }
        public string href { get; set; }
    }
}