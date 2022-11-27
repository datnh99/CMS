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
    [RoutePrefix("api/hashtag")]
    public class HashtagController : ApiController
    {
        HashtagService hashtagService = new HashtagServiceImpl();

        [DisableCors]
        [HttpPost]
        [Route("Add/{add}")]
        public ResponseData Add([FromBody] HashtagRequest request)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                responseData.data = hashtagService.Add(request);
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
        [Route("getByNewId/{get-by-new-id}")]
        public ResponseData getByNewId(int? newID)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                List<HashtagResponse> listResult = hashtagService.GetHashtag(newID);
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
        [HttpGet]
        [Route("getPopularTags/{get-popular-tags}")]
        public ResponseData getPopularTags()
        {
            ResponseData responseData = new ResponseData();
            try
            {
                List<HashtagResponse> listResult = hashtagService.getPopularTags();
                responseData.success = true;
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
    }
}
