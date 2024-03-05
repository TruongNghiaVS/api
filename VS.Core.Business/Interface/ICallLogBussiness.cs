using VS.Core.dataEntry.User;


namespace VS.Core.Business.Interface
{
    public interface ICallLogBussiness : IGenericBussine<LogCall>
    {

        Task<int> CountCallBYNoAgree (string noAgree, string phone, string lineCode);
    }
}
