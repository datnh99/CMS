using CMS_SU21_BE.Common.Base;
using CMS_SU21_BE.Models.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace CMS_SU21_BE.Services.Implements
{
    public class EmailServiceImpl : BaseService,EmailService
    {
        public Boolean sendEmail(MailTemplate mailtemplate)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                string mailFrom = ConfigurationManager.AppSettings["mailFrom"];
                string passWord = ConfigurationManager.AppSettings["password"];
                string username = getLoggedInUsername();
                mailtemplate.subject = username + mailtemplate.subject;
                mail.From = new MailAddress(mailFrom);
                foreach (var mailTo in mailtemplate.toMail.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                {
                    mail.To.Add(mailTo);
                }
                string FilePath = HttpContext.Current.Server.MapPath("~/Mailtemplate/index.html");
                StreamReader str = new StreamReader(FilePath);
                string MailText = str.ReadToEnd();
                MailText = MailText.Replace("[linkReference]", mailtemplate.linkReference);
                str.Close();
                mail.Subject = mailtemplate.subject;
                mail.Body = MailText;
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(mailFrom, passWord);
                SmtpServer.EnableSsl = true;
                mail.IsBodyHtml = true;
                SmtpServer.Send(mail);
                return true;
            }catch(Exception e)
            {
                return false;
            }
        }
        public Boolean testEmailConnection()
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                string mailFrom = ConfigurationManager.AppSettings["mailFrom"];
                string passWord = ConfigurationManager.AppSettings["password"];
                string username = getLoggedInUsername();
                mail.From = new MailAddress(mailFrom);
                mail.To.Add(username+"@fpt.edu.vn");
                mail.Subject ="FUSU TEST EMAIL";
                mail.Body = "FUSU TEST EMAIL";
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(mailFrom, passWord);
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}