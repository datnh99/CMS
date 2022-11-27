using CMS_SU21_BE.Common.Base;

namespace CMS_SU21_BE.Models.Entities
{
    public class UserFollower : BaseEntity
    {
        public string account { get; set; }

        public string follower { get; set; }
    }
}