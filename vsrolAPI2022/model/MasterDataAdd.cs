namespace VS.core.API.model
{
    public class MasterDataAdd
    {
        public string? Code { get; set; }
        public string? DisplayName { get; set; }
        public string? FullName { get; set; }

        public int? Hour { get; set; }
        public int? Day { get; set; }

        public int? GroupId { get; set; }


    }
    public class MasterDataUpdate
    {

        public string? Id { get; set; }
        //public string? Code { get; set; }
        public string? DisplayName { get; set; }
        public string? FullName { get; set; }


        public int? Hour { get; set; }
        public int? Day { get; set; }



    }

    public class GroupEmployeeAdd
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public int? ManagerId { get; set; }
        public int? IsActive { get; set; }
        public GroupEmployeeAdd()
        {
            IsActive = 1;
        }
    }

    public class AddMemberGroupRequest
    {
        public int? Groupid { get; set; }
        public int? Memberid { get; set; }

    }
    public class GroupEmployeeUpdate
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public int? ManagerId { get; set; }
        public int? IsActive { get; set; }

        public string? Id { get; set; }
    }


}
