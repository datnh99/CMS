using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_SU21_BE.Models.Responses
{
    public class ReviewArticleResponse
    {
        public int index { get; set; }
        public int? newID { get; set; }
        public String content { get; set; }
        public String sapo { get; set; }
        public String title { get; set; }
        public int? categoryID { get; set; }
        public String categoryName { get; set; }
        public String account { get; set; }
        public int? statusID { get; set; }
        public String statusName { get; set; }
        public DateTime createDate { get; set; }
        public DateTime updateDate { get; set; }
        public bool? deleteFlag { get; set; }
        public int originalArticleID { get; set; }
        public String reviewer { get; set; }
        public String note { get; set; }
    }
}