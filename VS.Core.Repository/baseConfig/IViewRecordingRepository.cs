using VS.core.Request;
using VS.Core.dataEntry.User;

namespace VS.Core.Repository.baseConfig
{
    public interface IViewRecordingRepository : IGenericRepository<ViewRecording>
    {
        Task<GetDashboardQcReponse> GetDataQc(GetDashboardQcRequest request);

    }




}
