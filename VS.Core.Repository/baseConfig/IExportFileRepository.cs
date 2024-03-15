using VS.core.Request;
using VS.Core.dataEntry.User;


namespace VS.Core.Repository.baseConfig
{
    public interface IExportFileRepository : IGenericRepository<CampagnProfile>
    {
        Task<CampagnProfileExportReponse> GetAllCase(CampagnProfileExportRequest request);
        Task<CampagnProfileExportReponse> GetAllCasev2(CampagnProfileExportRequest request);
    }




}
