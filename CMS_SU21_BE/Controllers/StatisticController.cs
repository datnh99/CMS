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
    [RoutePrefix("api/statistic")]
    public class StatisticController : ApiController
    {
        private StatisticService statisticService = new StatisticServiceImpl();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [DisableCors]
        [HttpGet]
        [Route("search/{search-by-account}")]
        public ResponseData searchByAccount(string account, int? pageSize, int? pageIndex)
        {
            ResponseData responseData = new ResponseData();
            var mapResult = new Dictionary<string, Object>();
            try
            {
                List<StatisticResponse> listResult = statisticService.search(account, pageSize.Value, pageIndex.Value);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [DisableCors]
        [HttpPost]
        [Route("add/{add}")]
        public ResponseData add(StatisticRequest request)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                responseData.data = statisticService.add(request);
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
        public ResponseData update(StatisticRequest request)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                responseData.data = statisticService.update(request);
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
        [Route("delete/{delete-by-id}")]
        public ResponseData deleteById(int? id)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                responseData.data = statisticService.deleteById(id.Value);
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