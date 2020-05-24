using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateReactAppVS.Utilities
{
    public static class StrUtils
    {
        public static string RemoveLeadingSlash(string s)
        {
            if (s[0] == '\\')
            {
                return s.TrimStart('\\');
            }

            return s;
        }
    }
}
