using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resources.Abstract
{
    public interface IResourceProvider
    {
        string GetResource(string name, string culture);                
    }
}
