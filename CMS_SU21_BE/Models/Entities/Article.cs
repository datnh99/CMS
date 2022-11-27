using CMS_SU21_BE.Common.Base;
using System;

namespace CMS_SU21_BE.Models.Entities
{
    public class Article : BaseEntity
    {
        public string content { get; set; }

        public int categoryID { get; set; }

        public DateTime approveDate { get; set; }

        public string author { get; set; }

        public int status { get; set; }

        public int totalViews { get; set; }

        public bool deleted { get; set; }

        public string sapo { get; set; }

        public string title { get; set; }

        public int introductionImage { get; set; }

        public string relatedArticle { get; set; }

        public bool clone { get; set; }

        public string fullTextContent { get; set; }

        public bool pinned { get; set; }
    }
}