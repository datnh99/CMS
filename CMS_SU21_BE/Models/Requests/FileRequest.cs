using CMS_SU21_BE.Common.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_SU21_BE.Models.Entities
{
    public class FileRequest : BaseEntity
    { 
        public string name { get; set; }
        public long size { get; set; }
        public string link { get; set; }
        public int articleId { get; set; }
        public string originalName { get; set; }

    }
}