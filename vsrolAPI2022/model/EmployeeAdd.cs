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
        public string? LineCode { get; set; }
        public string? Pass { get; set; }
        public string? CreateBy { get; set; }

        public int? LineId { get; set; }

    }
    public class EmployeeUpdate
    {
        public int Status { get; set; }
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

        public string? LineCode { get; set; }

    }

    public class DeleteModelRequest
    {

        public string? Id { get; set; }

    }


    public class EmployeeChangePassword
    {
        public string? PaswordNew { get; set; }
        public string? RepeatPassword { get; set; }

        public string? UserId { get; set; }


    }



}
