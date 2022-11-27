using CMS_SU21_BE.Common.Base;
using CMS_SU21_BE.Models.Responses;
using CMS_SU21_BE.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_SU21_BE.Repository.Implements
{
    public class DataReportSercviceImpl : BaseService, DataReportService
    {
        private DataReportRepository dataReportRepository = new DataReportRepository();

        public List<CategoryReportResponse> dataReportCategory(DateTime? fromDate, DateTime? toDate)
        {
            return dataReportRepository.dataReportCategory(fromDate, toDate);
        }

        public List<TimelineResponse> dataTimelineReport(DateTime? fromDate, DateTime? toDate, string viewType)
        {
            return dataReportRepository.dataTimelineReport(fromDate, toDate, viewType);
        }

        public List<ArticleResponse> getTopArticles(DateTime fromDate, DateTime toDate,Boolean export)
        {
            return dataReportRepository.getTopArticles(fromDate, toDate, export);
        }

        public List<TopWriterResponse> getTopWriter(DateTime? fromDate, DateTime? toDate)
        {
            return dataReportRepository.getTopWriter(fromDate, toDate);
        }
    }
}