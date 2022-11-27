using CMS_SU21_BE.Common.Authentication;
using CMS_SU21_BE.Common.Helper;
using CMS_SU21_BE.Services;
using CMS_SU21_BE.Services.Implements;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CMS_SU21_BE.Controllers
{
    [RoutePrefix("api/login")]
    public class ProcessLoginController : ApiController
    {

        private JwtManager jwtManager = new JwtManager();
        [DisableCors]
        [BasicAuthentication]
        [Route("basicProcessLogin/{basic-process-login}")]
        [HttpGet]
        public string basicProcessLogin()
        {
            return "WebAPI Method Called";
        }

        [AllowAnonymous]
        [DisableCors]
        [Route("processLogin/{process-login}")]
        [HttpPost]
        public ResponseData processLogin([FromBody]UserLogin userLogin)
        {
            ResponseData responseData = new ResponseData();
            var mapResult = new Dictionary<string, Object>();
            try
            {
                var user = User as ClaimsPrincipal;
                var identity = user.Identity as ClaimsIdentity;
                foreach (var claim in user.Claims.ToList())
                {
                    identity.RemoveClaim(claim);
                }
                Dictionary<string, Object> profile = jwtManager.getUserNameFromGoogleIDToken(userLogin.idToken);
                mapResult = jwtManager.GenerateToken(profile,false);
                mapResult.Add("gg_profile", profile);
                responseData.data = mapResult;
                responseData.success = true;
                return responseData;
            }
            catch (Exception e)
            {
                responseData.success = false;
                responseData.message = e.StackTrace;
                return responseData;
            }
        }

        [AllowAnonymous]
        [DisableCors]
        [Route("processLogout/{process-logout}")]
        [HttpPost]
        public ResponseData processLogout()
        {
            ResponseData responseData = new ResponseData();
            try
            {

                var user = User as ClaimsPrincipal;
                var identity = user.Identity as ClaimsIdentity;
                foreach (var claim in user.Claims.ToList())
                {
                    identity.RemoveClaim(claim);
                }
                responseData.success = true;
                return responseData;
            }
            catch (Exception e)
            {
                responseData.success = false;
                responseData.message = e.StackTrace;
                return responseData;
            }
        }
        [AllowAnonymous]
        [DisableCors]
        [Route("verifyToken/{verify-token}")]
        [HttpGet]
        public ResponseData verifyToken(string token)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
                responseData.success = true;
                return responseData;
            }
            catch (Exception e)
            {
                responseData.success = false;
                responseData.message = e.StackTrace;
                return responseData;
            }
        }
    }
}