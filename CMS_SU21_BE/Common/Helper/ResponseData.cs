using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_SU21_BE.Common.Helper
{
    public class ResponseData
    {
        public String message { get; set; }
        public Object data { get; set; }
        public Boolean success { get; set; }
    }
}