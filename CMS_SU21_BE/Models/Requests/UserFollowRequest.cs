using CMS_SU21_BE.Common.Base;
using CMS_SU21_BE.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_SU21_BE.Models.Requests
{
    public class UserFollowRequest : BaseEntity
    {
        public string account { get; set; }

        public string follower { get; set; }

    }
}