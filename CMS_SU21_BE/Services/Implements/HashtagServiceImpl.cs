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
    public class HashtagServiceImpl : HashtagService
    {
        private HashtagRepository hashtagRepository = new HashtagRepository();
        public bool Add(HashtagRequest request)
        {
            return hashtagRepository.Add(request);        
        }

        public List<HashtagResponse> GetHashtag(int? newID)
        {
            return hashtagRepository.GetHashtag(newID);
        }

        public List<HashtagResponse> getPopularTags()
        {
            return hashtagRepository.getPopularTags();
        }
    }
}