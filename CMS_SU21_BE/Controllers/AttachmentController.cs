using CMS_SU21_BE.Common.Helper;
using CMS_SU21_BE.Models.Entities;
using CMS_SU21_BE.Models.Requests;
using CMS_SU21_BE.Models.Responses;
using CMS_SU21_BE.Services;
using CMS_SU21_BE.Services.Implements;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CMS_SU21_BE.Controllers
{
    [RoutePrefix("api/attachments")]
    public class AttachmentController : ApiController
    {
        private FileService fileService = new FileServiceImpl();

        [DisableCors]
        [HttpPost]
        [Route("uploadAttachments/{upload-attachments}")]
        public async Task<HttpResponseMessage> uploadAttachments(int articleId)
        {
            var mapResult = new Dictionary<string, Object>();
            string filePath = null;
            if (Request.Content.IsMimeMultipartContent())
            {
                try
                {
                    string root = HttpContext.Current.Server.MapPath("~/Upload/Attachments");
                    var streamProvider = new MultipartFormDataStreamProvider(root);
                    await Request.Content.ReadAsMultipartAsync(streamProvider);
                    var listFile = streamProvider.FileData;
                    int fileID = 0;
                    foreach (MultipartFileData fileData in listFile)
                    {
                        if (string.IsNullOrEmpty(fileData.Headers.ContentDisposition.FileName))
                        {
                            return Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted");
                        }
                        string originalName = fileData.Headers.ContentDisposition.FileName;
                        string fileName = DateTime.Now.ToString("yyyy’-‘MM’-‘dd’T’HH’:’mm’:’ss.fffffffK") + originalName;
                        string regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
                        Regex r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));

                        fileName = fileName.Trim('"');
                        fileName = r.Replace(fileName, "");
                        if (fileName.Contains(@"/") || fileName.Contains(@"\"))
                        {
                            fileName = Path.GetFileName(fileName);
                        }
                        filePath = Path.Combine(root, fileName);
                        File.Move(fileData.LocalFileName, filePath);
                         /*   if (!checkValidSize(filePath))
                            {
                                return Request.CreateResponse(HttpStatusCode.NotAcceptable, "Please choose another introduction image with higher quality!");
                            }*/
                        FileRequest fRequest = new FileRequest
                        {
                            createdBy = "system",
                            createdTime = DateTime.Now,
                            modifiedBy = "system",
                            modifiedTime = DateTime.Now,
                            name = fileName,
                            size = 0,
                            link = fileName,
                            articleId = articleId,
                            originalName = originalName
                        };
                        fileID = fileService.Add(fRequest);
                    }
                    var response = Request.CreateResponse(HttpStatusCode.OK, fileID);
                    return response;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    return Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted");

                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted");
            }
        }
        [DisableCors]
        [HttpGet]
        [Route("getAttachment/{get-attachment}")]
        public HttpResponseMessage getAttachment(int? id)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                HttpContext context = HttpContext.Current;
                string root = context.Server.MapPath("~/Upload/Attachments");

                //Limit access only to images folder at root level  
                FileResponse fileInfor = fileService.getById(id.Value);
                string filePath = root + "\\" + fileInfor.name;
                string extension = Path.GetExtension(filePath);

                //converting Pdf file into bytes array  
                var dataBytes = File.ReadAllBytes(filePath);
                //adding bytes to memory stream   
                var dataStream = new MemoryStream(dataBytes);

                HttpResponseMessage httpResponseMessage = Request.CreateResponse(HttpStatusCode.OK);
                httpResponseMessage.Content = new StreamContent(dataStream);
                httpResponseMessage.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                httpResponseMessage.Content.Headers.ContentDisposition.FileName = fileInfor.name;
                httpResponseMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

                return httpResponseMessage;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted");
            }
        }
        [DisableCors]
        [HttpPost]
        [Route("deleteByIds/{delete-by-ids}")]
        public ResponseData deleteByIds([FromBody] List<int> ids)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                if(ids.Count <= 0)
                {
                    responseData.success = true;
                    return responseData;
                }
                HttpContext context = HttpContext.Current;
                string root = context.Server.MapPath("~/Upload/Attachments");
                List<FileResponse> listFile = fileService.getByListIds(ids);
                if(listFile!=null || listFile.Count > 0)
                {
                    foreach(FileResponse res in listFile)
                    {
                        FileInfo file = new FileInfo(root+"\\"+res.name);
                        while (IsFileLocked(file))
                            Thread.Sleep(1000);
                        file.Delete();
                    }
                }
             
                responseData.data = fileService.deleteByIds(ids);
                responseData.success = true;
                return responseData;
            }
            catch (Exception e)
            {
                responseData.message = e.StackTrace;
                responseData.success = false;
                return responseData;
            }

        }
        protected virtual bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }

    }
   
}