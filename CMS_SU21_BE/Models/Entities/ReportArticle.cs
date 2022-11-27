using CMS_SU21_BE.Common.Base;

namespace CMS_SU21_BE.Models.Entities
{
    public class ReportArticle : BaseEntity
    {
        public bool deleted { get; set; }

        public string reporter { get; set; }

        public int articleID { get; set; }

        public string note { get; set; }
    }
}