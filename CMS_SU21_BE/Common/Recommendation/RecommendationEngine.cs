using CMS_SU21_BE.Common.Base;
using CMS_SU21_BE.Common.Constant;
using CMS_SU21_BE.Models.Responses;
using CMS_SU21_BE.Services;
using CMS_SU21_BE.Services.Implements;
using NReco.CF.Taste.Impl.Model.File;
using NReco.CF.Taste.Impl.Neighborhood;
using NReco.CF.Taste.Impl.Recommender;
using NReco.CF.Taste.Impl.Similarity;
using NReco.CF.Taste.Recommender;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace CMS_SU21_BE.Common.Recommendation
{
    public class RecommendationEngine :BaseService
    {
        public List<int> getRecommendation()
        {
            try
            {
                HttpContext context = HttpContext.Current;
                string root = context.Server.MapPath("~/Common/Recommendation/ratings.dat");
                var model = new FileDataModel(root);
                var similarity = new LogLikelihoodSimilarity(model);
                var neighborhood = new NearestNUserNeighborhood(3, similarity, model);
                var recommender = new GenericUserBasedRecommender(model, neighborhood, similarity);
                var recommendedItems = recommender.Recommend(getLoggedInUserId(), 8);
                return recommendedItems.Select(ri => Convert.ToInt32(ri.GetItemID())).ToList();
            }catch(Exception e)
            {
                return null;
            }
        
        }
        public void writeRecommendationData(RecommenDationData data)
        {
            var csv = new StringBuilder();
            HttpContext context = HttpContext.Current;
            string root = context.Server.MapPath("~/Common/Recommendation/ratings.dat");
            //in your loop
            var score = 1;
            if(data.action == StatisticConstant.STATISTIC_ACTION_FOLLOW_USER)
            {
                score = 3;
            }
            //Suggestion made by KyleMit
            foreach(int item in data.listArticleId)
            {
                var newLine = string.Format("{0}\t{1}\t{2}\t{3}", data.userId, item, score, data.time);
                csv.AppendLine(newLine);
            }
            //after your loop
            File.AppendAllText(root, csv.ToString());
        }
    }
}