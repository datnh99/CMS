using CMS_SU21_BE.Common.Helper;
using CMS_SU21_BE.Models.Requests;
using CMS_SU21_BE.Models.Responses;
using CMS_SU21_BE.Services;
using CMS_SU21_BE.Services.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CMS_SU21_BE.Controllers
{
    [RoutePrefix("api/denounce")]
    public class DenounceArticleController : ApiController
    {
        private DenounceArticleService denounceArticleService = new DenounceArticleServiceImpl();

        [DisableCors]
        [HttpPost]
        [Route("Add/{add}")]
        public ResponseData Add([FromBody] DenounceArticleRequest request)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                responseData.data = denounceArticleService.add(request);
                return responseData;
            }
            catch (Exception e)
            {
                responseData.success = false;
                responseData.message = e.Message;
                return responseData;
            }
        }

        [DisableCors]
        [HttpGet]
        [Route("getByArticleId/{get-by-article-id}")]
        public ResponseData getByArticleId(int articleID, int pageIndex, int pageSize)
        {
            ResponseData responseData = new ResponseData();
            var mapResult = new Dictionary<string, Object>();
            try
            {
                List<DenounceArticleResponse> listResult = denounceArticleService.getByArticleId(articleID, pageIndex, pageSize);
                mapResult.Add("items", listResult);
                int total = denounceArticleService.countByArticleId(articleID);
                mapResult.Add("totals", total);
                responseData.data = mapResult;
                return responseData;
            }
            catch (Exception e)
            {
                responseData.success = false;
                responseData.message = e.Message;
                return responseData;
            }
        }

        [DisableCors]
        [HttpGet]
        [Route("checkReportByArticleID/{check-report-by-article-id}")]
        public ResponseData checkReportByArticleID(int articleID)
        {
            ResponseData responseData = new ResponseData();
            var mapResult = new Dictionary<string, Object>();
            try
            {
                int total = denounceArticleService.countByArticleId(articleID);
                mapResult.Add("items", total > 0);
                responseData.data = mapResult;
                return responseData;
            }
            catch (Exception e)
            {
                responseData.success = false;
                responseData.message = e.Message;
                return responseData;
            }
        }
    }
}