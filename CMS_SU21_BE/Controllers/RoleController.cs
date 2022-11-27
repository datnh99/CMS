using CMS_SU21_BE.Common.Helper;
using CMS_SU21_BE.Models;
using CMS_SU21_BE.Services;
using CMS_SU21_BE.Services.Implements;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CMS_SU21_BE.Controllers
{
    [RoutePrefix("api/role")]
    public class RoleController : ApiController
    {

        private RoleService roleService = new RoleServiceImpl();


        [DisableCors]
        [HttpGet]
        [Route("getRoles/{get-roles}")]
        public ResponseData getRoles()
        {
            ResponseData responseData = new ResponseData();
            try
            {
                List<Role> listResult = roleService.GetRoles();
                responseData.data = listResult;
                return responseData;
            }
            catch (Exception e)
            {
                responseData.success = false;
                responseData.message = e.StackTrace;
                return responseData;
            }
        }
        [DisableCors]
        [HttpPost]
        [Route("saveLoggedInUserRole/{save-logged-in-user-role}")]
        public ResponseData saveLoggedInUserRole(string role)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                var user = User as ClaimsPrincipal;
                var identity = user.Identity as ClaimsIdentity;
                identity.AddClaim(new Claim("role", role));
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
