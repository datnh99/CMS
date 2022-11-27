using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_SU21_BE.Models.Responses
{
    public class TopWriterResponse
    {
        public int totalArticle { get; set; }
        public int totalFollower { get; set; }
        public int totalViews { get; set; }
        public int avatar { get; set; }
        public string account { get; set; }
        public string lastName { get; set; }
        public string firstName { get; set; }

    }
}