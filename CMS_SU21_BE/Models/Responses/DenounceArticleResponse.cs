using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_SU21_BE.Models.Responses
{
    public class DenounceArticleResponse
    {
        public int index { get; set; }
        public int id { get; set; }
        public string createdBy { get; set; }
        public DateTime createdTime { get; set; }
        public string modifiedBy { get; set; }
        public DateTime modifiedTime { get; set; }
        public int deleted { get; set; }
        public int articleID { get; set; }
        public string reason { get; set; }
        public string reasonOther { get; set; }
        public string reasonName { get; set; }
    }
}