using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_SU21_BE.Models.Requests
{
    public class ReviewArticleRequest
    {
        public int newID { get; set; }
        public String title { get; set; }
        public int? categoryID { get; set; }
        public String categoryName { get; set; }
        public String account { get; set; }
        public String reviewer { get; set; }
        public int? statusID { get; set; }
        public String statusName { get; set; }
        public DateTime createDate { get; set; }
        public DateTime updateDate { get; set; }
        public string roleCode { get; set; }
    }
}