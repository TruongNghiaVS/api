namespace VS.Core.dataEntry.User
{
    public class MasterData : BaseEntry
    {
        public string? Code { get; set; }
        public string? DisplayName { get; set; }
        public string? FullName { get; set; }
        public int? Hour { get; set; }
        public int? Day { get; set; }
        public int? GroupId { get; set; }
    }

    public class Reason : MasterData
    {

    }
}
