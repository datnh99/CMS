using CMS_SU21_BE.Common.Base;
using CMS_SU21_BE.Common.Helper;
using CMS_SU21_BE.Models.Entities;
using CMS_SU21_BE.Models.Requests;
using CMS_SU21_BE.Services;
using CMS_SU21_BE.Services.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CMS_SU21_BE.Controllers
{
    [RoutePrefix("api/email")]
    public class EmailController : ApiController
    {
        private EmailService emailService = new EmailServiceImpl();

        [DisableCors]
        [HttpPost]
        [Route("sendEmail/{send-email}")]
        public ResponseData sendEmail([FromBody] EmailRequest request)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                MailTemplate template = new MailTemplate();
                template.body = "Please click the following link to see more information : " + request.linkReference;
                template.subject = " has shared an articles for you";
                template.toMail = request.mailTo;
                template.linkReference = request.linkReference;
                responseData.data = emailService.sendEmail(template);
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
        [Route("testEmailConnection/{tes-email-connection}")]
        public ResponseData testEmailConnection()
        {
            ResponseData responseData = new ResponseData();
            try
            {
                responseData.data = emailService.testEmailConnection();
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