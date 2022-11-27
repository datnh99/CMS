using CMS_SU21_BE.Common.Base;

namespace CMS_SU21_BE.Models.Entities
{
    public class CommentEntity : BaseEntity
    {
        public bool deleted { get; set; }

        public int articleID { get; set; }

        public  string content { get; set; }

        public int parentID { get; set; }
    }
}