using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace TrustSystems3.BL.Utils
{
    public static class ResourceUtils
    {
        public static string GetRootCategory(string name)
        {
            return "Root" + name.Replace("and", "").Replace("of", "").Replace("&", "").Replace(" ", "");
        }
    }
}