using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_SU21_BE.Common.Base
{
    public class BaseEntity
    {
        public int id { get; set; }

        public string createdBy { get; set; }

        public DateTime createdTime { get; set; }

        public DateTime modifiedTime { get; set; }

        public string modifiedBy { get; set; }
    }
}