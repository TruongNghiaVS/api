namespace VS.Core.Repository.Model
{
    public class SmsIndexModel : BaseIndexModel
    {
        public string? Phone { get; set; }

        public int Netword { get; set; }
        public string? NoAgree { get; set; }

        public string Status { get; set; }

        public string? LineCode { get; set; }

        public int? ProfileId { get; set; }

        public string? UserId { get; set; }

        public int? VendorId { get; set; }

        public DateTime? TimeBuisiness { get; set; }

        public string Content { get; set; }


    }
}
