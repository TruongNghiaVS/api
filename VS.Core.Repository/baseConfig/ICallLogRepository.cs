using VS.Core.dataEntry.User;

namespace VS.Core.Repository.baseConfig
{
    public interface ICallLogRepository : IGenericRepository<LogCall>
    {

        Task<int> CountCallBYNoAgree(string noAgree, string phone, string lineCode);
     

    }




}
