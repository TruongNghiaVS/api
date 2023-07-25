namespace VS.Core.Business.Model
{
    public class CampanginDataImportRequest
    {
        public List<ProfileHandler> ListData { get; set; }
        public string? Id { get; set; }
        public bool? Updatedata { get; set; }

        public CampanginDataImportRequest()
        {
            ListData = new List<ProfileHandler>();
            Updatedata = false;
        }

    }

    public class CampanginDataImportDeleted
    {
        public List<string> ListData { get; set; }
        public string? Id { get; set; }

        public CampanginDataImportDeleted()
        {
            ListData = new List<string>();
        }

    }

    public class MasterDataImportRequest
    {
        public List<ReasonHandler> ListData { get; set; }
        public string? Id { get; set; }

        public MasterDataImportRequest()
        {
            ListData = new List<ReasonHandler>();
        }

    }
}
