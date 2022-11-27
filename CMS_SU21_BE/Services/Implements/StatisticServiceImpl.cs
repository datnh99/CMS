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
    public class StatisticServiceImpl : StatisticService
    {
        private StatisticRepository statisticRepository = new StatisticRepository();
        public int add(StatisticRequest request)
        {
            return statisticRepository.add(request);
        }

        public bool deleteById(int id)
        {
            return statisticRepository.deleteById(id);
        }

        public List<StatisticResponse> search(string account, int? pageIndex, int? pageSize)
        {
            return statisticRepository.search(account, pageIndex.Value, pageSize.Value);
        }

        public bool update(StatisticRequest request)
        {
            return statisticRepository.update(request);
        }

        public List<StatisticResponse> getByAccountAndObjectID(string account, int objectID)
        {
            return statisticRepository.getByAccountAndObjectID(account, objectID);
        }


        public List<StatisticResponse> getByAccountAndObjectIDAndAction(string account, int objectID, string action)
        {
            return statisticRepository.getByAccountAndObjectIDAndAction(account, objectID, action);
        }

        public List<int> getListNearestArticlesId(string lstNotIn,int limit)
        {
            return statisticRepository.getListNearestArticlesId(lstNotIn,limit);
        }
    }
}