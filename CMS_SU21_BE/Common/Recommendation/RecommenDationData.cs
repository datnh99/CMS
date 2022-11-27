using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_SU21_BE.Common.Recommendation
{
    public class RecommenDationData 
    {
        public string action { get; set; }
        public int userId { get; set; }
        public long time { get; set; }
        public List<int> listArticleId { get; set; }

    }
}