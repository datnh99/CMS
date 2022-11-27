using CMS_SU21_BE.Common.Base;
using CMS_SU21_BE.Common.Constant;
using CMS_SU21_BE.Common.Recommendation;
using CMS_SU21_BE.Models.Requests;
using CMS_SU21_BE.Models.Responses;
using CMS_SU21_BE.Repository;
using CMS_SU21_BE.Repository.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace CMS_SU21_BE.Services.Implements
{
    public class ArticleServiceImpl : BaseService, ArticleService
    {
        private ArticleRepository articleRepository = new ArticleRepository();

        private MasterDataService masterDataService = new MasterDataServiceImpl();

        private StatisticService statisticService = new StatisticServiceImpl();

        private DenounceArticleService denounceArticleService = new DenounceArticleServiceImpl();

        private RecommendationEngine recommendationEngine = new RecommendationEngine();

/*        private UserService userService = new UserServiceImpl();
*/        public string CheckTitle(string title)
        {
            return articleRepository.CheckTitle(title);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public int Add(ArticleRequest request)
        {
            request.author = getLoggedInUsername();
            return articleRepository.Add(request);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newID"></param>
        /// <returns></returns>
        public bool DeleteById(int newID)
        {
            MasterDataResponse masterDataResponse = masterDataService.getByTypeAndCode(MasterDataConstant.ARTICLE_STATUS_DELETED_LOWER_CODE, MasterDataConstant.ARTICLE_STATUS_TYPE);
            return articleRepository.DeleteArticleById(newID, masterDataResponse.id, getLoggedInUsername());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool updateNews(ArticleRequest request)
        {
            String username = getLoggedInUsername();
            request.modifiedBy = username;
            bool checkUpdateArticle = articleRepository.UpdateNews(request);
            if (request.updateArticleByReviewer && checkUpdateArticle)
            {
                bool checkDeleteDenounce = denounceArticleService.deleteByArticleID(request.id);
                if(!checkDeleteDenounce)
                {
                    return false;
                }
            }
            return checkUpdateArticle;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagContent"></param>
        /// <param name="newID"></param>
        /// <returns></returns>
        public bool addHashTag(List<string> tagContent, int? newID)
        {
            return articleRepository.addHashTag(tagContent,newID.Value);
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
        public List<ArticleResponse> Search(int? newID, string title, int? categoryID, int? status, int? pageSize, int? pageIndex)
        {
            return articleRepository.Search(newID,title,categoryID,status,pageSize,pageIndex);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="newID"></param>
        /// <returns></returns>
        public List<string> GetTagContentByNewID(int? newID)
        {
            return articleRepository.GetTagContentByNewID(newID);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="submissionRequest"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public List<ArticleResponse> SearchRequest(SubmissionRequest submissionRequest, int? pageSize, int? pageIndex)
        {
            List<ArticleResponse> responses = articleRepository.searchArticleToManagement(submissionRequest, pageSize, pageIndex);

            if(responses != null && responses.Count > 0)
            {
                for(int i = 0; i < responses.Count; i++)
                {
                    responses[i].index = (pageIndex.Value - 1) * pageSize.Value + i + 1;
                }
            } 
            return responses;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int totalSearchRequest()
        {
            return articleRepository.totalSearchRequest();
        }

        public int totalsSearchRequest(SubmissionRequest submissionRequest)
        {
            string roleOfUser = getLoggedInUserRole();
            if (roleOfUser.ToLower().Equals(UserRoleConstant.USER_ROLE_MODERATOR))
            {
                List<string> listCode = new List<string>();
                listCode.Add(MasterDataConstant.ARTICLE_STATUS_POSTED_LOWER_CODE);
                listCode.Add(MasterDataConstant.ARTICLE_STATUS_REJECTED_LOWER);
                List<MasterDataResponse> masterDataResponse = masterDataService.getByTypeAndListCode(MasterDataConstant.ARTICLE_STATUS_TYPE, listCode);
                List<int> statusIDs = new List<int>();
                for (int i = 0; i < masterDataResponse.Count; i++)
                {
                    statusIDs.Add(masterDataResponse[i].id);
                }
                submissionRequest.reviewer = getLoggedInUsername();
                return articleRepository.totalsArticleReviewedByAccount(submissionRequest, statusIDs);
            }
            return articleRepository.totalsSearchRequest(submissionRequest);
        }

        public int totalsSearch(int? newID, string title, int? categoryID, int? status)
        {
            return articleRepository.totalsSearch(newID, title, categoryID, status);
        }

        public List<ArticleResponse> fullTextSearchArticle(string inputSearch, int? pageSize, int? pageIndex)
        {
            return articleRepository.fullTextSearchArticle(inputSearch,  pageSize,  pageIndex);
        }

        public int totalsFullTextSearch(string inputSearch)
        {
            return articleRepository.totalsFullTextSearch(inputSearch);
        }

        public List<ArticleResponse> searchLandingArticles(SubmissionRequest submissionRequest, int? pageSize, int? pageIndex)
        {
            return articleRepository.searchLandingArticles(submissionRequest, pageSize,pageIndex);
        }

        public List<ArticleResponse> getRelatedArticlesByListId(List<int> articleIds)
        {
            return articleRepository.getRelatedArticlesByListId(articleIds);
        }

        public int totalSearchLandingArticle(SubmissionRequest submissionRequest)
        {
            return articleRepository.totalSearchLandingArticle(submissionRequest);
        }

        public ArticleResponse getArticleById(int? articleId, string status)
        {
            return articleRepository.getArticleById(articleId, status);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        public ArticleResponse getOriginalArticleById(int? articleId)
        {
            return articleRepository.getOriginalArticleById(articleId);
        }

        public List<ArticleResponse> getTopArticles(SubmissionRequest submissionRequest)
        {
            string username = getLoggedInUsername();
            string listNotIn = null;
            List<ArticleResponse> listFollowed =  articleRepository.getTopArticles(username);
            List<int> listRecommended = new List<int>();
            if (listFollowed.Count >= 8)
            {
                return listFollowed;
            }
            int limit = 8;
            if(listFollowed.Count > 0)
            {
                listNotIn = string.Join(",", listFollowed.Select(art => art.id).ToList());
                limit = 8 - listFollowed.Count;
            }
            listRecommended = statisticService.getListNearestArticlesId(listNotIn, limit);
            if (listRecommended.Count > 0)
            {
                submissionRequest.lstArticlesId = string.Join(",", listRecommended);
            }
            List<ArticleResponse> listRecommendedArticle = articleRepository.getRecommendedArticles(submissionRequest.lstArticlesId, null, limit);
            if(listRecommendedArticle.Count > 0)
            {
                listFollowed.AddRange(listRecommendedArticle);
            }
            return listFollowed;
        }
        public ArticleResponse getByIdForViewer(int articleId)
        {
            string account = getLoggedInUsername();

            // check exist view for that article
            List<StatisticResponse> statisticResponse = statisticService.getByAccountAndObjectIDAndAction(account, articleId, StatisticConstant.STATISTIC_ACTION_VIEW_ARTICLE);

            StatisticRequest request = new StatisticRequest();
            request.account = account;
            request.createdBy = account;
            request.modifiedBy = account;
            request.objectId = articleId;
            request.action = StatisticConstant.STATISTIC_ACTION_VIEW_ARTICLE;
            request.deleted = false;
            statisticService.add(request);
            if (statisticResponse == null || statisticResponse.Count == 0)
            { // Save log view article
                articleRepository.updateTotalViews(articleId);
          /*      RecommenDationData data = new RecommenDationData();
                data.time = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
                data.action = StatisticConstant.STATISTIC_ACTION_VIEW_ARTICLE;
                data.listArticleId = new List<int>();
                data.listArticleId.Add(articleId);
                data.userId = getLoggedInUserId();
                recommendationEngine.writeRecommendationData(data);*/
            }
            // Get article detail
            MasterDataResponse masterDataResponse = masterDataService.getByTypeAndCode(MasterDataConstant.ARTICLE_STATUS_POSTED_LOWER_CODE, MasterDataConstant.ARTICLE_STATUS_TYPE);
            return articleRepository.getArticleById(articleId, masterDataResponse.lowerName);
        }

        public bool hideArticleById(int articleID)
        {
            MasterDataResponse masterDataPosted = masterDataService.getByTypeAndCode(MasterDataConstant.ARTICLE_STATUS_POSTED_LOWER_CODE, MasterDataConstant.ARTICLE_STATUS_TYPE);
            ArticleResponse articleResponse = articleRepository.getArticleById(articleID, masterDataPosted.lowerName);
            if(articleResponse == null || articleResponse.status != masterDataPosted.id)
            {
                throw new ArgumentException(String.Format("Can not hide this article!"));
            }
            string account = getLoggedInUsername();
            MasterDataResponse masterDataResponse = masterDataService.getByTypeAndCode(MasterDataConstant.ARTICLE_STATUS_HIDE_LOWER_CODE, MasterDataConstant.ARTICLE_STATUS_TYPE);
            return articleRepository.UpdateStatusArticle(articleID, masterDataResponse.id, account); ;
        }

        public bool unhideArticleById(int articleID)
        {
            MasterDataResponse masterDataHiding = masterDataService.getByTypeAndCode(MasterDataConstant.ARTICLE_STATUS_HIDE_LOWER_CODE, MasterDataConstant.ARTICLE_STATUS_TYPE);
            ArticleResponse articleResponse = articleRepository.getArticleById(articleID, masterDataHiding.lowerName);
            if (articleResponse == null || articleResponse.status != masterDataHiding.id)
            {
                throw new ArgumentException(String.Format("Can not unhide this article!"));
            }
            string account = getLoggedInUsername();
            MasterDataResponse masterDataResponse = masterDataService.getByTypeAndCode(MasterDataConstant.ARTICLE_STATUS_POSTED_LOWER_CODE, MasterDataConstant.ARTICLE_STATUS_TYPE);
            return articleRepository.UpdateStatusArticle(articleID, masterDataResponse.id, account); 
        }

        public bool updateStatusEditting(int articleID, string modifiedBy)
        {
            MasterDataResponse masterDataEditting = masterDataService.getByTypeAndCode(MasterDataConstant.ARTICLE_STATUS_EDITING_LOWER_CODE, MasterDataConstant.ARTICLE_STATUS_TYPE);
            return articleRepository.UpdateStatusArticle(articleID, masterDataEditting.id, modifiedBy);
        }

        public List<ArticleResponse> getArticleByPolularTags(List<string> hashtag)
        {
            return articleRepository.getArticleByPolularTags(hashtag);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="articleID"></param>
        /// <returns></returns>
        public bool updateStatusPinArticle(int articleID, int categoryID, bool isPinned)
        {
            string account = getLoggedInUsername();
            if(!getLoggedInUserRole().ToLower().Equals(UserRoleConstant.USER_ROLE_EDITOR.ToLower()))
            {
                throw new ArgumentException(String.Format("You don't have permission to pin Article!"));
            }
            if (isPinned) { 
                articleRepository.unpinAllArticleByCategoryID(account, categoryID);
            }

            return articleRepository.updateStatusPinArticle(articleID, account, isPinned);
        }

        public List<ArticleResponse> getAllPinnedArticle()
        {
            return articleRepository.getAllPinnedArticle(MasterDataConstant.ARTICLE_STATUS_POSTED_LOWER_CODE);
        }

        public bool updateStatusArticle(int id, string status, string modifiedBy)
        {
            MasterDataResponse masterDataResponse = masterDataService.getByTypeAndCode(status, MasterDataConstant.ARTICLE_STATUS_TYPE);
            int statusID = 0;
            if (masterDataResponse != null)
            {
                statusID = masterDataResponse.id;
            }
            if( statusID <= 0)
            {
                throw new ArgumentException(String.Format("Can not update status {0} for this article!"), status);
            }
            return articleRepository.updateStatusArticle(id, modifiedBy, statusID);
        }

        public bool updateStatusArticleByStatusID(int id, int status, string modifiedBy)
        {
            return articleRepository.updateStatusArticle(id, modifiedBy, status);
        }

        public List<ArticleResponse> getNewestArticleOfCategory()
        {
            return articleRepository.getNewestArticleOfCategory();
        }

        public List<ArticleResponse> getPinnedArticleByCategory(int categoryID)
        {
            articleRepository.getAllPinnedArticle(MasterDataConstant.ARTICLE_STATUS_POSTED_LOWER_CODE);
            return articleRepository.getPinnedArticleByCategory(MasterDataConstant.ARTICLE_STATUS_POSTED_LOWER_CODE, categoryID);
        }

        public List<ArticleResponse> searchRelatedArticle(string title)
        {
            return articleRepository.searchRelatedArticle(title);
        }

        public ArticleResponse getArticleOptimizeByOldId(int articleId)
        {
            return articleRepository.getArticleOptimizeByOldId(articleId);
        }

        public List<ArticleResponse> searchArticleManagement(SubmissionRequest submissionRequest, int? pageSize, int? pageIndex)
        {
            string roleOfUser = getLoggedInUserRole();
            List<ArticleResponse> responses;
            if (roleOfUser.ToLower().Equals(UserRoleConstant.USER_ROLE_MODERATOR))
            {
                List<string> listCode = new List<string>();
                listCode.Add(MasterDataConstant.ARTICLE_STATUS_POSTED_LOWER_CODE);
                listCode.Add(MasterDataConstant.ARTICLE_STATUS_REJECTED_LOWER);
                List<MasterDataResponse> masterDataResponse = masterDataService.getByTypeAndListCode(MasterDataConstant.ARTICLE_STATUS_TYPE, listCode);
                List<int> statusIDs = new List<int>();
                for (int i = 0; i < masterDataResponse.Count; i++)
                {
                    statusIDs.Add(masterDataResponse[i].id);
                }
                submissionRequest.reviewer = getLoggedInUsername();
                responses = articleRepository.searchArticleManagementByAccountManager(submissionRequest, statusIDs, pageSize, pageIndex);
            }
            else
            {
                responses = articleRepository.searchAllArticleToManagement(submissionRequest, pageSize, pageIndex);
            }
            if (responses != null && responses.Count > 0)
            {
                for (int i = 0; i < responses.Count; i++)
                {
                    responses[i].index = (pageIndex.Value - 1) * pageSize.Value + i + 1;
                }
            }
            return responses; 
        }

        public int totalsSearchArticleManagement(SubmissionRequest submissionRequest)
        {
            string roleOfUser = getLoggedInUserRole();
            if (roleOfUser.ToLower().Equals(UserRoleConstant.USER_ROLE_MODERATOR))
            {
                List<string> listCode = new List<string>();
                listCode.Add(MasterDataConstant.ARTICLE_STATUS_POSTED_LOWER_CODE);
                listCode.Add(MasterDataConstant.ARTICLE_STATUS_REJECTED_LOWER);
                List<MasterDataResponse> masterDataResponse = masterDataService.getByTypeAndListCode(MasterDataConstant.ARTICLE_STATUS_TYPE, listCode);
                List<int> statusIDs = new List<int>();
                for (int i = 0; i < masterDataResponse.Count; i++)
                {
                    statusIDs.Add(masterDataResponse[i].id);
                }
                submissionRequest.reviewer = getLoggedInUsername();
                return articleRepository.totalsArticleManagementByAccountManager(submissionRequest, statusIDs);
            }
            return articleRepository.totalsSearchArticleManagement(submissionRequest);
        }
    }
}