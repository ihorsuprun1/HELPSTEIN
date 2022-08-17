using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HELPSTEIN
{
    class ID
    {
        public string ids = "|||";
        public string idhs = "|||||||";
        public string getID()
        {
           // string result = "";
            DateTime dat1 = DateTime.Now;
            string strdate = dat1.ToString("yyyyMMddHHmmss");
           
            return ids;
        }
    }
}
