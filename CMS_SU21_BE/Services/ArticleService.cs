using CMS_SU21_BE.Models.Requests;
using CMS_SU21_BE.Models.Responses;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Web;


namespace CMS_SU21_BE.Services
{
    public interface ArticleService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        string CheckTitle(string title);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        int Add(ArticleRequest request);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="newID"></param>
        /// <returns></returns>
        bool DeleteById(int newID);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        bool updateNews(ArticleRequest request);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagContent"></param>
        /// <param name="newID"></param>
        /// <returns></returns>
        bool addHashTag(List<string> tagContent, int? newID);

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
        List<ArticleResponse> Search(int? newID, string title, int? categoryID, int? status, int? pageSize, int? pageIndex);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newID"></param>
        /// <returns></returns>
        List<string> GetTagContentByNewID(int? newID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newID"></param>
        /// <param name="title"></param>
        /// <param name="categoryID"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        int totalsSearch(int? newID, string title, int? categoryID, int? status);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="submissionRequest"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        List<ArticleResponse> SearchRequest(SubmissionRequest submissionRequest, int? pageSize, int? pageIndex);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        int totalsSearchRequest(SubmissionRequest submissionRequest);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="articleId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        ArticleResponse getArticleById(int? articleId, string status);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        int totalSearchRequest();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputSearch"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        List<ArticleResponse> fullTextSearchArticle(string inputSearch, int? pageSize, int? pageIndex);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputSearch"></param>
        /// <returns></returns>        
        int totalsFullTextSearch(string inputSearch);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="submissionRequest"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        List<ArticleResponse> searchLandingArticles(SubmissionRequest submissionRequest, int? pageSize, int? pageIndex);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="submissionRequest"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        List<ArticleResponse> searchArticleManagement(SubmissionRequest submissionRequest, int? pageSize, int? pageIndex);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="submissionRequest"></param>
        /// <returns></returns>
        int totalsSearchArticleManagement(SubmissionRequest submissionRequest);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="articleIds"></param>
        /// <returns></returns>
        List<ArticleResponse> getRelatedArticlesByListId(List<int> articleIds);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="submissionRequest"></param>
        /// <returns></returns>
        int totalSearchLandingArticle(SubmissionRequest submissionRequest);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        ArticleResponse getOriginalArticleById(int? articleId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="submissionRequest"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        List<ArticleResponse> getTopArticles(SubmissionRequest submissionRequest);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        ArticleResponse getByIdForViewer(int articleId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="articleID"></param>
        /// <returns></returns>
        bool hideArticleById(int articleID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="articleID"></param>
        /// <returns></returns>
        bool unhideArticleById(int articleID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="articleID"></param>
        /// <returns></returns>
        bool updateStatusEditting(int articleID, string modifiedBy);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hashtag"></param>
        /// <returns></returns>
        List<ArticleResponse> getArticleByPolularTags(List<string> hashtag);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="articleID"></param>
        /// <returns></returns>
        bool updateStatusPinArticle(int articleID, int categoryID, bool isPinned);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<ArticleResponse> getAllPinnedArticle();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        bool updateStatusArticle(int value, string status, string modifiedBy);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        bool updateStatusArticleByStatusID(int value, int status, string modifiedBy);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<ArticleResponse> getNewestArticleOfCategory();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        List<ArticleResponse> getPinnedArticleByCategory(int categoryID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        List<ArticleResponse> searchRelatedArticle(string title);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        ArticleResponse getArticleOptimizeByOldId(int articleId);
    }
}