using VS.core.Request;
using VS.Core.dataEntry.User;


namespace VS.Core.Repository.baseConfig
{
    public interface IExportFileRepository : IGenericRepository<CampagnProfile>
    {
        Task<CampagnProfileExportReponse> GetAllCampagnProfile(CampagnProfileExportRequest request);

    }




}
