using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_SU21_BE.Models.Responses
{
    public class StatisticResponse : BaseResponse
    {
        public string account { get; set; }

        public int objectId { get; set; }

        public string action { get; set; }

        public bool deleted { get; set; }
    }
}