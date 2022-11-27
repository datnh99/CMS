using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_SU21_BE.Models.Requests
{
    public class AddReviewArticleRequest
    {
        public string account { get; set; }
        public bool confirm { get; set; }
        public string note { get; set; }
        public int articleId { get; set; }
    }
}