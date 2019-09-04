using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Cache;

namespace Entities.System
{
    public class ImageBaseEntity : BaseEntity
    {
        public List<Image> Images { get; set; }

        protected ImageBaseEntity()
        {
            Images = new List<Image>();
        }
    }
}
