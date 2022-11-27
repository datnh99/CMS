using CMS_SU21_BE.Common.Base;
using CMS_SU21_BE.Models.Entities;
using CMS_SU21_BE.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_SU21_BE.Services.Implements
{
    public  class BlacklistWordsServiceImpl : BaseService,BlacklistWordsService
    {
        private BlacklistWordsRepository blacklistWordsRepository = new BlacklistWordsRepository();

        public List<BlackListWords> searchBlacklistWords(string content, int? pageSize, int? pageIndex)
        {
            return blacklistWordsRepository.searchBlacklistWords(content, pageSize, pageIndex);
        }
        public void add(List<string> content)
        {
            string username = getLoggedInUsername();
            
            foreach(string words in content)
            {
                blacklistWordsRepository.add(words, username);
            }
        }
        public bool delete(int id)
        {
            return blacklistWordsRepository.delete(id);
        }

        public List<string> getAllBlacklistWords()
        {
            return blacklistWordsRepository.getAllBlacklistWords();
        }

        public int countBlacklistWords(string content)
        {
            return blacklistWordsRepository.countBlacklistWords(content);
        }
    }
}