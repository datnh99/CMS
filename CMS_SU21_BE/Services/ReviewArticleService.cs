using CMS_SU21_BE.Models.Entities;
using CMS_SU21_BE.Models.Requests;
using CMS_SU21_BE.Models.Responses;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace CMS_SU21_BE.Services
{
    public interface ReviewArticleService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reviewArticleRequest"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        List<ReviewArticleResponse> searchRequestReviewArticle(ReviewArticleRequest reviewArticleRequest, int? pageSize, int? pageIndex);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reviewArticleRequest"></param>
        /// <returns></returns>
        int countRequestReviewArticle(ReviewArticleRequest reviewArticleRequest);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="addReviewArticleRequest"></param>
        /// <returns></returns>
        bool addReviewArticle(AddReviewArticleRequest addReviewArticleRequest);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="addReviewArticleRequest"></param>
        /// <returns></returns>
        bool updateReviewArticle(AddReviewArticleRequest addReviewArticleRequest);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="articleRequest"></param>
        /// <returns></returns>
        bool reviewArticleByReviewer(ArticleRequest articleRequest);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="articleID"></param>
        /// <returns></returns>
        List<ReviewArticleResponse> getArticleReviewed(int articleID);
    }
}