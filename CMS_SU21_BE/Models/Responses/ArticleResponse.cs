using CMS_SU21_BE.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_SU21_BE.Models.Responses
{
    public class ArticleResponse : BaseResponse
    {

        public int index { get; set; }
        public string content { get; set; }

        public int categoryID { get; set; }

        public string author { get; set; }

        public int status { get; set; }

        public int totalViews { get; set; }

        public DateTime approveDate { get; set; }

        public bool deleted { get; set; }

        public string sapo { get; set; }

        public string title { get; set; }

        public int introductionImage { get; set; }

        public List<string> tagContent { get; set; }

        public string relatedArticle { get; set; }

        public bool clone { get; set; }

        public String categoryName { get; set; }

        public String statusName { get; set; }

        public int originalArticleID { get; set; }

        public bool followedUser { get; set; }

        public long numberReport { get; set; }
        public int totalComment { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }

        public bool pinned { get; set; }

        public List<FileResponse> attachments { get; set; }
        public int authorAvatar { get; set; }


    }
}