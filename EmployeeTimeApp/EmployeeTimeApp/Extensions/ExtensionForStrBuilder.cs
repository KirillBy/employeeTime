using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTimeApp.Class
{
    //Extension for StringBuider, which types ... if str less than value
        public static class StringHelper
        {
            public static string Shorten(this StringBuilder str, int value)
            {
                return
                    value > 3 && str.Length > value ?
                    str.ToString().Substring(0, value - 3) + "..." : str.ToString();
            }
        }  
}
