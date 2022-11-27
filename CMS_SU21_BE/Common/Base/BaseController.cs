using CMS_SU21_BE.Common.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Web;

namespace CMS_SU21_BE.Common.Base
{
    public class BaseController
    {
        /// <summary>
        /// get logged in username 
        /// </summary>
        /// <returns></returns>
        public String getLoggedInUsername()
        {
            var claims = ClaimsPrincipal.Current.Identities.ToList();
            return claims[1].RoleClaimType;
        }
    }
}