using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_SU21_BE.Models.Requests
{
    public class SubmissionRequest
    {
        public string title { get; set; }

        public int categoryID { get; set; }

        public string account { get; set; }

        public int status { get; set; }

        public String lstArticlesId;

        public string reviewer { get; set; }

        public string hashtag { get; set; }

    }
}