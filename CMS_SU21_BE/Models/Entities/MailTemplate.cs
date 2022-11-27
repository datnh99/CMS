
namespace CMS_SU21_BE.Models.Entities
{
    public class MailTemplate
    {
        public string subject { get; set; }

        public string body { get; set; }

        public string fromMail { get; set; }

        public string toMail { get; set; }

        public string linkReference { get; set; }
    }
}