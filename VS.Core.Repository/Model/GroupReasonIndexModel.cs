namespace VS.Core.Repository.Model
{
    public class GroupReasonIndexModel : BaseIndexModel
    {
        public string? Code { get; set; }

        public string? FullName { get; set; }

        public string? Description { get; set; }

        public bool? Status { get; set; }



        public int Folder { get; set; }


        public string FolderText
        {
            get
            {
                if (this.Folder == 1)
                {
                    return "Out-bound";
                }
                return "In-bound";
            }
        }

        public string StatusText
        {
            get
            {
                if (this.Status == true)
                {
                    return "Hoạt động";
                }
                return "Không hoạt động";
            }
        }

        public int? CompanyId { get; set; }

        public string CompanyName
        {
            get
            {
                return "ACS";
            }
        }

    }
}
