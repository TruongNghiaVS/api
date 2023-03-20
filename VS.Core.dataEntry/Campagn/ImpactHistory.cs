namespace VS.Core.dataEntry.Campagn
{
    public class ImpactHistory : BaseEntry
    {
        public string? ShortDescription { get; set; }
        public string? StatusIm { get; set; }

        public string? NoteIm { get; set; }

        public DateTime? Promiseday { get; set; }

        public string? MoneyPromise { get; set; }

        public DateTime? DaysuggestTime { get; set; }

        public int? StatusFollow { get; set; }

        public int? Relationship { get; set; }

        public int? ProfileId { get; set; }

        public int? Priority { get; set; }

        public int? VendorId { get; set; }

        public string? AssigeeId { get; set; }

        public string? LineCode { get; set; }
    }
}
