﻿namespace VS.Core.Repository.Model
{

    public class GroupEmployeeViewIndexModel : EmployeeIndexModel
    {

        public string? ManagementName { get; set; }
    }
    public class EmployeeIndexModel : BaseIndexModel
    {

        public string? UserName { get; set; }

        public int? VendorId { get; set; }
        public string? RoleId { get; set; }

        public string? Email { get; set; }

        public string? FullName { get; set; }

        public string? PhoneNumber { get; set; }

        public bool? IsActive { get; set; }

        public string LineCode { get; set; }
        public int? LineId { get; set; }

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
                if (VendorId == 62)
                {
                    return "Mirae";
                }
                if (VendorId == 96)
                {
                    return "Jaccs";
                }
                return "";
            }
        }



        public EmployeeIndexModel() : base()
        {

        }
    }
}