using CMS_SU21_BE.Common.Helper;
using CMS_SU21_BE.Models;
using CMS_SU21_BE.Models.Requests;
using CMS_SU21_BE.Models.Responses;
using CMS_SU21_BE.Services;
using CMS_SU21_BE.Services.Implements;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CMS_SU21_BE.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : ApiController 
    {
        UserService userService = new UserServiceImpl();

        [DisableCors]
        [HttpGet]
        [Route("searchUserByAccount/{search-user-by-account}")]
        public ResponseData searchUserByAccount(string account)
        {
            ResponseData responseData = new ResponseData();
            var mapResult = new Dictionary<string, Object>();
            try
            {
                List<UserResponse> listResult = userService.searchUserByAccount(account);
                mapResult.Add("items", listResult);

                responseData.data = mapResult;
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
        [HttpGet]
        [Route("getUserByAccount/{get-user-by-account}")]
        public ResponseData getUserByAccount(string account)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                UserResponse result = userService.getUserDetailByAccount(account);
                responseData.success = true;
                responseData.data = result;
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
        [Route("addUser/{addUser}")]
        public ResponseData addUser([FromBody] UserRequest request)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                int id = userService.Add(request);
                responseData.data = id > 0;
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
        [Route("editUserInfor/{edit-user-infor}")]
        public ResponseData editUserInfor([FromBody] UserRequest request)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                bool checkEditUserInfor = userService.editUserInfor(request);
                responseData.data = checkEditUserInfor;
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
