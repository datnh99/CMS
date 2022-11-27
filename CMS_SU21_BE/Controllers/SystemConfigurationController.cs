using CMS_SU21_BE.Common.Helper;
using CMS_SU21_BE.Services;
using CMS_SU21_BE.Services.Implements;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CMS_SU21_BE.Controllers
{
    [RoutePrefix("api/system-config")]
    public class SystemConfigurationController : ApiController
    {
        private BlacklistWordsService blacklistWordsService = new BlacklistWordsServiceImpl();

        [DisableCors]
        [HttpPost]
        [Route("setEmailConfig/{set-email-config}")]
        public ResponseData setEmailConfig(string userInfor)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                var decodeUserInfor = System.Text.Encoding.UTF8.GetString(
                    Convert.FromBase64String(userInfor));
                var arrUserNameandPassword = decodeUserInfor.Split(':');
                var config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
                config.AppSettings.Settings.Remove("mailFrom");
                config.AppSettings.Settings.Add("mailFrom", arrUserNameandPassword[0]);
                config.AppSettings.Settings.Remove("password");
                config.AppSettings.Settings.Add("password", arrUserNameandPassword[1]);
                config.Save();
                responseData.success = true;
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
        [HttpGet]
        [Route("getEmailConfig/{get-email-config}")]
        public ResponseData getEmailConfig()
        {
            ResponseData responseData = new ResponseData();
            try
            {

                string mailFrom = ConfigurationManager.AppSettings["mailFrom"];
                responseData.data = mailFrom;
                responseData.success = true;
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
        [Route("addBlacklistWords/{add-blacklist-words}")]
        public ResponseData addBlacklistWords([FromBody] List<string> content)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                blacklistWordsService.add(content);
                responseData.success = true;
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
        [HttpGet]
        [Route("getAllBlacklistWords/{get-all-blacklist-words}")]
        public ResponseData getAllBlacklistWords()
        {
            ResponseData responseData = new ResponseData();
            try
            {
                responseData.data = blacklistWordsService.getAllBlacklistWords();
                responseData.success = true;
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
        [HttpGet]
        [Route("searchBlacklistWords/{search-blacklist-words}")]
        public ResponseData searchBlacklistWords(string content, int? pageSize, int? pageIndex)
        {
            ResponseData responseData = new ResponseData();
            Dictionary<string, Object> mapResult = new Dictionary<string, Object>();
            try
            {
                mapResult.Add("items", blacklistWordsService.searchBlacklistWords(content, pageSize, pageIndex));
                mapResult.Add("totals", blacklistWordsService.countBlacklistWords(content));
                responseData.data = mapResult;
                responseData.success = true;
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
        [Route("delete/{delete}")]
        public ResponseData delete(int id)
        {
            ResponseData responseData = new ResponseData();
            Dictionary<string, Object> mapResult = new Dictionary<string, Object>();
            try
            {
                responseData.data = blacklistWordsService.delete(id);
                responseData.success = true;
                return responseData;
            }
            catch (Exception e)
            {
                responseData.success = false;
                responseData.message = e.StackTrace;
                return responseData;
            }
        }
    }
}
