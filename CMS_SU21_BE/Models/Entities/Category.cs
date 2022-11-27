using CMS_SU21_BE.Common.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_SU21_BE.Models.Entities
{
    public class Category : BaseEntity
    {
        public bool deleted { get; set; }

        public string categoryName { get; set; }

        public string manager { get; set; }

        public int introductionImage { get; set; }

        public int avatarImage { get; set; }
    }
}