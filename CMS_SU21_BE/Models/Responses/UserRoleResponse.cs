using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_SU21_BE.Models.Responses
{
    public class UserRoleResponse
    {
        public int index { get; set; }
        public int id { get; set; }
        public string account { get; set; }
        public string roleCode { get; set; }
        public string roleName { get; set; }
        public string createdBy { get; set; }
        public DateTime createdTime { get; set; }
        public string modifiedBy { get; set; }
        public DateTime modifiedTime { get; set; }
        public List<int> categoryMangement { get; set; }
    }
}