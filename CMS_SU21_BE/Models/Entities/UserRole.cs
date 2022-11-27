using CMS_SU21_BE.Common.Base;

namespace CMS_SU21_BE.Models
{
    public class UserRole : BaseEntity
    {
        public string account { get; set; }

        public string roleCode { get; set; }

    }
}