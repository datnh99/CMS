using CMS_SU21_BE.Common.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_SU21_BE.Models.Entities
{
    public class ArticleVersion : BaseEntity
    {
        public int articleID { get; set; }
        public int oldArticleID { get; set; }
    }
}