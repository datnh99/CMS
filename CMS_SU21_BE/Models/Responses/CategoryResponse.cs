using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_SU21_BE.Models.Responses
{
    public class CategoryResponse
    {
        public int index { get; set; }
        public int id { get; set; }
        public string createdBy { get; set; }
        public DateTime createdTime { get; set; }
        public string modifiedBy { get; set; }
        public DateTime modifiedTime { get; set; }
        public int deleted { get; set; }
        public string categoryName { get; set; }
        public string manager { get; set; }
        //public string categoryCode { get; set; }
        public int introductionImage { get; set; }
        public List<string> account_temp { get; set; }
        public int totalArticle { get; set; }

    }
}