using CMS_SU21_BE.Common.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace CMS_SU21_BE.Common.Base
{
    public class BaseRepository : ApiController
    {
        /// <summary>
        /// get logged in username 
        /// </summary>
        /// <returns></returns>
        public String getLoggedInUsername()
        {
            var user = User as ClaimsPrincipal;
            var identity = user.Identity as ClaimsIdentity;
            var claims = user.Claims.Where(c => c.Type == "username")
                   .Select(c => c.Value).FirstOrDefault();
            return claims;
        }
        public int getLoggedInUserId()
        {
            var user = User as ClaimsPrincipal;
            var identity = user.Identity as ClaimsIdentity;
            var claims = user.Claims.Where(c => c.Type == "userId")
                   .Select(c => c.Value).FirstOrDefault();
            if (!string.IsNullOrEmpty(claims))
            {
                return Convert.ToInt32(claims);
            }
            return 0;
        }
        public String getLoggedInUserRole()
        {
            var user = User as ClaimsPrincipal;
            var identity = user.Identity as ClaimsIdentity;
            var claims = user.Claims.Where(c => c.Type == "role")
                   .Select(c => c.Value).FirstOrDefault();
            return claims;
        }
    }
}