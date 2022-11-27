using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_SU21_BE.Models.Responses
{
    public class HashtagResponse
    {
        public string tagContent { get; set; }
        public int articleId { get; set; }
        public int totalArticle { get; set; }


    }
}