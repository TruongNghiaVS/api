using VS.core.Request;
using VS.Core.dataEntry.User;

namespace VS.Core.Repository.baseConfig
{
    public interface ICallLogRepository : IGenericRepository<CallLog>
    {

        Task<CallLogReponse> GetALl(CallLogSerarchRequest request);


    }




}
