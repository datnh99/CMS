using CMS_SU21_BE.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_SU21_BE.Services
{
    interface BlacklistWordsService
    {   
        /// <summary>
         /// 
         /// </summary>
         /// <param name="content"></param>
         /// <param name="pageSize"></param>
         /// <param name="pageIndex"></param>
         /// <returns></returns>
        List<BlackListWords> searchBlacklistWords(string content, int? pageSize, int? pageIndex);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        void add(List<string> content);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool delete(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<string> getAllBlacklistWords();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        int countBlacklistWords(string content);
    }
}
