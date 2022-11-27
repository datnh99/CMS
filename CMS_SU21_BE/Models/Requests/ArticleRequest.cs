using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_SU21_BE.Models.Requests
{
    public class ArticleRequest
    {
        public int id { get; set; }
        public string content { get; set; }

        public int categoryID { get; set; }

        public string author { get; set; }

        public int status { get; set; }

        public int totalViews { get; set; }

        public DateTime approveDate { get; set; }

        public string modifiedBy { get; set; }

        public bool deleted { get; set; }

        public string sapo { get; set; }

        public string title { get; set; }

        public int introductionImage { get; set; }

        public List<string> tagContent { get; set; }

        public string relatedArticle { get; set; }

        public string note { get; set; }

        public bool confirm { get; set; }
        public bool clone { get; set; }

        public bool updateArticleByReviewer { get; set; }

    }
}