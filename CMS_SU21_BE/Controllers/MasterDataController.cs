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
    [RoutePrefix("api/master-data")]
    public class MasterDataController : ApiController
    {

        private MasterDataService masterDataService = new MasterDataServiceImpl();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="masterDataName"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [DisableCors]
        [HttpGet]
        [Route("search/{search}")]
        public ResponseData search(string masterDataName, int? pageSize, int? pageIndex)
        {
            ResponseData responseData = new ResponseData();
            var mapResult = new Dictionary<string, Object>();
            try
            {
                List<MasterDataResponse> listResult = masterDataService.Search(masterDataName, pageSize.Value, pageIndex.Value);
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
        /// <param name="lowerCode"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [DisableCors]
        [HttpGet]
        [Route("getByTypeAndCode/{get-by-type-and-code}")]
        public ResponseData getByTypeAndCode(string lowerCode, string type)
        {
            ResponseData responseData = new ResponseData();
            var mapResult = new Dictionary<string, Object>();
            try
            {
                MasterDataResponse masterDataResponse = masterDataService.getByTypeAndCode(lowerCode, type);
                mapResult.Add("items", masterDataResponse);
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



        [HttpGet]
        [Route("getByType/{get-by-type}")]
        public ResponseData getByType(string type, int? pageSize, int? pageIndex)
        {
            ResponseData responseData = new ResponseData();
            var mapResult = new Dictionary<string, Object>();
            try
            {
                List<MasterDataResponse> masterDataResponse = masterDataService.getByType(type,pageSize.Value,pageIndex.Value);
                mapResult.Add("items", masterDataResponse);
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
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getAllByType/{get-all-by-type}")]
        public ResponseData getAllByType(string type)
        {
            ResponseData responseData = new ResponseData();
            var mapResult = new Dictionary<string, Object>();
            try
            {
                List<MasterDataResponse> masterDataResponse = masterDataService.getAllByType(type);
                mapResult.Add("items", masterDataResponse);
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
        public ResponseData Add(MasterDataRequest request)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                responseData.data = masterDataService.Add(request);
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
        [Route("delete/{delete}")]
        public ResponseData delete(int id)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                responseData.data = masterDataService.DeleteById(id);
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