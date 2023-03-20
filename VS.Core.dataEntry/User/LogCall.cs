namespace VS.Core.dataEntry.User
{
    public class LogCall : BaseEntry
    {
        public string? Phone { get; set; }
        public string? NoAgree { get; set; }

        public string? LineCode { get; set; }

        public int? ProfileId { get; set; }

        public string? UserId { get; set; }

        public int? VendorId { get; set; }

        public DateTime? TimeBuisiness { get; set; }


        public LogCall()
        {
            TimeBuisiness = DateTime.Now;
        }

    }


}
