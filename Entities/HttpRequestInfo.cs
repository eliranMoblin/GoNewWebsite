using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class HttpRequestInfo
    {
        public HttpRequestInfo()
        {
            Date = DateTime.UtcNow;
            Id = Guid.NewGuid();

        }


        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public Guid SessionId { get; set; }

        public Guid MachineId { get; set; }

        public Guid? UserId { get; set; }

        public string Device { get; set; }

        public string UserHostAddress { get; set; }

        public string UrlReferrer { get; set; }

        public string UserAgent { get; set; }

        public string RequestUrl { get; set; }

        public string RequestUrlHost { get; set; }

        public string RouteValues { get; set; }

        public string Headers { get; set; }

        public string Cookies { get; set; }

        public string Querystring { get; set; }

        public string IpCountryCode { get; set; }

        public string HeaderFrom { get; set; }

        public byte? SourceTypeId { get; set; }

        public string Source { get; set; }

        public long? ipNumber { get; set; }

        public bool isNewSession { get; set; }

        public bool isNewVisitor { get; set; }
    }
}
