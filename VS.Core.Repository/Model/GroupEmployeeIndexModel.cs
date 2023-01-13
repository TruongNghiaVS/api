namespace VS.Core.Repository.Model
{
    public class GroupEmployeeIndexModel : BaseIndexModel
    {

        public string? Name { get; set; }

        public string? ManagerId { get; set; }

        public string? ManagerName { get; set; }

        public string Code { get; set; }

        public bool? IsActive { get; set; }


    }
}
