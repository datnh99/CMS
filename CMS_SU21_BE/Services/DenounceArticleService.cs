using CMS_SU21_BE.Models.Requests;
using CMS_SU21_BE.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_SU21_BE.Services
{
    interface DenounceArticleService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        int add(DenounceArticleRequest request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reporter"></param>
        /// <param name="articleID"></param>
        /// <returns></returns>
        DenounceArticleResponse getByReporterAndArticleID(string reporter, int articleID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="articleID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        List<DenounceArticleResponse> getByArticleId(int articleID, int pageIndex, int pageSize);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="articleID"></param>
        /// <returns></returns>
        int countByArticleId(int articleID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool deleteByArticleID(int id);
    }
}
