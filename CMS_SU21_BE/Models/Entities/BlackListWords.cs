using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_SU21_BE.Models.Entities
{
    public class BlackListWords
    {
        public int id { get; set; }
        public string content { get; set; }
        public string createdBy { get; set; }
        public DateTime createdTime { get; set; }
        public int index { get; set; }

    }
}