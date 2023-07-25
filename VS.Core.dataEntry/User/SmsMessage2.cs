namespace VS.Core.dataEntry.User
{
    public class SmsMessage2 : BaseEntry
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

        public int? Netword { get; set; }




        public SmsMessage2()
        {
            TimeBuisiness = DateTime.Now;
        }

    }


}
