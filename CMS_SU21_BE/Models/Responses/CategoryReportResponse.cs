using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_SU21_BE.Models.Responses
{
    public class CategoryReportResponse
    {
        public string categoryName { get; set; }
        public int totalArticles { get; set; }
    }
}