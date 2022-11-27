using System;

namespace CMS_SU21_BE.Models.Responses
{
    public class MasterDataResponse
    {
        public int id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string lowerCode { get; set; }
        public string lowerName { get; set; }
        public string createdBy { get; set; }
        public DateTime createdTime { get; set; }
        public string modifiedBy { get; set; }
        public DateTime modifiedTime { get; set; }
        public string type { get; set; }
        public bool actFlg { get; set; }
    }
}