using CMS_SU21_BE.Common.Base;
using CMS_SU21_BE.Repository;
using CMS_SU21_BE.Repository.Implements;
using System;
using System.Linq;
using System.Web;

namespace CMS_SU21_BE.Services.Implements
{
    public class ArticleVersionServiceImpl : BaseService, ArticleVersionService
    {
        private ArticleVersionRepository articleVersionRepository = new ArticleVersionRepository();

        public int AddNew(int newID, int articleID)
        {
            string account = getLoggedInUsername();
            return articleVersionRepository.AddNew(newID, articleID, account);
        }
    }
}