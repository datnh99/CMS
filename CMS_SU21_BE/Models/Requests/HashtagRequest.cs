using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_SU21_BE.Models.Requests
{
    public class HashtagRequest
    {
        public List<string> tagContent { get; set; }
        public int id { get; set; }
    }
}