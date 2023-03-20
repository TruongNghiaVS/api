namespace VS.Core.Repository.Model
{
    public class GroupEmployeeIndexModel : BaseIndexModel
    {

        public string? Name { get; set; }

        public string? ManagerId { get; set; }


        public string? ManagementName { get; set; }
        public string Code { get; set; }

        public bool? IsActive { get; set; }


    }

    public class GroupEmployeeNotInGroupIndexModel
    {

        public string? FullName { get; set; }

        public string? UserName { get; set; }

        public int? Id { get; set; }



    }
}
