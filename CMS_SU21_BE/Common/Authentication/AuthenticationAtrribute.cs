using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace CMS_SU21_BE.Common.Authentication
{
    public class AuthenticationAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.ActionDescriptor.ActionName == "processLogin"
                || actionContext.ActionDescriptor.ActionName == "getImage"
                || actionContext.ActionDescriptor.ActionName == "basicProcessLogin"
                || actionContext.ActionDescriptor.ActionName == "saveLoggedInUserRole"
                || actionContext.ActionDescriptor.ActionName == "uploadImage"
                || actionContext.ActionDescriptor.ActionName == "addUser"
                || actionContext.ActionDescriptor.ActionName == "searchUserByAccount"
                || actionContext.ActionDescriptor.ActionName == "getAttachment"
                || actionContext.ActionDescriptor.ActionName == "getImageThumb"
            )
            {
                return;

            }
            if (actionContext.Request.Headers.Authorization != null)
            {
                HttpRequestMessage request = actionContext.Request;
                AuthenticationHeaderValue authorization = request.Headers.Authorization;

                var authToken = authorization.Parameter;

                if (authorization.Scheme != "Bearer")
                {
                    return;
                }

                // 4. If there are credentials that the filter understands, try to validate them.
                // 5. If the credentials are bad, set the error result.
                // at 0th postion of array we get username and at 1st we get password  
                if (ValidateToken(authToken))
                {
                    // setting current principle  
                }
                else
                {
                    actionContext.Response = actionContext.Request
                        .CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
            else
            {
                actionContext.Response = actionContext.Request
                    .CreateResponse(HttpStatusCode.Unauthorized);
            }
        }

        private static bool ValidateToken(string token)
        {
            try
            {
                String username = null;

                var simplePrinciple = JwtManager.GetPrincipal(token);
                var identity = simplePrinciple.Identity as ClaimsIdentity;

                if (identity == null || !identity.IsAuthenticated)
                    return false;

                var usernameClaim = identity.FindFirst(ClaimTypes.Name);
                username = usernameClaim?.Value;

                if (string.IsNullOrEmpty(username))
                    return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace.ToString());
                return false;
            }
            return true;
        }
    }
}