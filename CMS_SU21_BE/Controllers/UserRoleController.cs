using CMS_SU21_BE.Common.Helper;
using CMS_SU21_BE.Models.Requests;
using CMS_SU21_BE.Models.Responses;
using CMS_SU21_BE.Services;
using CMS_SU21_BE.Services.Implements;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
namespace CMS_SU21_BE.Controllers
{
    [RoutePrefix("api/user-role")]
    public class UserRoleController : ApiController
    {
        private UserRoleService userRoleService = new UserRoleServiceImpl();

        private CategoryService categoryService = new CategoryServiceImpl();
        /// <summary>
        /// Add new UserRole
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// 
        [DisableCors]
        [Route("addUserRole/{add-user-role}")]
        [HttpPost]
        public ResponseData addUserRole([FromBody] UserRoleRequest request)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                responseData.data = userRoleService.Add(request);
                return responseData;
            }
            catch (Exception e)
            {
                responseData.success = false;
                responseData.message = e.Message.ToString();
                return responseData;
            }
        }

        [DisableCors]
        [Route("updateRoles/{update-roles}")]
        [HttpPost]
        public ResponseData updateRoles([FromBody] UserRoleRequest request)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                responseData.data = userRoleService.updateUserRoles(request);
                return responseData;
            }
            catch (Exception e)
            {
                responseData.success = false;
                responseData.message = e.Message.ToString();
                return responseData;
            }
        }

        [DisableCors]
        [HttpPost]
        [Route("search/{search}")]
        public ResponseData search([FromBody] UserRoleRequest request, int? pageSize, int? pageIndex)
        {
            ResponseData responseData = new ResponseData();
            var mapResult = new Dictionary<string, Object>();
            try
            {
                List<UserRoleResponse> listResult = userRoleService.search(request, pageSize.Value, pageIndex.Value);
                mapResult.Add("items", listResult);

                int total = userRoleService.totalSearchUserRole(request);
                mapResult.Add("total", total);

                responseData.data = mapResult;
                return responseData;
            }
            catch (Exception e)
            {
                responseData.success = false;
                responseData.message = e.Message.ToString();
                return responseData;
            }
        }

        [DisableCors]
        [HttpPost]
        [Route("deleteUserRole/{delete-user-role}")]
        public ResponseData deleteUserRole([FromBody] UserRoleRequest request)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                bool checkUpdate = false;

                // Check if role = moderator and list category != null
                if (request.RoleCode.ToLower().Equals("moderator") && request.categoryID != null)
                {
                    // update manager for list category user management
                    checkUpdate = categoryService.updateManageForCategory(request.categoryID, "null");
                    if(!checkUpdate)
                    {
                        responseData.success = checkUpdate;
                        return responseData;
                    }
                }

                // delete role of user
                bool checkDeleteRole = userRoleService.deleteById(request.Id);
                responseData.data = checkDeleteRole;
                responseData.success = true && checkDeleteRole;
                return responseData;
            }
            catch (Exception e)
            {
                responseData.success = false;
                responseData.message = e.Message.ToString();
                return responseData;
            }
        }
    }
}