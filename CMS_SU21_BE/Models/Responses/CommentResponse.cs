using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_SU21_BE.Models.Responses
{
    public class CommentResponse
    {
        public int id { get; set; }

        public int articleID { get; set; }

        public string content { get; set; }

        public int parentID { get; set; }

        public bool deleted { get; set; }

        public DateTime modifiedTime { get; set; }

        public DateTime createdTime { get; set; }

        public string createdBy { get; set; }

        public string modifiedBy { get; set; }

        public string displayName { get; set; }

    }
}