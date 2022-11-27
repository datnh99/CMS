using CMS_SU21_BE.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_SU21_BE.Services
{
    interface EmailService
    {   /// <summary>
        /// 
        /// </summary>
        /// <param name="mailtemplate"></param>
        /// <returns></returns>
        Boolean sendEmail(MailTemplate mailtemplate);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Boolean testEmailConnection();
    }
}
