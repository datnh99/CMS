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
    [RoutePrefix("api/category")]
    public class CategoryController : ApiController
    {

        private CategoryService categoryService = new CategoryServiceImpl();


        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryName"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [DisableCors]
        [HttpGet]
        [Route("Search/{search}")]
        public ResponseData Search(string categoryName, int? pageSize, int? pageIndex)
        {
            ResponseData responseData = new ResponseData();
            var mapResult = new Dictionary<string, Object>();
            try
            {
                List<CategoryResponse> listResult = categoryService.Search(categoryName, pageSize.Value, pageIndex.Value);
                mapResult.Add("items", listResult);
                int total = categoryService.countBySearch(categoryName);
                mapResult.Add("totals", total);
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
        [Route("getAllCategory/{get-all-category}")]
        public ResponseData getAllCategory()
        {
            ResponseData responseData = new ResponseData();
            var mapResult = new Dictionary<string, Object>();
            try
            {
                List<CategoryResponse> listResult = categoryService.getAll();
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
        /// <returns></returns>
        [DisableCors]
        [HttpGet]
        [Route("totalCategory/{total-category}")]
        public ResponseData totalCategory()
        {
            ResponseData responseData = new ResponseData();
            try
            {
                responseData.data = categoryService.totalCategory();
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
        /// <param name="categoryName"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [DisableCors]
        [HttpGet]
        [Route("SearchByNameAndManager/{search-by-name-and-manager}")]
        public ResponseData SearchByNameAndManager(string categoryName, string account, int? pageSize, int? pageIndex)
        {
            ResponseData responseData = new ResponseData();
            var mapResult = new Dictionary<string, Object>();
            try
            {
                List<CategoryResponse> listResult = categoryService.SearchByNameAndManager(categoryName, account, pageSize, pageIndex);
                mapResult.Add("items", listResult);
                int total = categoryService.countByNameAndManager(categoryName, account);
                mapResult.Add("totals", total);
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
        [Route("Add/{add}")]
        public ResponseData Add([FromBody] CategoryRequest request)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                responseData.data = categoryService.Add(request);
                return responseData;
            }
            catch (Exception e)
            {
                responseData.success = false;
                responseData.message = e.Message;
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
        [Route("updateCategory/{update-category}")]
        public ResponseData updateCategory([FromBody] CategoryRequest request)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                responseData.data = categoryService.updateCategory(request);
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
        /// <param name="categoryID"></param>
        /// <returns></returns>
        [DisableCors]
        [HttpPost]
        [Route("Delete/{delete}")]
        public ResponseData Delete(int categoryID)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                responseData.data = categoryService.DeleteById(categoryID);
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
        [Route("getCategoryById/{get-category-by-id}")]
        public ResponseData getCategoryById(int id)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                responseData.data = categoryService.getCategoryById(id);
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
        [Route("searchCategoryToAddManager/{search-to-add-manager}")]
        public ResponseData searchCategoryToAddManager(string categoryName, int? pageSize, int? pageIndex)
        {
            ResponseData responseData = new ResponseData();
            var mapResult = new Dictionary<string, Object>();
            try
            {
                List<CategoryResponse> listResult = categoryService.searchCategoryToAddManager(categoryName, pageSize.Value, pageIndex.Value);
                mapResult.Add("items", listResult);
                int total = categoryService.countBySearchCategoryToAddManager(categoryName);
                mapResult.Add("totals", total);
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
        [Route("getByManager/{get-by-manager}")]
        public ResponseData getByManager(string manager)
        {
            ResponseData responseData = new ResponseData();
            var mapResult = new Dictionary<string, Object>();
            try
            {
                List<CategoryResponse> listResult = categoryService.getByManager(manager);
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
    }
}
