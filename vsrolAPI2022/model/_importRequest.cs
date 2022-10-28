namespace VS.core.API.model
{


    public partial class CampanginDataImport
    {
        public List<IFormFile> FileData { get; set; }
        public string? Id { get; set; }


    }

    public partial class CampanginAssigneeRequestItem
    {

        public int? SumCounted { get; set; }
        public string Id { get; set; }
        public int? OldCounted { get; set; }



    }
    public partial class CampanginAssigneeRequest
    {
        public List<CampanginAssigneeRequestItem> DataRequest { get; set; }
        public string CampangId { get; set; }
    }


}
