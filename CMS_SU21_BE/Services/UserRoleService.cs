using CMS_SU21_BE.Models;
using CMS_SU21_BE.Models.Requests;
using CMS_SU21_BE.Models.Responses;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace CMS_SU21_BE.Services
{
    public interface UserRoleService
    {
        /// <summary>
        /// Add new UserRole
        /// </summary>
        /// <param name="userRole"></param>
        /// <returns></returns>
        bool Add(UserRoleRequest request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        bool updateUserRoles(UserRoleRequest request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        bool checkExistRoleOfUser(string account, string roleCode);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        /// <param name="roleCode"></param>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        List<UserRoleResponse> search(UserRoleRequest request, int value1, int value2);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        int totalSearchUserRole(UserRoleRequest request);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool deleteById(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        List<UserRoleResponse> getUserRoleByAccount(string account);
    }
}