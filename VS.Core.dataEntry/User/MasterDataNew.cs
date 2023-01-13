namespace VS.Core.dataEntry.User
{
    public class MasterDataNew : BaseEntry
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Status { get; set; }

        public int Type { get; set; }

        public MasterDataNew()
        {
            Status = "";
        }

    }


}
