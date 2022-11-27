using CMS_SU21_BE.Common.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_SU21_BE.Models.Entities
{
    public class DenounceArticle : BaseEntity
    {
        public bool deleted { get; set; }

        public int articleID { get; set; }

        public string reason { get; set; }

        public string reasonOther { get; set; }
    }
}