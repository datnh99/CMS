using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_SU21_BE.Models.Requests
{
    public class CategoryManagementRequest
    {
        public int categoryID { get; set; }
        public int id { get; set; }
        public string createBy { get; set; }
        public DateTime createDate { get; set; }
        public string account { get; set; }
    }
}