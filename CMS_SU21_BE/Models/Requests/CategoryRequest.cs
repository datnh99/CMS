using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_SU21_BE.Models.Requests
{
    public class CategoryRequest
    {
        public int id { get; set; }
        public string createdBy { get; set; }
        public DateTime createdTime { get; set; }
        public string modifiedBy { get; set; }
        public DateTime modifiedTime { get; set; }
        public int deleted { get; set; }
        public string categoryName { get; set; }
        //public string categoryCode { get; set; }
        public string manager { get; set; }
        public List<string> account { get; set; }
        public int introductionImage { get; set; }
    }
}