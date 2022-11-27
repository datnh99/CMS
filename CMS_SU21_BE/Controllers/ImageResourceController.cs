using CMS_SU21_BE.Models.Entities;
using CMS_SU21_BE.Services;
using CMS_SU21_BE.Services.Implements;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CMS_SU21_BE.Controllers
{
    [RoutePrefix("api/image")]
    public class ImageResourceController : ApiController
    {
        FileService fileService = new FileServiceImpl();


        [DisableCors]
        [HttpPost]
        [Route("uploadImageCkEditor/{upload-ck-editor}")]
        public async Task<HttpResponseMessage> uploadImageCkEditor()
        {
            var mapResult = new Dictionary<string, Object>();

            if (Request.Content.IsMimeMultipartContent())
            {
                try
                {
                    string root = HttpContext.Current.Server.MapPath("~/Upload/Image");

                    var streamProvider = new MultipartFormDataStreamProvider(root);
                    await Request.Content.ReadAsMultipartAsync(streamProvider);
                    var listFile = streamProvider.FileData;
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
                        var filePath = Path.Combine(root, fileName);
                        File.Move(fileData.LocalFileName, filePath);
                        FileRequest fRequest = new FileRequest
                        {
                            createdBy = "system",
                            createdTime = DateTime.Now,
                            modifiedBy = "system",
                            modifiedTime = DateTime.Now,
                            name = fileName,
                            size = 0,
                            link = filePath,
                            articleId = 0,
                            originalName = originalName
                        };
                        int fileID = fileService.Add(fRequest);

                        mapResult.Add("url", "https://localhost:44345/api/image/getImage/{get-image}?id=" + fileID);
                        
/*                        mapResult.Add("url", "data:image/png;base64," + getImageToBase64(filePath));
*/
                    }
                    var response = Request.CreateResponse(HttpStatusCode.OK, mapResult);
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
        [HttpPost]
        [Route("uploadImage/{upload-image}")]
        public async Task<HttpResponseMessage> uploadImage(Boolean introductionImage)
        {
            var mapResult = new Dictionary<string, Object>();
            string filePath = null;
            if (Request.Content.IsMimeMultipartContent())
            {
                try
                {
                    string root = HttpContext.Current.Server.MapPath("~/Upload/Image");
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
                        if (introductionImage)
                        {
                            if (!checkValidSize(filePath))
                            {
                                return Request.CreateResponse(HttpStatusCode.NotAcceptable, "Please choose another introduction image with higher quality!");
                            }
                        }
                        FileRequest fRequest = new FileRequest
                        {
                            createdBy = "system",
                            createdTime = DateTime.Now,
                            modifiedBy = "system",
                            modifiedTime = DateTime.Now,
                            name = fileName,
                            size = 0,
                            link = fileName,
                            articleId = 0,
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
         [Route("getImage/{get-image}")]
         public HttpResponseMessage getImage(int? id)
         {
             try
             {
                MemoryStream ms = new MemoryStream();
                HttpContext context = HttpContext.Current;
                string root = context.Server.MapPath("~/Upload/Image");

                //Limit access only to images folder at root level  
                FileResponse fileInfor =  fileService.getById(id.Value);
                string filePath = root +"\\" + fileInfor.name;
                string extension = Path.GetExtension(filePath);
                if (File.Exists(filePath))
                {
                    if (!string.IsNullOrWhiteSpace(extension))
                    {
                        extension = extension.Substring(extension.IndexOf(".") + 1);
                    }
                    ImageFormat format = GetImageFormat(extension);
                    //If invalid image file is requested the following line wil throw an exception  
                    new Bitmap(filePath).Save(ms, format != null ? format as ImageFormat : ImageFormat.Bmp);
                }
                else
                {
                    new Bitmap(context.Server.MapPath("/content/images/fallback.png")).Save(ms, ImageFormat.Png);
                }

                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                result.Content = new ByteArrayContent(ms.ToArray());
                result.Content.Headers.ContentType = new MediaTypeHeaderValue(string.Format("image/{0}", Path.GetExtension(filePath)));
                return result;

            }
             catch (Exception e)
             {
                 Console.WriteLine(e.StackTrace);
                 return Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted");
             }
         }
        [DisableCors]
        [HttpGet]
        [Route("getImageThumb/{get-image-thumb}")]
        public HttpResponseMessage getImageThumb(int? id)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                HttpContext context = HttpContext.Current;
                string root = context.Server.MapPath("~/Upload/Image");
                //Limit access only to images folder at root level  
                FileResponse fileInfor = fileService.getById(id.Value);
                string filePath = root + "\\" + fileInfor.name;
                Image image = Image.FromFile(filePath);
                //Image thumb = image.GetThumbnailImage(600, 400, () => false, IntPtr.Zero);

                Image thumb = ScaleImage(image, 300);
                string extension = Path.GetExtension(filePath);

                if (File.Exists(filePath))
                {
                    if (!string.IsNullOrWhiteSpace(extension))
                    {
                        extension = extension.Substring(extension.IndexOf(".") + 1);
                    }
                    ImageFormat format = GetImageFormat(extension);
                    //If invalid image file is requested the following line wil throw an exception  
                    new Bitmap(thumb).Save(ms, format != null ? format as ImageFormat : ImageFormat.Bmp);
                }
                else
                {
                    new Bitmap(context.Server.MapPath("/content/images/fallback.png")).Save(ms, ImageFormat.Png);
                }

                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
               
                result.Content = new ByteArrayContent(ms.ToArray());
                result.Content.Headers.ContentType = new MediaTypeHeaderValue(string.Format("image/{0}", Path.GetExtension(filePath)));
                return result;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted");
            }
        }
        public static Image ScaleImage(Image image, int height)
        {
            double ratio = (double)height / image.Height;
            int newWidth = (int)(image.Width * ratio);
            int newHeight = (int)(image.Height * ratio);
            Bitmap newImage = new Bitmap(newWidth, newHeight);
            using (Graphics g = Graphics.FromImage(newImage))
            {
                g.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            image.Dispose();
            return newImage;
        }

        public Boolean checkValidSize(String filePath)
        {
            using (Image image = Image.FromFile(filePath))
            {
              if(image.Height < 500 || image.Width < 1000)
                {
                    return false;
                }
            }
            return true;
          
        }
     /*   private static int ScaleWidth(int originalHeight, int newHeight, int originalWidth)
        {
            var scale = Convert.ToDouble(newHeight) / Convert.ToDouble(originalHeight);

            return Convert.ToInt32(originalWidth * scale);
        }*/
        public String getImageToBase64(String filePath)
        {
            string base64String = "";
            using (Image image = Image.FromFile(filePath))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();

                    // Convert byte[] to Base64 String
                    base64String = Convert.ToBase64String(imageBytes);
                }
            }
            return base64String;
        }
        public static ImageFormat GetImageFormat(string extension)
        {
            ImageFormat result = null;
            PropertyInfo prop = typeof(ImageFormat).GetProperties().Where(p => p.Name.Equals(extension, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            if (prop != null)
            {
                result = prop.GetValue(prop) as ImageFormat;
            }
            return result;
        }
    }
}