using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Homework.Models
{
    public class RequestLoger
    {
        public static void DoLog (string type, string clIP, string reURL)
        {
            using (StreamWriter sw = new StreamWriter("RequestLog.txt", true, System.Text.Encoding.Default))
            {
                sw.WriteLine("{0} ----- {1} ----- {2}", type, clIP, reURL);
            }
        }
    }
}
