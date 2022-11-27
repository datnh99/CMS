using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_SU21_BE.Models.Responses
{
    public class UserFollowResponse : BaseResponse
    {
        public string account { get; set; }

        public string follower { get; set; }
    }
}