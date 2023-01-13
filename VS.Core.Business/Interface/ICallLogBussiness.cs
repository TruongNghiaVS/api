using VS.core.Request;
using VS.Core.dataEntry.User;


namespace VS.Core.Business.Interface
{
    public interface ICallLogBussiness : IGenericBussine<CallLog>
    {
        Task<CallLogReponse> GetALl(CallLogSerarchRequest request);

    }
}
