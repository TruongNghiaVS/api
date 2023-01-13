namespace VS.Core.Repository.Model
{
    public class LoginReportIndexModel : BaseIndexModel
    {
        public string? UserName { get; set; }
        public string? FullName { get; set; }
        public string? ActionUser { get; set; }
        public string? Note { get; set; }
        public DateTime? BusinessTime { get; set; }


    }
}
