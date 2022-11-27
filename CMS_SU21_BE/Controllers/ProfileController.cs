using CMS_SU21_BE.Common.Base;
using CMS_SU21_BE.Common.Helper;
using CMS_SU21_BE.Models.Requests;
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
    [RoutePrefix("api/profile")]
    public class ProfileController : ApiController
    {

        private ProfileService profileService = new ProfileServiceImpl();


        [DisableCors]
        [HttpGet]
        [Route("countProfileArticle/{count-profile-article}")]
        public ResponseData countProfileArticle(string status)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                responseData.data = profileService.countProfileArticle(status);
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

        [DisableCors]
        [HttpGet]
        [Route("countProfileComments/{count-profile-comments}")]
        public ResponseData countProfileComments()
        {
            ResponseData responseData = new ResponseData();
            try
            {
                responseData.data = profileService.countProfileComments();
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