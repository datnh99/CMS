using CMS_SU21_BE.Common.Base;

namespace CMS_SU21_BE.Models.Entities
{
    public class FileEntity : BaseEntity
    {
        public string name { get; set; }

        public long size { get; set; }

        public string link { get; set; }

        public int articleId { get; set; }

        public bool deleted { get; set; }

        public string originalName { get; set; }
    }
}