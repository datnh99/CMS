using CMS_SU21_BE.Common.Helper;
using CMS_SU21_BE.Common.Constant;
using CMS_SU21_BE.Models.Requests;
using CMS_SU21_BE.Models.Responses;
using CMS_SU21_BE.Services;
using CMS_SU21_BE.Services.Implements;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using CMS_SU21_BE.Common.Recommendation;

namespace CMS_SU21_BE.Controllers
{
    [RoutePrefix("api/article")]
    public class ArticleController : ApiController
    {
        private ArticleService articleService = new ArticleServiceImpl();

        MasterDataService masterDataService = new MasterDataServiceImpl();
        private FileService fileService = new FileServiceImpl();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        [DisableCors]
        [HttpGet]
        [Route("checkTitle/{check-title}")]
        public ResponseData checkTitle(string title)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                responseData.data = articleService.CheckTitle(title);
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
        /// <param name="newID"></param>
        /// <param name="title"></param>
        /// <param name="categoryID"></param>
        /// <param name="status"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [DisableCors]
        [HttpGet]
        [Route("search/{search}")]
        public ResponseData search(int? newID, string title, int? categoryID, int? status, int? pageSize, int? pageIndex)
        {
            ResponseData responseData = new ResponseData();
            var mapResult = new Dictionary<string, Object>();
            try
            {
                List<ArticleResponse> listResult = articleService.Search(newID, title, categoryID, status, pageSize.Value, pageIndex.Value);
                int total = articleService.totalsSearch(newID, title, categoryID, status);
                mapResult.Add("items", listResult);
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
        [HttpPost]
        [Route("searchRequest/{search-request}")]
        public ResponseData searchRequest(SubmissionRequest submissionRequest, int? pageSize, int? pageIndex)
        {
            ResponseData responseData = new ResponseData();
            var mapResult = new Dictionary<string, Object>();
            try
            {
                List<ArticleResponse> listResult = articleService.SearchRequest(submissionRequest, pageSize.Value, pageIndex.Value);
                int total = articleService.totalsSearchRequest(submissionRequest);
                mapResult.Add("items", listResult);
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
        [HttpPost]
        [Route("searchArticleManagement/{search-article-management}")]
        public ResponseData searchArticleManagement(SubmissionRequest submissionRequest, int? pageSize, int? pageIndex)
        {
            ResponseData responseData = new ResponseData();
            var mapResult = new Dictionary<string, Object>();
            try
            {
                List<ArticleResponse> listResult = articleService.searchArticleManagement(submissionRequest, pageSize.Value, pageIndex.Value);
                int total = articleService.totalsSearchArticleManagement(submissionRequest);
                mapResult.Add("items", listResult);
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
        [HttpPost]
        [Route("getTopArticles/{get-top-articles}")]
        public ResponseData getTopArticles(SubmissionRequest submissionRequest)
        {
            ResponseData responseData = new ResponseData();
            var mapResult = new Dictionary<string, Object>();
            try
            {

                List<ArticleResponse> listResult = articleService.getTopArticles(submissionRequest);
                mapResult.Add("items", listResult);
                responseData.success = true;
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
        [HttpPost]
        [Route("searchLandingArticles/{search-landing-article}")]
        public ResponseData searchLandingArticles(SubmissionRequest submissionRequest, int? pageSize, int? pageIndex)
        {
            ResponseData responseData = new ResponseData();
            var mapResult = new Dictionary<string, Object>();
            try
            {
                List<ArticleResponse> listResult = articleService.searchLandingArticles(submissionRequest, pageSize.Value, pageIndex.Value);
                int total = articleService.totalSearchLandingArticle(submissionRequest);
                mapResult.Add("items", listResult);
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
        [Route("add/{add}")]
        public ResponseData add([FromBody] ArticleRequest request, Boolean isDraft)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                String existedArticle = articleService.CheckTitle(request.title);
                if (!string.IsNullOrEmpty(existedArticle))
                {
                    throw new Exception("Your article's title already existed !");
                }
                MasterDataResponse msData = new MasterDataResponse();
                String msCode = MasterDataConstant.ARTICLE_STATUS_SUBMITTED;
                if (isDraft)
                {
                    msCode = MasterDataConstant.ARTICLE_STATUS_DRAFT;
                }
                msData = masterDataService.getByTypeAndCode(msCode, MasterDataConstant.ARTICLE_STATUS_TYPE);
                request.status = msData.id;
                responseData.success = true;

                responseData.data = articleService.Add(request);
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
        /// <param name="newID"></param>
        /// <returns></returns>
        [DisableCors]
        [HttpPost]
        [Route("deleteById/{delete-id}")]
        public ResponseData deleteById(int newID)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                responseData.data = articleService.DeleteById(newID);
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
        [Route("updateArticle/{update-article}")]
        public ResponseData updateArticle([FromBody] ArticleRequest request, Boolean isDraft)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                MasterDataResponse msData = new MasterDataResponse();
                String msCode = MasterDataConstant.ARTICLE_STATUS_SUBMITTED;
                if (isDraft)
                {
                    msCode = MasterDataConstant.ARTICLE_STATUS_DRAFT;
                }
                msData = masterDataService.getByTypeAndCode(msCode, MasterDataConstant.ARTICLE_STATUS_TYPE);
                request.status = msData.id;
                responseData.success = true;
                responseData.data = articleService.updateNews(request);
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
        [Route("updateArticleByReviewer/{update-article-by-reviewer}")]
        public ResponseData updateArticleByReviewer([FromBody] ArticleRequest request)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                responseData.success = true;
                request.updateArticleByReviewer = true;
                responseData.data = articleService.updateNews(request);
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
        /// <param name="newID"></param>
        /// <returns></returns>
        [DisableCors]
        [HttpGet]
        [Route("getArticleById/{get-article-by-id}")]
        public ResponseData getArticleById(int? articleId, string status)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                ArticleResponse articleResponse = articleService.getArticleById(articleId.Value, status);
                responseData.success = true;
                if (articleResponse == null)
                {
                    responseData.success = false;
                }
                articleResponse.attachments = fileService.getByArticleId(articleResponse.id);
                responseData.data = articleResponse;
                return responseData
;
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
        [Route("getOriginalArticleById/{get-original-article-by-id}")]
        public ResponseData getOriginalArticleById(int? articleId)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                ArticleResponse articleResponse = articleService.getOriginalArticleById(articleId.Value);
                responseData.success = true;
                if (articleResponse == null)
                {
                    responseData.success = false;
                }
                responseData.data = articleResponse;
                return responseData
;
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
        [Route("getArticleOptimizeByOldId/{get-article-optimize-by-old-id}")]
        public ResponseData getArticleOptimizeByOldId(int? articleId)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                ArticleResponse articleResponse = articleService.getArticleOptimizeByOldId(articleId.Value);
                responseData.success = true;
                if (articleResponse == null)
                {
                    responseData.success = false;
                }
                responseData.data = articleResponse;
                return responseData
;
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
        [Route("totalSearchRequest/{total-search-request}")]
        public ResponseData totalSearchRequest()
        {
            ResponseData responseData = new ResponseData();
            try
            {
                responseData.data = articleService.totalSearchRequest();
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
        [Route("fullTextSearch/{full-text-search}")]
        public ResponseData fullTextSearch(string inputSearch, int? pageSize, int? pageIndex)
        {
            ResponseData responseData = new ResponseData();
            var mapResult = new Dictionary<string, Object>();
            try
            {
                List<ArticleResponse> listResult = articleService.fullTextSearchArticle(inputSearch, pageSize.Value, pageIndex.Value);
                int total = articleService.totalsFullTextSearch(inputSearch);
                mapResult.Add("items", listResult);
                mapResult.Add("totals", total);
                responseData.success = true;
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
        [HttpPost]
        [Route("getRelatedArticlesByListId/{get-related-article-by-list-id}")]
        public ResponseData getRelatedArticlesByListId([FromBody] List<int> ids)
        {
            ResponseData responseData = new ResponseData();
            var mapResult = new Dictionary<string, Object>();
            try
            {
                List<ArticleResponse> listResult = articleService.getRelatedArticlesByListId(ids);
                responseData.success = true;
                responseData.data = listResult;
                return responseData;
            }
            catch (Exception e)
            {
                responseData.success = false;
                responseData.message = e.ToString();
                return responseData;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        [DisableCors]
        [HttpGet]
        [Route("getByIdForViewer/{get-by-id-for-viewer}")]
        public ResponseData getByIdForViewer(int? articleId)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                ArticleResponse articleResponse = articleService.getByIdForViewer(articleId.Value);
                responseData.success = true;
                if (articleResponse == null)
                {
                    responseData.success = false;
                }
                articleResponse.attachments = fileService.getByArticleId(articleResponse.id);
                responseData.data = articleResponse;
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
        [Route("hideArticleById/{hide-article-by-id}")]
        public ResponseData hideArticleById(int articleID)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                bool update = articleService.hideArticleById(articleID);
                responseData.success = update;
                responseData.data = update;
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
        [Route("unhideArticleById/{unhide-article-by-id}")]
        public ResponseData unhideArticleById(int articleID)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                bool update = articleService.unhideArticleById(articleID);
                responseData.success = update;
                responseData.data = update;
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
        [Route("updateStatusEditting/{update-status-editting}")]
        public ResponseData updateStatusEditting(int articleID, string modifiedBy)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                bool update = articleService.updateStatusEditting(articleID, modifiedBy);
                responseData.success = update;
                responseData.data = update;
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
        [Route("updateStatusPinArticle/{update-status-pin-article-by-id}")]
        public ResponseData updateStatusPinArticle(int articleID, int categoryID, bool isPinned)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                bool pinned = articleService.updateStatusPinArticle(articleID, categoryID, isPinned);
                responseData.success = pinned;
                responseData.data = pinned;
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
        [Route("getPinnedArticle/{get-pinned-article}")]
        public ResponseData getPinnedArticle()
        {
            ResponseData responseData = new ResponseData();
            var mapResult = new Dictionary<string, Object>();
            try
            {
                List<ArticleResponse> listResult = articleService.getAllPinnedArticle();
                mapResult.Add("items", listResult);
                responseData.success = true;
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
        [HttpPost]
        [Route("getArticleByPolularTags/{get-article-by-popular-tags}")]
        public ResponseData getArticleByPolularTags([FromBody] List<string> hashtag)
        {
            ResponseData responseData = new ResponseData();
            var mapResult = new Dictionary<string, Object>();
            try
            {
                List<ArticleResponse> listResult = articleService.getArticleByPolularTags(hashtag);
                responseData.success = true;
                responseData.data = listResult;
                return responseData;
            }
            catch (Exception e)
            {
                responseData.message = e.StackTrace;
                return responseData;
            }

        }
        [DisableCors]
        [HttpGet]
        [Route("getNewestArticleOfCategory/{get-newest-article-of-category}")]
        public ResponseData getNewestArticleOfCategory()
        {
            ResponseData responseData = new ResponseData();
            try
            {
                List<ArticleResponse> listResult = articleService.getNewestArticleOfCategory();
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
        [Route("updateStatusArticle/{update-status-article}")]
        public ResponseData updateStatusArticle(int articleId, string status, string modifiedBy)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                bool checkUpdateArticle = articleService.updateStatusArticle(articleId, status, modifiedBy);
                responseData.success = checkUpdateArticle;
                responseData.data = checkUpdateArticle;
                return responseData
;
            }
            catch (Exception e)
            {
                responseData.success = false;
                responseData.message = e.ToString();
                return responseData;

            }
        }

        [DisableCors]
        [HttpGet]
        [Route("updateStatusArticleByStatusID/{update-status-article-by-status-id}")]
        public ResponseData updateStatusArticleByStatusID(int articleId, int status, string modifiedBy)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                bool checkUpdateArticle = articleService.updateStatusArticleByStatusID(articleId, status, modifiedBy);
                responseData.success = checkUpdateArticle;
                responseData.data = checkUpdateArticle;
                return responseData
;
            }
            catch (Exception e)
            {
                responseData.success = false;
                responseData.message = e.ToString();
                return responseData;

            }
        }

        [DisableCors]
        [HttpGet]
        [Route("getPinnedArticleByCategory/{get-pinned-article-by-category}")]
        public ResponseData getPinnedArticleByCategory(int categoryID)
        {
            ResponseData responseData = new ResponseData();
            var mapResult = new Dictionary<string, Object>();
            try
            {
                List<ArticleResponse> listResult = articleService.getPinnedArticleByCategory(categoryID);
                mapResult.Add("items", listResult);
                responseData.success = true;
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
        [Route("searchRelatedArticle/{search-related-article}")]
        public ResponseData searchRelatedArticle(string title)
        {
            ResponseData responseData = new ResponseData();
            var mapResult = new Dictionary<string, Object>();
            try
            {
                List<ArticleResponse> listResult = articleService.searchRelatedArticle(title);
                mapResult.Add("items", listResult);
                responseData.success = true;
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
