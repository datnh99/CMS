using CMS_SU21_BE.Common.Base;
using CMS_SU21_BE.Common.Constant;
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
    public class DenounceArticleServiceImpl : BaseService, DenounceArticleService
    {
        private DenounceArticleRepository denounceArticleRepository = new DenounceArticleRepository();

        private MasterDataService masterDataService = new MasterDataServiceImpl();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public int add(DenounceArticleRequest request)
        {
            request.createdBy = getLoggedInUsername();
            DenounceArticleResponse denounceArticleResponse = denounceArticleRepository.getByReporterAndArticleID(request.createdBy, request.articleID);
            if (denounceArticleResponse.id > 0)
            {
                throw new ArgumentException(String.Format("You have already reported this post!"));
            }
            return denounceArticleRepository.add(request);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reporter"></param>
        /// <param name="articleID"></param>
        /// <returns></returns>
        public DenounceArticleResponse getByReporterAndArticleID(string reporter, int articleID)
        {
            return denounceArticleRepository.getByReporterAndArticleID(reporter, articleID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="articleID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<DenounceArticleResponse> getByArticleId(int articleID, int pageIndex, int pageSize)
        {
            List<MasterDataResponse> masterDataResponses = masterDataService.getAllByType(MasterDataConstant.REASON_DENOUNCE_TYPE);

            List<DenounceArticleResponse> denounceArticleResponses = denounceArticleRepository.getByArticleId(articleID, pageIndex, pageSize);

            for (int i = 0; i < denounceArticleResponses.Count; i++)
            {
                denounceArticleResponses[i].index = (pageIndex - 1) * pageSize + i + 1;
                if (!string.IsNullOrEmpty(denounceArticleResponses[i].reason)) {
                    string reasonTemp = "";
                    var numbers = denounceArticleResponses[i].reason.Split(',').Select(Int32.Parse).ToList();
                    for(int k = 0; k < numbers.Count; k++)
                    {
                        for (int j = 0; j < masterDataResponses.Count; j++)
                        {
                            if (masterDataResponses[j].id == numbers[k])
                            {
                                if(string.IsNullOrEmpty(reasonTemp))
                                {
                                    reasonTemp = masterDataResponses[j].name;
                                } else
                                {
                                    reasonTemp = reasonTemp + ", " + masterDataResponses[j].name;
                                }
                            }
                        }
                    }
                    denounceArticleResponses[i].reasonName = reasonTemp;
                }
            }

            return denounceArticleResponses;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="articleID"></param>
        /// <returns></returns>
        public int countByArticleId(int articleID)
        {
            return denounceArticleRepository.countByArticleId(articleID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool deleteByArticleID(int id)
        {
            return denounceArticleRepository.deleteByArticleID(id);
        }
    }
}