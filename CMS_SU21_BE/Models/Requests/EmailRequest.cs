using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_SU21_BE.Models.Requests
{
    public class EmailRequest
    {
        public string mailTo { get; set; }
        public string linkReference { get; set; }


    }
}