using CMS_SU21_BE.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_SU21_BE.Services
{
    interface DataReportService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        List<CategoryReportResponse> dataReportCategory(DateTime? fromDate, DateTime? toDate);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        List<ArticleResponse> getTopArticles(DateTime fromDate, DateTime toDate,Boolean export);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="viewType"></param>
        /// <returns></returns>
        List<TimelineResponse> dataTimelineReport(DateTime? fromDate, DateTime? toDate, string viewType);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        List<TopWriterResponse> getTopWriter(DateTime? fromDate, DateTime? toDate);
    }
}
