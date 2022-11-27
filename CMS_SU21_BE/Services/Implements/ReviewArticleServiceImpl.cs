using CMS_SU21_BE.Common.Base;
using CMS_SU21_BE.Common.Constant;
using CMS_SU21_BE.Models.Entities;
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
    public class ReviewArticleServiceImpl : BaseService, ReviewArticleService
    {
        private MasterDataRepository masterDataRepository = new MasterDataRepository();

        private ReviewArticleRepository reviewArticleRepository = new ReviewArticleRepository();

        private ArticleService articleService = new ArticleServiceImpl();

        private ArticleVersionService articleVersionService = new ArticleVersionServiceImpl();

        public bool addReviewArticle(AddReviewArticleRequest addReviewArticleRequest)
        {
            /*ReviewArticleRequest reviewArticleRequest = new ReviewArticleRequest();
            reviewArticleRequest.account = addReviewArticleRequest.account;
            reviewArticleRequest.reviewer = addReviewArticleRequest.account;
            reviewArticleRequest.newID = addReviewArticleRequest.articleId;
            List<ReviewArticleResponse> listReviewArticleResponse = reviewArticleRepository.searchRequestReviewArticle(reviewArticleRequest, 10, 1);
            if(listReviewArticleResponse.Count > 0)
            {
                return true;
            }*/
            List<ReviewArticleResponse> reviewArticleResponses = reviewArticleRepository.getArticleReviewed(addReviewArticleRequest.articleId);
            bool existReview = false;
            if(reviewArticleResponses != null && reviewArticleResponses.Count > 0)
            {
                for (int i = 0; i < reviewArticleResponses.Count; i++)
                {
                    if (reviewArticleResponses[i].reviewer == addReviewArticleRequest.account)
                    {
                        existReview = true;
                    }
                }
                if (!existReview)
                {
                    throw new InvalidOperationException("This article has been reviewed by someone else!");
                }
            }

            ReviewArticleResponse reviewArticleResponse = reviewArticleRepository.existRequestReviewArticle(addReviewArticleRequest.articleId, addReviewArticleRequest.account);
            if(reviewArticleResponse != null && reviewArticleResponse.account == addReviewArticleRequest.account && reviewArticleResponse.newID == addReviewArticleRequest.articleId)
            {
                return true;
            }
            if (reviewArticleResponse.account == null) {
                return reviewArticleRepository.addReviewArticle(addReviewArticleRequest);
            }
            else
            {
                throw new InvalidOperationException("This article has been reviewed by someone else!");
            }
        }

        public List<ReviewArticleResponse> getArticleReviewed(int articleID)
        {
            return reviewArticleRepository.getArticleReviewed(articleID);
        }

        public int countRequestReviewArticle(ReviewArticleRequest reviewArticleRequest)
        {
            reviewArticleRequest.reviewer = getLoggedInUsername();
            List<string> listCode = new List<string>();
            listCode.Add("SUBMITTED");
            listCode.Add("REVIEWING");
            List<MasterDataResponse> masterDataResponse = masterDataRepository.getByTypeAndListCode(MasterDataConstant.ARTICLE_STATUS_TYPE, listCode);
            List<int> listCodeID = new List<int>();
            for (int i = 0; i < masterDataResponse.Count; i++)
            {
                listCodeID.Add(masterDataResponse[i].id);
            }
            return reviewArticleRepository.countRequestReviewArticle(reviewArticleRequest, listCodeID);
        }

        public bool reviewArticleByReviewer(ArticleRequest articleRequest)
        {
            string accountLogin = getLoggedInUsername();
            if (articleRequest.confirm == false)
            {
                MasterDataResponse masterDataResponse = masterDataRepository.getByTypeAndCode(MasterDataConstant.ARTICLE_STATUS_REJECTED_LOWER, MasterDataConstant.ARTICLE_STATUS_TYPE);
                articleRequest.status = masterDataResponse.id;
                bool checkUpdateArticle = articleService.updateNews(articleRequest);

                // Update note to record of this article in ReviewArticle Table
                AddReviewArticleRequest addReviewArticleRequest = new AddReviewArticleRequest();
                addReviewArticleRequest.articleId = articleRequest.id;
                addReviewArticleRequest.account = accountLogin;
                addReviewArticleRequest.confirm = articleRequest.confirm;
                addReviewArticleRequest.note = articleRequest.note;
                bool checkUpdateReviewArticle = updateReviewArticle(addReviewArticleRequest);
                /*if (!checkUpdateReviewArticle)
                {
                    return addReviewArticle(addReviewArticleRequest);
                }*/

            } else
            {
                // Get id of posted status
                MasterDataResponse masterDataResponse = masterDataRepository.getByTypeAndCode(MasterDataConstant.ARTICLE_STATUS_POSTED_LOWER, MasterDataConstant.ARTICLE_STATUS_TYPE);

                // Get article of user submit 
                ArticleResponse articleResponse = articleService.getArticleById(articleRequest.id,"");

                ArticleRequest request = new ArticleRequest();
                request.content = articleResponse.content;
                request.categoryID = articleResponse.categoryID;
                request.author = getLoggedInUsername();
                request.status = articleResponse.status;
                request.clone = true;
                request.totalViews = articleResponse.totalViews;
                request.deleted = true;
                request.sapo = articleResponse.sapo;
                request.title = articleResponse.title;
                request.introductionImage = articleResponse.introductionImage;
                request.tagContent = articleResponse.tagContent;
                request.relatedArticle = articleResponse.relatedArticle;

                // Save version of user to new record
                int articleID = articleService.Add(request);
                if (articleID <= 0)
                {
                    throw new InvalidOperationException("Review fail!");
                }

                // Get version of reviewer to update for current version
                articleRequest.status = masterDataResponse.id;
                articleRequest.approveDate = DateTime.Now;
                bool checkUpdateArticle = articleService.updateNews(articleRequest);
                if (!checkUpdateArticle)
                {
                    throw new InvalidOperationException("Review fail!");
                }

                // Add infor of new record with old record to ArticleVersion Table
                int versionID = articleVersionService.AddNew(articleRequest.id, articleID);
                if (versionID <= 0)
                {
                    throw new InvalidOperationException("Review fail!");
                }

                // Update note to record of this article in ReviewArticle Table
                AddReviewArticleRequest addReviewArticleRequest = new AddReviewArticleRequest();
                addReviewArticleRequest.articleId = articleRequest.id;
                addReviewArticleRequest.account = getLoggedInUsername();
                addReviewArticleRequest.confirm = articleRequest.confirm;
                addReviewArticleRequest.note = articleRequest.note;
                bool checkUpdateReviewArticle = updateReviewArticle(addReviewArticleRequest);
                /*if (!checkUpdateReviewArticle)
                {
                    return addReviewArticle(addReviewArticleRequest);
                }*/

            }
            return true;
        }

        public List<ReviewArticleResponse> searchRequestReviewArticle(ReviewArticleRequest reviewArticleRequest, int? pageSize, int? pageIndex)
        {
            reviewArticleRequest.reviewer = getLoggedInUsername();
            List<string> listCode = new List<string>();
            listCode.Add("SUBMITTED");
            listCode.Add("REVIEWING");
            List<MasterDataResponse> masterDataResponse = masterDataRepository.getByTypeAndListCode(MasterDataConstant.ARTICLE_STATUS_TYPE, listCode);
            List<int> listCodeID = new List<int>();
            for(int i = 0; i < masterDataResponse.Count; i++)
            {
                listCodeID.Add(masterDataResponse[i].id);
            }
            List<ReviewArticleResponse> reviewArticleResponses = reviewArticleRepository.searchRequestReviewArticle(reviewArticleRequest, listCodeID, pageSize, pageIndex);
            if (reviewArticleResponses != null && reviewArticleResponses.Count > 0)
            {
                for (int i = 0; i < reviewArticleResponses.Count; i++)
                {
                    reviewArticleResponses[i].index = (pageIndex.Value - 1) * pageSize.Value + i + 1;
                }
            }
            return reviewArticleResponses;
        }

        public bool updateReviewArticle(AddReviewArticleRequest addReviewArticleRequest)
        {
            return reviewArticleRepository.updateReviewArticle(addReviewArticleRequest);
        }
    }
}