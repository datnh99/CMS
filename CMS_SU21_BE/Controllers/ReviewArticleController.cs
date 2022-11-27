using CMS_SU21_BE.Common.Helper;
using CMS_SU21_BE.Common.Constant;
using CMS_SU21_BE.Models.Entities;
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
    [RoutePrefix("api/review-article")]
    public class ReviewArticleController : ApiController
    {
        ReviewArticleService reviewArticleService = new ReviewArticleServiceImpl();

        MasterDataService masterDataService = new MasterDataServiceImpl();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reviewArticleRequest"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [DisableCors]
        [HttpPost]
        [Route("searchReviewArticle/{search-review-article}")]
        public ResponseData searchReviewArticle([FromBody] ReviewArticleRequest reviewArticleRequest, int? pageSize, int? pageIndex)
        {
            ResponseData responseData = new ResponseData();
            var mapResults = new Dictionary<string, Object>();
            try
            {
                mapResults.Add("items", reviewArticleService.searchRequestReviewArticle(reviewArticleRequest, pageSize, pageIndex));

                int total = reviewArticleService.countRequestReviewArticle(reviewArticleRequest);
                mapResults.Add("totals", total);

                responseData.data = mapResults;
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
        [HttpPost]
        [Route("addReviewArticle/{add-review-article}")]
        public ResponseData addReviewArticle([FromBody] AddReviewArticleRequest addReviewArticleRequest)    
        {
            ResponseData responseData = new ResponseData();
            var mapResults = new Dictionary<string, Object>();
            try
            {
                responseData.success = reviewArticleService.addReviewArticle(addReviewArticleRequest);
                return responseData;
            } catch(Exception ex)
            {
                responseData.success = false;
                responseData.message = ex.Message;
                return responseData;
            }

        }

        [DisableCors]
        [HttpPost]
        [Route("reviewArticleByReviewer/{review-article-by-reviewer}")]
        public ResponseData reviewArticleByReviewer([FromBody] ArticleRequest articleRequest)
        {
            ResponseData responseData = new ResponseData();
            var mapResults = new Dictionary<string, Object>();
            try
            {
                responseData.success = reviewArticleService.reviewArticleByReviewer(articleRequest);
                return responseData;
            }
            catch (Exception ex)
            {
                responseData.success = false;
                responseData.message = ex.Message;
                return responseData;
            }

        }

        [DisableCors]
        [HttpGet]
        [Route("getArticleReviewed/{get-article-reviewed}")]
        public ResponseData getArticleReviewed(int articleID)
        {
            ResponseData responseData = new ResponseData();
            var mapResults = new Dictionary<string, Object>();
            try
            {
                List<ReviewArticleResponse> reviewArticleResponses = reviewArticleService.getArticleReviewed(articleID);
                mapResults.Add("items", reviewArticleResponses);

                responseData.data = mapResults;
                responseData.success = reviewArticleResponses.Count > 0;
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
