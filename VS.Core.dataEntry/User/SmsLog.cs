namespace VS.Core.dataEntry.User
{
    public class SmsMessage : BaseEntry
    {
        public string? Phone { get; set; }
        public string? NoAgree { get; set; }

        public string Status { get; set; }

        public string? LineCode { get; set; }

        public int? ProfileId { get; set; }

        public string? UserId { get; set; }

        public int? VendorId { get; set; }

        public DateTime? TimeBuisiness { get; set; }

        public string Content { get; set; }

        public int error_code { get; set; }

        public string sn { get; set; }

        public string sms_in_queue { get; set; }

        public string task_id { get; set; }

        public SmsMessage()
        {
            TimeBuisiness = DateTime.Now;
        }

    }


}
