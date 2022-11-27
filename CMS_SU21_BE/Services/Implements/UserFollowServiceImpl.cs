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
    public class UserFollowServiceImpl : BaseService, UserFollowService
    {
        private UserFollowRepository userFollowRepositoryImpl = new UserFollowRepository();

        private RecommendationEngine recommendationEngine = new RecommendationEngine();

        public int add(UserFollowRequest request)
        {
            String username = getLoggedInUsername();
            request.createdBy = username;
            request.modifiedBy = username;
            request.follower = username;
         /*   List<int> listId = userFollowRepositoryImpl.getAllArticleByAccount(request.account);
            if (listId != null)
            {
                RecommenDationData data = new RecommenDationData();
                data.time = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
                data.action = StatisticConstant.STATISTIC_ACTION_FOLLOW_USER;
                data.userId = getLoggedInUserId();
                data.listArticleId = listId;
                recommendationEngine.writeRecommendationData(data);
            }*/
            return userFollowRepositoryImpl.add(request);
        }

        //unfollow user
        public bool deleteByAccount(string account)
        {
            String username = getLoggedInUsername();
            return userFollowRepositoryImpl.deleteByAccount(account, username);
        }


        public Boolean getFollowedUser(string account)
        {
            String username = getLoggedInUsername();
            return userFollowRepositoryImpl.getFollowedUser(account, username);
        }

        public bool update(UserFollowRequest request)
        {
            String username = getLoggedInUsername();
            request.createdBy = username;
            request.modifiedBy = username;
            return userFollowRepositoryImpl.update(request);
        }
    }
}