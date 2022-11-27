using System;

namespace CMS_SU21_BE.Models.Requests
{
    public class MasterDataRequest
    {
        public int id { get; set; }
        public String code { get; set; }
        public String name { get; set; }
        public String lowerCode { get; set; }
        public String lowerName { get; set; }
        public String createdBy { get; set; }
        public DateTime createdTime { get; set; }
        public String modifiedBy { get; set; }
        public DateTime modifiedTime { get; set; }
        public String type { get; set; }
        public bool actFlg { get; set; }
    }
}