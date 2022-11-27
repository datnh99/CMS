using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_SU21_BE.Services
{
    public interface ArticleVersionService
    {
        int AddNew(int newID, int articleID);
    }
}