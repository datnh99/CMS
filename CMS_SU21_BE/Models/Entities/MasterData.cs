using CMS_SU21_BE.Common.Base;

namespace CMS_SU21_BE.Models.Entities
{
    public class MasterData : BaseEntity
    {
        public string code { get; set; }

        public string name { get; set; }

        public string lowerCode { get; set; }

        public string lowerName { get; set; }

        public string type { get; set; }

        public bool actFlg { get; set; }
    }
}