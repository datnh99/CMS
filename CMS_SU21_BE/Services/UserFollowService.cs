using CMS_SU21_BE.Models.Requests;
using CMS_SU21_BE.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_SU21_BE.Services
{
    public interface UserFollowService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        Boolean getFollowedUser(string account);

        /// <summary>
        /// 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        int add(UserFollowRequest request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        bool update(UserFollowRequest request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        bool deleteByAccount(string account);
    }
}