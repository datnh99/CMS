using CMS_SU21_BE.Models.Requests;
using CMS_SU21_BE.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_SU21_BE.Services
{
    public interface StatisticService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        List<StatisticResponse> search(string account, int? pageIndex, int? pageSize);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        int add(StatisticRequest request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        bool update(StatisticRequest request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool deleteById(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        /// <param name="objectID"></param>
        /// <returns></returns>
        List<StatisticResponse> getByAccountAndObjectID(string account, int objectID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        /// <param name="objectID"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        List<StatisticResponse> getByAccountAndObjectIDAndAction(string account, int objectID, string action);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstNotIn"></param>
        /// <returns></returns>
        List<int> getListNearestArticlesId(string lstNotIn,int limit);
    }
}