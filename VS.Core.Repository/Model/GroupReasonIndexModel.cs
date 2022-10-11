namespace VS.Core.Repository.Model
{
    public class GroupReasonIndexModel : BaseIndexModel
    {
        public string? Code { get; set; }

        public string? FullName { get; set; }

        public string? Description { get; set; }

        public bool? Status { get; set; }

        public int Folder { get; set; }

        public int? CompanyId { get; set; }

    }
}
