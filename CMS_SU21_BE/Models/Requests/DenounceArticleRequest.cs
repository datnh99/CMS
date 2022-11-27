using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_SU21_BE.Models.Requests
{
    public class DenounceArticleRequest
    {
        public int id { get; set; }
        public string createdBy { get; set; }
        public DateTime createdTime { get; set; }
        public string modifiedBy { get; set; }
        public DateTime modifiedTime { get; set; }
        public int deleted { get; set; }
        public int articleID { get; set; }
        public List<int> reason { get; set; }
        public string reasonOther { get; set; }
    }
}