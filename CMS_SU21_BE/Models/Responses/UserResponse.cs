using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_SU21_BE.Models.Responses
{
    public class UserResponse
    {
        public int userId { get; set; }
        public int roleId { get; set; }
        public string account { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }
        public string roleCode { get; set; }
        public string roleName { get; set; }

        public string createdBy { get; set; }
        public DateTime createdTime { get; set; }
        public string modifiedBy { get; set; }
        public DateTime modifiedTime { get; set; }
        public string address { get; set; }
        public bool gender { get; set; }
        public string phoneNumber { get; set; }
        public DateTime dateOfBirth { get; set; }
        public int avatar { get; set; }
    }
}