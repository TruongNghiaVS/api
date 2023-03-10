namespace VS.Core.Business.Model
{
    public class CampanginDataImportRequest
    {
        public List<ProfileHandler> ListData { get; set; }
        public string? Id { get; set; }

        public CampanginDataImportRequest()
        {
            ListData = new List<ProfileHandler>();
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
