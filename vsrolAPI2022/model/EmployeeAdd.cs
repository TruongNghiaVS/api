namespace VS.core.API.model
{
    public class EmployeeAdd
    {
        public string? UserName { get; set; }
        //public string? Code { get; set; }
        public string? RoleId { get; set; }
        //public string? Token { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public string? Phone { get; set; }
        public string? Pass { get; set; }
        public string? CreateBy { get; set; }


    }
    public class EmployeeUpdate
    {
        public string? UserName { get; set; }
        public string? Id { get; set; }
        //public string? Code { get; set; }
        public string? RoleId { get; set; }
        //public string? Token { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public string? Phone { get; set; }

        public string? Address { get; set; }

        public string? companyId { get; set; }



    }

    public class DeleteModelRequest
    {

        public string? Id { get; set; }

    }



}
