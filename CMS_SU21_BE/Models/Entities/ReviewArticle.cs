using CMS_SU21_BE.Common.Base;

namespace CMS_SU21_BE.Models.Entities
{
    public class ReviewArticle : BaseEntity
    {
        public int articleId { get; set; }

        public string account { get; set; }

        public bool approve { get; set; }

        public string note { get; set; }
    }
}