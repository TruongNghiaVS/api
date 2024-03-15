namespace VS.Core.dataEntry.User
{
    public class GroupReason : BaseEntry
    {
        public string? Code { get; set; }

        public string? FullName { get; set; }

        public string? Description { get; set; }

        public bool? Status { get; set; }

        public int? Folder { get; set; }

        public int? VendorId { get; set; }


    }

    public class ItemCheck
    {

        public int? Value { get; set; }
    }


   


}
