namespace VS.Core.dataEntry.User
{
    public class GroupEmployee : BaseEntry
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public string Status { get; set; }

        public int? ManagerId
        {
            get; set;



        }


    }
}
