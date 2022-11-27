using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_SU21_BE.Models.Requests
{
    public class UserRoleRequest
    {
        public int Id { get; set; }
        public string createdBy { get; set; }
        public DateTime createdTime { get; set; }
        public string modifiedBy { get; set; }
        public DateTime modifiedTime { get; set; }
        public string Account { get; set; }
        public string RoleCode { get; set; }
        public List<string> RoleCodeOfUser { get; set; }
        public List<int> categoryID { get; set; }
        public List<int> oldCategoryID { get; set; }

    }
}