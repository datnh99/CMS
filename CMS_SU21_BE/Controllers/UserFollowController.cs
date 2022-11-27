using CMS_SU21_BE.Common.Helper;
using CMS_SU21_BE.Models.Requests;
using CMS_SU21_BE.Models.Responses;
using CMS_SU21_BE.Services;
using CMS_SU21_BE.Services.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CMS_SU21_BE.Controllers
{
    [RoutePrefix("api/user-follow")]
    public class UserFollowController : ApiController
    {
        private UserFollowService userFollowService = new UserFollowServiceImpl();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [DisableCors]
        [HttpGet]
        [Route("getFollowedUser/{get-followed-user}")]
        public ResponseData checkFollowedUser(string account)
        {
            ResponseData responseData = new ResponseData();
            try
            {

                responseData.data = userFollowService.getFollowedUser(account);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [DisableCors]
        [HttpPost]
        [Route("add/{add}")]
        public ResponseData add(string account)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                UserFollowRequest request = new UserFollowRequest();
                request.account = account;
                responseData.data = userFollowService.add(request);
                return responseData;
            }
            catch (Exception e)
            {
                responseData.success = false;
                responseData.message = e.StackTrace;
                return responseData;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [DisableCors]
        [HttpPost]
        [Route("update/{update}")]
        public ResponseData update(UserFollowRequest request)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                responseData.data = userFollowService.update(request);
                return responseData;
            }
            catch (Exception e)
            {
                responseData.success = false;
                responseData.message = e.StackTrace;
                return responseData;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [DisableCors]
        [HttpPost]
        [Route("delete/{delete-by-account}")]
        public ResponseData deleteByAccount(string account)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                responseData.data = userFollowService.deleteByAccount(account);
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