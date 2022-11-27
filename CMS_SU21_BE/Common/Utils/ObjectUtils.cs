using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_SU21_BE.Common.Utils
{
    public static class ObjectUtils
    {
    
        public static bool isNullOrEmpty<T>(this IEnumerable<T> data)
        {
            return data == null && !data.Any();
        }
     
    }
}