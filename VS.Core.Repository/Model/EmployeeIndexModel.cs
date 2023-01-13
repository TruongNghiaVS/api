namespace VS.Core.Repository.Model
{
    public class EmployeeIndexModel : BaseIndexModel
    {

        public string? UserName { get; set; }

        public string? RoleId { get; set; }

        public string? Email { get; set; }

        public string? FullName { get; set; }

        public string? PhoneNumber { get; set; }

        public bool? IsActive { get; set; }

        public string LineCode { get; set; }
        public string PositionName
        {
            get
            {
                return "Collection";
            }
        }

        public string DepartmentName { get; set; }

        public string CompanyName
        {

            get
            {
                return "ACS";
            }
        }

        public EmployeeIndexModel() : base()
        {

        }
    }
}
