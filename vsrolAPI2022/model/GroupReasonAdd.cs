namespace VS.core.API.model
{
    public class GroupReasonAdd
    {
        public string? Code { get; set; }
        public string? FullName { get; set; }

        public bool? Status { get; set; }

        public int Folder { get; set; }

        public string? Description { get; set; }



    }
    public class GroupReasonUpdate
    {
        public string? Id { get; set; }
        public string? Code { get; set; }
        public string? FullName { get; set; }
        public bool? Status { get; set; }
        public int? Folder { get; set; }
        public string? Description { get; set; }

    }




}
