namespace VS.Core.dataEntry.User
{
    public class GroupEmployee : BaseEntry
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public string Status { get; set; }
        public int? VendorId { get; set; }

        public int? ManagerId
        {
            get; set;



        }


    }

    public class GroupMember : BaseEntry
    {
        public int? Groupid { get; set; }

        public int? Memberid { get; set; }



        public string? Status { get; set; }
        public int? VendorId { get; set; }




    }
}
