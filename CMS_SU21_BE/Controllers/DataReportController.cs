using CMS_SU21_BE.Common.Helper;
using CMS_SU21_BE.Models.Responses;
using CMS_SU21_BE.Repository.Implements;
using CMS_SU21_BE.Services;
using CMS_SU21_BE.Services.Implements;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CMS_SU21_BE.Controllers
{
    [RoutePrefix("api/data-report")]
    public class DataReportController : ApiController
    {
        private DataReportService dataReportService = new DataReportSercviceImpl();
        private ExcelService excelService = new ExcelServiceImpl();

        [DisableCors]
        [HttpPost]
        [Route("dataReportCategory/{data-report-category}")]
        public ResponseData dataReportCategory([FromBody] DateFormSearch form)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                List<CategoryReportResponse> listResult = dataReportService.dataReportCategory(form.fromDate, form.toDate);
                responseData.success = true;
                responseData.data = listResult;
                return responseData;
            }
            catch (Exception e)
            {
                responseData.success = false;
                responseData.message = e.StackTrace;
                return responseData;
            }
        }


        [DisableCors]
        [HttpPost]
        [Route("getTopArticles/{get-top-articles}")]
        public ResponseData getTopArticles([FromBody] DateFormSearch form)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                List<ArticleResponse> listResult = dataReportService.getTopArticles(form.fromDate, form.toDate, false);
                responseData.success = true;
                responseData.data = listResult;
                return responseData;
            }
            catch (Exception e)
            {
                responseData.success = false;
                responseData.message = e.StackTrace;
                return responseData;
            }
        }
        [DisableCors]
        [HttpPost]
        [Route("dataTimelineReport/{data-timeline-report}")]
        public ResponseData dataTimelineReport([FromBody] DateFormSearch form, string viewType)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                List<TimelineResponse> listResult = dataReportService.dataTimelineReport(form.fromDate, form.toDate, viewType);
                responseData.success = true;
                responseData.data = listResult;
                return responseData;
            }
            catch (Exception e)
            {
                responseData.success = false;
                responseData.message = e.StackTrace;
                return responseData;
            }
        }

        [DisableCors]
        [HttpPost]
        [Route("getTopWriter/{get-top-writer}")]
        public ResponseData getTopWriter([FromBody] DateFormSearch form)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                List<TopWriterResponse> listResult = dataReportService.getTopWriter(form.fromDate, form.toDate);
                responseData.success = true;
                responseData.data = listResult;
                return responseData;
            }
            catch (Exception e)
            {
                responseData.success = false;
                responseData.message = e.StackTrace;
                return responseData;
            }
        }

        [DisableCors]
        [HttpPost]
        [Route("exportData/{export-data}")]
        public HttpResponseMessage exportData([FromBody] DateFormSearch form)
        {
            List<ArticleResponse> listTopArticle = dataReportService.getTopArticles(form.fromDate, form.toDate, true);
            List<CategoryReportResponse> listCategory = dataReportService.dataReportCategory(form.fromDate, form.toDate);
            List<ExportArticleResponse> listExportArticle = listTopArticle.Select(art =>
            {
                var export = new ExportArticleResponse
                {
                    id = art.id,
                    title = art.title,
                    category = art.categoryName,
                    author = art.author,
                    totalViews = art.totalViews,
                    totalComment = art.totalComment,
                    createdDate = art.createdTime,
                    approveDate = art.approveDate
                };
                return export;
            }).ToList();
            IWorkbook _workbook = new XSSFWorkbook(); //Creating New Excel object
            ISheet _sheetArticle = _workbook.CreateSheet("Articles");
            ISheet _sheetCategory = _workbook.CreateSheet("Category"); //Creating New Excel Sheet object
                                                                      //Creating New Excel Sheet object
            List<string> _headersArticle = new List<string>();
            List<string> _headersCategory = new List<string>();

            var headerStyle = _workbook.CreateCellStyle(); //Formatting
            var headerFont = _workbook.CreateFont();
            headerFont.IsBold = true;
            headerStyle.SetFont(headerFont);
            headerStyle.FillForegroundColor = IndexedColors.Orange.Index;
            excelService.WriteData(listExportArticle, _sheetArticle, _headersArticle); //your list object to NPOI excel conversion happens here

            //Header
            var headerArticle = _sheetArticle.CreateRow(0);
            for (var i = 0; i < _headersArticle.Count; i++)
            {
                var cell = headerArticle.CreateCell(i);
                cell.SetCellValue(_headersArticle[i]);
                cell.CellStyle = headerStyle;
                // It's heavy, it slows down your Excel if you have large data                
                //_sheet.AutoSizeColumn(i);
            }
            excelService.WriteData(listCategory, _sheetCategory, _headersCategory); //your list object to NPOI excel conversion happens here
            var headerCategory = _sheetCategory.CreateRow(0);
            for (var i = 0; i < _headersCategory.Count; i++)
            {
                var cell = headerCategory.CreateCell(i);
                cell.SetCellValue(_headersCategory[i]);
                cell.CellStyle = headerStyle;
                // It's heavy, it slows down your Excel if you have large data                
                //_sheet.AutoSizeColumn(i);
            }
            using (var memoryStream = new MemoryStream()) //creating memoryStream
            {
                _workbook.Write(memoryStream);
                var response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(memoryStream.ToArray())
                };

                response.Content.Headers.ContentType = new MediaTypeHeaderValue
                       ("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                response.Content.Headers.ContentDisposition =
                       new ContentDispositionHeaderValue("attachment")
                       {
                           FileName = $"{"ExportData"}_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx"
                       };

                return response;
            }
        }
    }
}