namespace VS.Core.dataEntry.User
{
    public class Account : BaseEntry
    {
        public string? UserName { get; set; }
        //public string? Code { get; set; }
        public string? RoleId { get; set; }
        //public string? Token { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public List<string>? Permissions { get; set; }
        public string? Phone { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public int OrgId { get; set; }

        public int? VendorId { get; set; }

        public string? LineCode { get; set; }


        public string? Pass { get; set; }

        //public bool isHead
        //{
        //    get
        //    {
        //        return (RoleCode == "head") ? true : false;
        //    }
        //}
        //public bool isDev
        //{
        //    get
        //    {
        //        return (RoleCode == "dev") ? true : false;
        //    }
        //}
        //public bool isAdmin
        //{
        //    get
        //    {
        //        return (RoleCode == "admin" || RoleCode == "head") ? true : false;
        //    }
        //}
        //public bool isSale
        //{
        //    get
        //    {
        //        return (RoleCode == "sale") ? true : false;
        //    }
        //}
        //public bool isRsmAsmSS
        //{
        //    get
        //    {
        //        return (RoleCode == "rsm" || RoleCode == "asm" || RoleCode == "ss") ? true : false;
        //    }
        //}

    }
}
