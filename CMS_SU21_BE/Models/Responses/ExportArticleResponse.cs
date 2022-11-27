using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_SU21_BE.Models.Responses
{
    public class ExportArticleResponse
    {
        public int id { get; set; }
        public string title { get; set; }
        public string category { get; set; }
        public string author { get; set; }
        public int totalViews { get; set; }
        public int totalComment { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime approveDate { get; set; }

    }
}