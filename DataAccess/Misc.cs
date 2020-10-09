using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccess
{
    class Misc
    {
        public static string SafeSqlName(string name)
        {
            string result = "";

            foreach (var c in name)
            {
                if (char.IsLetterOrDigit(c) || c == '_')
                {
                    result += c;
                }
            }

            return result;
        }
    }
}