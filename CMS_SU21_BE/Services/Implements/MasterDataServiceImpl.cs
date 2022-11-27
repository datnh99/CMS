using CMS_SU21_BE.Common.Base;
using CMS_SU21_BE.Models.Requests;
using CMS_SU21_BE.Models.Responses;
using CMS_SU21_BE.Repository;
using CMS_SU21_BE.Repository.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace CMS_SU21_BE.Services.Implements
{
    public class MasterDataServiceImpl : BaseService, MasterDataService
    {
        private MasterDataRepository masterDataRepository = new MasterDataRepository();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="masterDataName"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public List<MasterDataResponse> Search(string masterDataName, int? pageSize, int? pageIndex)
        {
            return masterDataRepository.Search(masterDataName,pageSize,pageIndex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lowerCode"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public MasterDataResponse getByTypeAndCode(string lowerCode, string type)
        {
            return masterDataRepository.getByTypeAndCode(lowerCode, type);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public List<MasterDataResponse> getByType(string type, int? pageSize, int? pageIndex)
        {
            return masterDataRepository.getByType(type, pageSize, pageIndex);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool Add(MasterDataRequest request)
        {
            return masterDataRepository.Add(request);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteById(int id)
        {
            return masterDataRepository.DeleteById(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<MasterDataResponse> getAllByType(string type)
        {
            return masterDataRepository.getAllByType(type);
        }

        public List<MasterDataResponse> getByTypeAndListCode(string type, List<string> listCode)
        {
            return masterDataRepository.getByTypeAndListCode(type, listCode);
        }
    }
}