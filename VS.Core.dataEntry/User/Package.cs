namespace VS.Core.dataEntry.User
{
    public class Package : BaseEntry
    {
        public string? Name { get; set; }

        public int? Type { get; set; }

        public string? Value { get; set; }

        public string? IdUser { get; set; }

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }
        public int? VendorId { get; set; }

        public int? Status { get; set; }

        public int? Priorities { get; set; }

    }

    public class DPDItem
    {
        public string value { get; set; }
        public string lable { get; set; }
    }

}
