using CMS_SU21_BE.Common.Base;

namespace CMS_SU21_BE.Models.Entities
{
    public class Statistic : BaseEntity
    {
        public bool deleted { get; set; }

        public string account { get; set; }

        public string action { get; set; }

        public int objectId { get; set; }
    }
}