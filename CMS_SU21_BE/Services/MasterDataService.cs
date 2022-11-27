using CMS_SU21_BE.Models.Requests;
using CMS_SU21_BE.Models.Responses;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace CMS_SU21_BE.Services
{
    public interface MasterDataService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="masterDataName"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        List<MasterDataResponse> Search(string masterDataName, int? pageSize, int? pageIndex);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lowerCode"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        MasterDataResponse getByTypeAndCode(string lowerCode, string type);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        List<MasterDataResponse> getByType(string type, int? pageSize, int? pageIndex);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        bool Add(MasterDataRequest request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DeleteById(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        List<MasterDataResponse> getAllByType(string type);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aRTICLE_STATUS_TYPE"></param>
        /// <param name="listCode"></param>
        /// <returns></returns>
        List<MasterDataResponse> getByTypeAndListCode(string aRTICLE_STATUS_TYPE, List<string> listCode);
    }
}