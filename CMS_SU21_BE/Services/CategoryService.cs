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
    public interface CategoryService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryName"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        List<CategoryResponse> Search(string categoryName, int? pageSize, int? pageIndex);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        int totalCategory();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryName"></param>
        /// <param name="account"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        List<CategoryResponse> SearchByNameAndManager(string categoryName, string account, int? pageSize, int? pageIndex);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        int countBySearch(string categoryName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        int Add(CategoryRequest request);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<CategoryResponse> getAll();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        bool updateCategory(CategoryRequest request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        bool updateManageForCategory(List<int> categoryIDs, string manager);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        bool DeleteById(int categoryID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryName"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        int countByNameAndManager(string categoryName, string account);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        CategoryResponse getCategoryById(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryName"></param>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        List<CategoryResponse> searchCategoryToAddManager(string categoryName, int pageSize, int pageIndex);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        int countBySearchCategoryToAddManager(string categoryName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="manager"></param>
        /// <returns></returns>
        List<CategoryResponse> getByManager(string manager);
    }

}