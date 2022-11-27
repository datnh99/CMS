using CMS_SU21_BE.Models;
using CMS_SU21_BE.Models.Requests;
using CMS_SU21_BE.Models.Responses;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Web;
namespace CMS_SU21_BE.Services
{
    public interface HashtagService
    {   /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        bool Add(HashtagRequest request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newID"></param>
        /// <returns></returns>
        List<HashtagResponse> GetHashtag(int? newID);



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<HashtagResponse> getPopularTags();
    }
}