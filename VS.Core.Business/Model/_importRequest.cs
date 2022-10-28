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
}
