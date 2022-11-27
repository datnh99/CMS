using CMS_SU21_BE.Common.Base;
using CMS_SU21_BE.Repository;
using CMS_SU21_BE.Repository.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_SU21_BE.Services.Implements
{
    public class ProfileServiceImpl : BaseService, ProfileService
    {

        private ProfileRepository profileRepository = new ProfileRepository();
        public int countProfileArticle(string status)
        {
            string username = getLoggedInUsername();
            return profileRepository.countProfileArticle(status, username);
        }

        public int countProfileComments()

        {
            string username = getLoggedInUsername();
            return profileRepository.countProfileComments(username);
        }
    }
}