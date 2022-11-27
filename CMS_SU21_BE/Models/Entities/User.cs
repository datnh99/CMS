using CMS_SU21_BE.Common.Base;
using System;

namespace CMS_SU21_BE.Models
{
    public class User : BaseEntity
    {
        public string account { get; set; }    

        public string firstName { get; set; }
   
        public string lastName { get; set; }

        public string address { get; set; }

        public bool gender { get; set; }

        public string phoneNumber { get; set; }

        public DateTime dateOfBirth { get; set; }

        public int avatar { get; set; }
    }
}